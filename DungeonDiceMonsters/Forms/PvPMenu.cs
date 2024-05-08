//Joel Campos
//4/2/24
//PvPMenu Form

using System;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace DungeonDiceMonsters
{
    public partial class PvPMenu : Form
    {
        #region Constructors
        public PvPMenu()
        {
            InitializeComponent();
            listDeckList.SetSelected(0, true);
        }
        #endregion

        #region Private Data
        private NetworkStream ns;
        private static PvPMenu StaticPvPMenu;
        private Deck _CurrentDeckSelected;
        private string _OpponentName;
        private Deck _OpponentsDeck;
        private PlayerColor MyColor;
        private BoardPvP _CurrentBoardPVP;
        Thread _Thread;
        #endregion

        #region Event Listeners
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuelMenu, false);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void btnFindMatch_Click(object sender, EventArgs e)
        {
            StaticPvPMenu = this;
            Enabled = false;
            PanelDeckSelection.Visible = false;
            lblWaitMessage.Visible = false;
            //Connect
            btnExit.Visible = false;
            btnFindMatch.Visible = false;
            IPAddress ip = IPAddress.Parse("127.0.0.1");
            int port = 5000;
            TcpClient client = new TcpClient();

            try
            {
                client.Connect(ip, port);
                ns = client.GetStream();
                _Thread = new Thread(o => ReceiveData((TcpClient)o));
                _Thread.Start(client);

                //After connecting send your player name/deck to the server
                string deckdata = _CurrentDeckSelected.GetDataStringLineForPVP();
                SendData(string.Format("{0}|{1}|{2}", "[MC PLAYER INFO]", GameData.Name, deckdata));
            }
            catch
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                Enabled = true;
                lblWaitMessage.Text = "Server unavailable, please try again later.";
                lblWaitMessage.Visible = true;
                BoardForm.WaitNSeconds(2000);
                PanelDeckSelection.Visible = true;
                lblWaitMessage.Visible = false;
                btnExit.Visible = true;
                btnFindMatch.Visible = true;
            }

        }
        private void listDeckList_SelectedIndexChanged(object sender, EventArgs e)
        {
            int _CurrentDeckIndexSelected = listDeckList.SelectedIndex;
            _CurrentDeckSelected = DecksData.Decks[_CurrentDeckIndexSelected];
            bool DeckIsReadyToUse = _CurrentDeckSelected.UseStatus;

            //Set the Ready flag
            ImageServer.LoadImage(PicDeckStatus, CardImageType.DeckStatusIcon, DeckIsReadyToUse.ToString());

            if (DeckIsReadyToUse)
            {
                btnFindMatch.Enabled = true;
            }
            else
            {
                btnFindMatch.Enabled = false;
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion

        #region Public Methods
        public void MessageReceived(string DATARECEIVED)
        {
            //Step 1: Extract the Message Keys
            string[] MessageTokens = DATARECEIVED.Split('|');
            string MessageKey = MessageTokens[0];

            //Step 3: Handle the message
            switch (MessageKey)
            {
                case "[WAITING FOR PLAYER 2]":
                    //Set the User as Player 1 (RED) - not player 2 has join thus only diplay user player info in
                    //the red area
                    Invoke(new MethodInvoker(delegate ()
                    {
                        lblRedPlayerName.Text = "Red Player: " + GameData.Name;
                        lblRedPlayerName.Visible = true;
                        MyColor = PlayerColor.RED;
                        lblWaitMessage.Text = "Waiting for Player 2!";
                        lblWaitMessage.Visible = true;
                    }));
                    break;
                case "[OPPONENT DATA RED]":
                    //Data for Player 1 (RED) has been recevied, that means user is player 2 (BLUE) and the match is 
                    //ready to go, diplay opponent's data in the Red Section and user data in the Blue section.
                    Invoke(new MethodInvoker(delegate ()
                    {
                        _OpponentName = MessageTokens[1];
                        _OpponentsDeck = new Deck();
                        _OpponentsDeck.InitializeFromPVPData(MessageTokens[2]);
                        lblBluePlayerName.Text = "Blue Player: " + GameData.Name;
                        lblRedPlayerName.Text = "Red Player: " + _OpponentName;
                        MyColor = PlayerColor.BLUE;
                        lblBluePlayerName.Visible = true;
                        lblRedPlayerName.Visible = true;
                        lblWaitMessage.Text = "Match found! - Starting Game...";
                        lblWaitMessage.Visible = true;
                        StartMatch();
                    }));
                    break;
                case "[OPPONENT DATA BLUE]":
                    //This is the follow up to "Waiting for Player 2" message key
                    //Data for Player 2 (BLUE) has been received, that means user was already in waiting mode
                    //and the User data is already displayed in the red area. Diplay the opponent info in the blue
                    //are and match is ready to go.
                    Invoke(new MethodInvoker(delegate ()
                    {
                        _OpponentName = MessageTokens[1];
                        _OpponentsDeck = new Deck();
                        _OpponentsDeck = new Deck();
                        _OpponentsDeck.InitializeFromPVPData(MessageTokens[2]);
                        lblBluePlayerName.Text = "Blue Player: " + _OpponentName;
                        lblBluePlayerName.Visible = true;
                        MyColor = PlayerColor.RED;
                        lblWaitMessage.Text = "Match found! - Starting Game...";
                        StartMatch();
                    }));
                    break;
                case "[OPPONENT DISCONNECT]":
                    //Close the BoardPVP form (this will also close the RollDiceMenu if it was open as well)
                    Invoke(new MethodInvoker(delegate ()
                    {
                        _CurrentBoardPVP.Dispose();
                        lblWaitMessage.Text = "Opponent disconnected, Match ENDED.";
                        lblBluePlayerName.Visible = false;
                        lblRedPlayerName.Visible = false;
                        PanelDeckSelection.Visible = true;
                        lblWaitMessage.Visible = true;
                        btnExit.Visible = true;
                        btnFindMatch.Visible = true;
                        Show();
                    }));               
                    break;
                default:
                    //ANY OTHER MESSAGE WILL BE FORWARDED TO THE BoardPVP
                    _CurrentBoardPVP.ReceiveMesageFromServer(DATARECEIVED);
                    break;
            }
        }
        #endregion

        #region Private Methods
        private void StartMatch()
        {
            //Start by having a 3 sec delay to pace of opening the baord form
            BoardForm.WaitNSeconds(3000);

            //Then Generate the PlayerData objects from each player to launch the 
            PlayerData user = new PlayerData(GameData.Name, _CurrentDeckSelected);
            PlayerData opponent = new PlayerData(_OpponentName, _OpponentsDeck);

            if (MyColor == PlayerColor.RED)
            {
                //_CurrentBoardPVP = new BoardPvP(user, opponent, MyColor, ns);
                _CurrentBoardPVP = new BoardPvP(user, opponent, MyColor, ns, true);
                Hide();
                Enabled = true;
                PanelDeckSelection.Visible = true;
                lblWaitMessage.Visible = true;
                btnExit.Visible = true;
                btnFindMatch.Visible = true;
                _CurrentBoardPVP.Show();
            }
            else
            {
                //_CurrentBoardPVP = new BoardPvP(opponent, user, MyColor, ns);
                _CurrentBoardPVP = new BoardPvP(opponent, user, MyColor, ns, true);
                Hide();
                Enabled = true;
                PanelDeckSelection.Visible = true;
                lblWaitMessage.Visible = true;
                btnExit.Visible = true;
                btnFindMatch.Visible = true;
                _CurrentBoardPVP.Show();
            }
        }
        #endregion

        #region TCP CLIENT Methods
        static void ReceiveData(TcpClient client)
        {
            NetworkStream ns = client.GetStream();
            byte[] receivedBytes = new byte[1024];
            int byte_count;

            while ((byte_count = ns.Read(receivedBytes, 0, receivedBytes.Length)) > 0)
            {
                //Set the Data Received and send it thur the non-static method
                //to the active instance of the PvPMenu form to be processed.
                string DATARECEIVED = Encoding.ASCII.GetString(receivedBytes, 0, byte_count);
                StaticPvPMenu.MessageReceived(DATARECEIVED);

                //If the Data received was the opponent disconnect notification, end the loop to disconnect client
                if(DATARECEIVED == "[OPPONENT DISCONNECT]") 
                {
                    break;
                }
            }

            //Disconect
            ns.Close();
            client.Dispose();
        }
        private void SendData(string message)
        {
            byte[] buffer = Encoding.ASCII.GetBytes(message);
            ns.Write(buffer, 0, buffer.Length);
        }
        #endregion
    }
}

        