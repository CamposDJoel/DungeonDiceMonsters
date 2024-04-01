//Joel Campos
//4/1/2024
//Server Form

using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;

namespace DDMPvPServer
{
    public partial class Server : Form
    {
        public Server()
        {
            InitializeComponent();
            txtStaticConnectionLog = txtConnectionLog;
            txtStaticMatchOutput = txtMatchOutput;
        }

        static readonly object _lock = new object();
        static readonly Dictionary<int, TcpClient> list_clients = new Dictionary<int, TcpClient>();
        static readonly Dictionary<int, Match> list_Matches = new Dictionary<int, Match>();
        static int clinetcount = 1;
        static int matchcount = 1;
        static TcpListener ServerSocket;
        static TextBox txtStaticConnectionLog;
        static TextBox txtStaticMatchOutput;
        Thread waitingforclients;


        public static void WaitForClients()
        {
            while (true)
            {
                TcpClient ConnectingClient = ServerSocket.AcceptTcpClient();
                lock (_lock) list_clients.Add(clinetcount, ConnectingClient);
                UpdateConnectionLog(string.Format("Client ID: {0} connected!{1}", clinetcount, Environment.NewLine));

                //add the client to the current active match
                Match activeMatch = list_Matches[matchcount];
                activeMatch.addPlayer(clinetcount);
                UpdateConnectionLog(string.Format("Client added to Match ID: {0}{1}", matchcount, Environment.NewLine));
                UpdateMatchLogsDiplay(activeMatch);

                if (activeMatch.IsMatchFull())
                {
                    //THE CONNECTING PLAYER WILL BE PLAYER 2 AKA BLUE Player
                    UpdateConnectionLog("2 Players joined the match, waiting for deck info to be exchanged." + Environment.NewLine);
                    //Send this conencting player the deck info of Player 1 (RED)
                    string RedsPlayerName = activeMatch.GetPlayerName(PlayerColor.RED);
                    string RedsDeckInfo = activeMatch.GetDeckData(PlayerColor.RED);
                    SendMessage(string.Format("{0}|{1}|{2}", "[OPPONENT DATA RED]", RedsPlayerName, RedsDeckInfo), ConnectingClient);

                    //Now create a new match to be open this match is now full
                    matchcount++;
                    list_Matches.Add(matchcount, new Match(matchcount));
                    UpdateConnectionLog(string.Format("Match ID: {0} open for players.{1}", matchcount, Environment.NewLine));
                }
                else
                {
                    UpdateConnectionLog("Waiting for a second player to join the mathc...." + Environment.NewLine);
                    SendMessage("[WAITING FOR PLAYER 2]", ConnectingClient);
                }

                UpdateMatchLogsDiplay(activeMatch);

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

            while (true)
            {
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

                //Step 2: Set the Client's Player Color
                PlayerColor ClientPlayerColor = ActiveMatch.GetPlayerColor(id);

                //Step 3: Set the ref to the Opponent TcpClient Object in case we need to 
                //forward message to it also its color
                //WARNING, if no oppent has been matched, dont do this.                
                PlayerColor OpponentPlayerColor = PlayerColor.NONE;
                if (ActiveMatch.IsMatchFull())
                {
                    int OpponentClientID = -1;
                    OpponentClientID = ActiveMatch.GetOpponentClientID(ClientPlayerColor);
                    Opponetclient = list_clients[OpponentClientID];
                    OpponentPlayerColor = ActiveMatch.GetPlayerColor(id);
                }

                //Step 4: Extract the data received from this call (AKA var "data")
                NetworkStream stream = client.GetStream();
                byte[] buffer = new byte[1024];
                int byte_count = stream.Read(buffer, 0, buffer.Length);
                if (byte_count == 0)
                {
                    break;
                }
                string data = Encoding.ASCII.GetString(buffer, 0, byte_count);

                //Step 5: Parse this data
                string[] MessageTokens = data.Split('|');

                //Step 6: Handle this data
                string MessageKey = MessageTokens[0];
                switch (MessageKey)
                {
                    //In this case, one of the player that just connected to a match sent its 
                    //Player info to exchange with the opponent
                    case "[MC PLAYER INFO]":
                        //Set the Player info to the match
                        ActiveMatch.SetPlayerInfo(ClientPlayerColor, MessageTokens[1], MessageTokens[2]);
                        if (ActiveMatch.AreBothPlayersReady())
                        {
                            //Send the opponent player the Clients Deck Info
                            string OpponentPlayerName = ActiveMatch.GetPlayerName(OpponentPlayerColor);
                            string OpponentDeckInfo = ActiveMatch.GetDeckData(OpponentPlayerColor);
                            SendMessage(string.Format("{0}|{1}|{2}", "[OPPONENT DATA BLUE]", OpponentPlayerName, OpponentDeckInfo), Opponetclient);
                        }
                        break;
                }

                //Update the match logs at the end of each iteration
                UpdateMatchLogsDiplay(ActiveMatch);
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
        private static void UpdateConnectionLog(string message)
        {
            txtStaticConnectionLog.AppendText(message);
        }
        private void btnStart_Click(object sender, EventArgs e)
        {
            btnStart.Visible = false;
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
            txtStaticConnectionLog.Clear();
            waitingforclients.Suspend();
            btnStart.Visible = true;
        }
        private void listMatches_SelectedIndexChanged(object sender, EventArgs e)
        {
            int indexSelected = listMatches.SelectedIndex + 1;
            Match selectedMatch = list_Matches[indexSelected];
            txtMatchOutput.Clear();
            txtMatchOutput.Text = selectedMatch.GetLogs();
        }
        private static void UpdateMatchLogsDiplay(Match activeMatch)
        {
            txtStaticMatchOutput.Clear();
            txtStaticMatchOutput.Text = activeMatch.GetLogs();
        }
    }
}