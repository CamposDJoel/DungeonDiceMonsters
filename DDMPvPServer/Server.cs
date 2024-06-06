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
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public Server()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
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

        static Server? staticServerObject;

        public static void WaitForClients()
        {
            while (true)
            {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                TcpClient ConnectingClient = ServerSocket.AcceptTcpClient();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                lock (_lock) list_clients.Add(clinetcount, ConnectingClient);
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} connected!", clinetcount));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                Thread t = new Thread(handle_clients);
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
                t.Start(clinetcount);
                clinetcount++;
            }
        }
        public static void handle_clients(object o)
        {
            int id = (int)o;
            TcpClient client;
            TcpClient? Opponetclient = null;

            lock (_lock) client = list_clients[id];

            //Step 1: Set a Ref to the Match this Client Belongs to
            Match? ActiveMatch = null;
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

                    //Before you send the data. data may content multiple messages from the server, split them
                    string[] MessagesReceived = data.Split('$');

                    for (int x = 0; x < MessagesReceived.Length; x++)
                    {
                        string Message = MessagesReceived[x];
                        if (Message != "")
                        {
                            //Step 3: Parse this data
                            string[] MessageTokens = data.Split('|');

                            //Step 4: Handle this data
                            string MessageKey = MessageTokens[0];

                            //Step 5: Set the Client's Player Color
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                            PlayerColor ClientPlayerColor = ActiveMatch.GetPlayerColor(id);
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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

                                case "[GAME OVER]":
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                                    staticServerObject.UpdateConnectionLog(string.Format("X Client ID: {0} disconnected during the Game Over!", id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                                    ActiveMatch.AddLogMessage(string.Format("X Player [{0}] disconnected during the game over. Match will be closed.", ClientPlayerColor));
                                    ActiveMatch.CloseMatch();
                                    //Just forward this back to the same clieent so the thread can end the connection
                                    //from the client side.
                                    SendMessage("[GAME OVER]", client);
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
                    }                   
                }
                catch
                {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                    if (ActiveMatch.IsClosed())
                    {
                        //Both players already disconnected at this point, just break the loop
                        break;
                    }
                    else if (ActiveMatch.AreBothPlayersReady())
                    {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} disconnected during the active match!", id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.

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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
                        staticServerObject.UpdateConnectionLog(string.Format("Client ID: {0} disconnected!", id));
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                        staticServerObject.UpdateConnectionLog(string.Format("Match ID: {0} open for players.", matchcount));
                        break;
                    }
#pragma warning restore CS8602 // Dereference of a possibly null reference.
                }
            }

            lock (_lock) list_clients.Remove(id);
            client.Client.Shutdown(SocketShutdown.Both);
            client.Close();
        }
        public static void SendMessage(string data, TcpClient recepient)
        {
            //Readd the "$" before fowarding 
            data += "$";

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
            ServerSocket = new TcpListener(IPAddress.Parse("192.168.0.220"), 5000);
            //ServerSocket = new TcpListener(IPAddress.Any, 5000);
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
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            ServerSocket.Stop();
#pragma warning restore CS8602 // Dereference of a possibly null reference.
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