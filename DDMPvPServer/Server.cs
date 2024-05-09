//Joel Campos
//4/1/2024
//Server Form

using System.Drawing;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace DDMPvPServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
        }

        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static readonly Dictionary<int, Match> list_Matches = new Dictionary<int, Match>();
        static int clinetcount = 1;
        static int matchcount = 1;
        static TcpListener? ServerSocket;
        Thread waitingforclients;

        StringBuilder SB = new StringBuilder();

        static Server staticServerObject;

        public static void WaitForClients()
        {
            while (true)
            {
                TcpClient ConnectingClient = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(clinetcount, ConnectingClient);
                staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} connected!", clinetcount));

                //add the client to the current active match
                Match activeMatch = list_Matches[matchcount];
                activeMatch.addPlayer(clinetcount);
                staticServerObject.UpdateConnectionLog(string.Format("Client added to Match ID: {0}", matchcount));

                if (activeMatch.IsMatchFull())
                {
                    //THE CONNECTING PLAYER WILL BE PLAYER 2 AKA BLUE Player
                    staticServerObject.UpdateConnectionLog("2 Players joined the match, waiting for deck info to be exchanged.");
                    //Send this conencting player the deck info of Player 1 (RED)
                    string RedsPlayerName = activeMatch.GetPlayerName(PlayerColor.RED);
                    string RedsDeckInfo = activeMatch.GetDeckData(PlayerColor.RED);
                    SendMessage(string.Format("{0}|{1}|{2}", "[OPPONENT DATA RED]", RedsPlayerName, RedsDeckInfo), ConnectingClient);

                    //Now create a new match to be open this match is now full
                    matchcount++;
                    list_Matches.Add(matchcount, new Match(matchcount));
                    staticServerObject.AddMatchToListUI();
                    staticServerObject.UpdateConnectionLog(string.Format("Match ID: {0} open for players.", matchcount));
                }
                else
                {
                    staticServerObject.UpdateConnectionLog("Waiting for a second player to join the match....");
                    SendMessage("[WAITING FOR PLAYER 2]", ConnectingClient);
                }

                Thread t = new Thread(handle_clients);
                t.Start(clinetcount);
                clinetcount++;
            }
        }
        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;
            TcpClient Opponetclient = null;

            lock (_lock) client = list_clients[id];

            //Step 1: Set a Ref to the Match this Client Belongs to
            Match ActiveMatch = null;
            foreach (KeyValuePair<int, Match> match in list_Matches)
            {
                Match thisMatch = match.Value;

                if (thisMatch.ContainsPlayer(id))
                {
                    ActiveMatch = thisMatch;
                    break;
                }
            }

            while (true)
            {
                try
                {
                    //Step 2: Extract the data received from this call (AKA var "data")
                    NetworkStream stream = client.GetStream();
                    byte[] buffer = new byte[1024];

                    int byte_count = stream.Read(buffer, 0, buffer.Length);
                    if (byte_count == 0)
                    {
                        break;
                    }
                    string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                    //Step 3: Parse this data
                    string[] MessageTokens = data.Split('|');

                    //Step 4: Handle this data
                    string MessageKey = MessageTokens[0];

                    //Step 5: Set the Client's Player Color
                    PlayerColor ClientPlayerColor = ActiveMatch.GetPlayerColor(id);

                    //Log the message received
                    ActiveMatch.AddLogMessage(string.Format("Message Received from Player [{0}]: [{1}]", ClientPlayerColor, data));

                    switch (MessageKey)
                    {
                        //In this case, one of the player that just connected to a match sent its 
                        //Player info to exchange with the opponent
                        case "[MC PLAYER INFO]":
                            //Set the Player info to the match
                            ActiveMatch.SetPlayerInfo(ClientPlayerColor, MessageTokens[1], MessageTokens[2]);
                            if (ActiveMatch.AreBothPlayersReady())
                            {
                                int OpponentClientIDA = ActiveMatch.GetOpponentClientID(ClientPlayerColor);
                                Opponetclient = list_clients[OpponentClientIDA];
                                PlayerColor OpponentPlayerColor = ActiveMatch.GetPlayerColor(id);
                                //Send the opponent player the Clients Deck Info
                                string OpponentPlayerName = ActiveMatch.GetPlayerName(OpponentPlayerColor);
                                string OpponentDeckInfo = ActiveMatch.GetDeckData(OpponentPlayerColor);
                                string notificationResponse = string.Format("{0}|{1}|{2}", "[OPPONENT DATA BLUE]", OpponentPlayerName, OpponentDeckInfo);
                                SendMessage(notificationResponse, Opponetclient);
                                ActiveMatch.AddLogMessage(string.Format("Sending [RED] a notification response: [{0}]", notificationResponse));
                            }
                            break;

                        //All other messages will simply forward the messages to the opponent client
                        default:
                            int OpponentClientID = ActiveMatch.GetOpponentClientID(ClientPlayerColor);
                            Opponetclient = list_clients[OpponentClientID];
                            SendMessage(data, Opponetclient);
                            ActiveMatch.AddLogMessage("Message forwared to opponent player!");
                            break;
                    }
                }
                catch
                {
                    if(ActiveMatch.IsClosed())
                    {
                        //Both players already disconnected at this point, just break the loop
                        break;
                    }
                    else if (ActiveMatch.AreBothPlayersReady())
                    {
                        staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} disconnected during the active match!", id));

                        //Send the opponent player a notification message this player has been disconnected
                        //Once this player receives the notification, their client app will disconnect them from the server manually.
                        PlayerColor ClientPlayerColor = ActiveMatch.GetPlayerColor(id);
                        int OpponentClientIDA = ActiveMatch.GetOpponentClientID(ClientPlayerColor);
                        Opponetclient = list_clients[OpponentClientIDA];
                        SendMessage("[OPPONENT DISCONNECT]", Opponetclient);

                        //Then mark this match as closed, no more actions will be taken on this match
                        ActiveMatch.AddLogMessage(string.Format("Player [{0}] disconnected in the middel of the match. Match will be closed.", ClientPlayerColor));
                        ActiveMatch.CloseMatch();
                        break;
                    }
                    else
                    {
                        //The match is waiting for the second player to join.
                        //Remove this player form the match
                        ActiveMatch.RemovePlayerInWaiting();
                        staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} disconnected!", id));
                        staticServerObject.UpdateConnectionLog(string.Format("Match ID: {0} open for players.", matchcount));
                        break;
                    }
                }
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        public static void SendMessage(string data, TcpClient recepient)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(data);

            lock (_lock)
            {
                NetworkStream stream = recepient.GetStream();
                stream.Write(buffer, 0, buffer.Length);
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void UpdateConnectionLog(string message)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                SB.AppendLine(string.Format("LOG#{0}: {1}", SB.Length, message));
                txtConnectionLog.Text = SB.ToString();
            }));
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            staticServerObject = this;
            btnStart.Visible = false;
            //ServerSocket = new TcpListener(IPAddress.Parse("192.168.0.220"), 5000);
            ServerSocket = new TcpListener(IPAddress.Any, 5000);
            ServerSocket.Start();

            //Start the first match
            listMatches.Items.Add("ID: " + matchcount);
            list_Matches.Add(matchcount, new Match(matchcount));
            UpdateConnectionLog(string.Format("Match ID: {0} open for players.{1}", matchcount, Environment.NewLine));
            waitingforclients = new Thread(WaitForClients);
            waitingforclients.Start();
            btnStop.Visible = true;
            listMatches.SetSelected(0, true);
        }
        private void btnStop_Click(object sender, EventArgs e)
        {
            btnStop.Visible = false;
            ServerSocket.Stop();
            list_Matches.Clear();
            btnStart.Visible = true;
        }
        private void listMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = listMatches.SelectedIndex + 1;
            Match selectedMatch = list_Matches[indexSelected];
            UpdateMatchLogsDiplay(selectedMatch);
        }
        private void AddMatchToListUI()
        {
            Invoke(new MethodInvoker(delegate ()
            {
                listMatches.Items.Add(string.Format("ID: {0}", matchcount));
            }));
        }
        private void UpdateMatchLogsDiplay(Match activeMatch)
        {
            Invoke(new MethodInvoker(delegate ()
            {
                txtMatchOutput.Text = "";
                txtMatchOutput.Text = activeMatch.GetLogs();
            }));
        }
    }
}