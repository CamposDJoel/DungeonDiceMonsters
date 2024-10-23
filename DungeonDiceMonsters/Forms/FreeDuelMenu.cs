//Joel Campos
//9/10/2023
//FreeDuelMenu Class

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class FreeDuelMenu : Form
    {
        #region Constructors
        public FreeDuelMenu()
        {
            SoundServer.PlayBackgroundMusic(Song.FreeDuelMenu);
            InitializeComponent();

            //Set the arrays for the pictures and borders
            Borders[0] = PicBorder0; Borders[10] = PicBorder10; Borders[20] = PicBorder20;
            Borders[1] = PicBorder1; Borders[11] = PicBorder11; Borders[21] = PicBorder21;
            Borders[2] = PicBorder2; Borders[12] = PicBorder12; Borders[22] = PicBorder22;
            Borders[3] = PicBorder3; Borders[13] = PicBorder13; Borders[23] = PicBorder23;
            Borders[4] = PicBorder4; Borders[14] = PicBorder14; Borders[24] = PicBorder24;
            Borders[5] = PicBorder5; Borders[15] = PicBorder15; Borders[25] = PicBorder25;
            Borders[6] = PicBorder6; Borders[16] = PicBorder16; Borders[26] = PicBorder26;
            Borders[7] = PicBorder7; Borders[17] = PicBorder17; Borders[27] = PicBorder27;
            Borders[8] = PicBorder8; Borders[18] = PicBorder18; Borders[28] = PicBorder28;
            Borders[9] = PicBorder9; Borders[19] = PicBorder19; Borders[29] = PicBorder29;
            Borders[30] = PicBorder30; Borders[31] = PicBorder31; Borders[32] = PicBorder32;
            Borders[33] = PicBorder33; Borders[34] = PicBorder34;

            Characters[0] = PicChar0; Characters[12]= PicChar12; Characters[24] = PicChar24;
            Characters[1] = PicChar1; Characters[13]= PicChar13; Characters[25] = PicChar25;
            Characters[2] = PicChar2; Characters[14]= PicChar14; Characters[26] = PicChar26;
            Characters[3] = PicChar3; Characters[15]= PicChar15; Characters[27] = PicChar27;
            Characters[4] = PicChar4; Characters[16]= PicChar16; Characters[28] = PicChar28;
            Characters[5] = PicChar5; Characters[17]= PicChar17; Characters[29] = PicChar29;
            Characters[6] = PicChar6; Characters[18]= PicChar18; Characters[30] = PicChar30;
            Characters[7] = PicChar7; Characters[19]= PicChar19; Characters[31] = PicChar31;
            Characters[8] = PicChar8; Characters[20]= PicChar20; Characters[32] = PicChar32;
            Characters[9] = PicChar9; Characters[21]= PicChar21; Characters[33] = PicChar33;
            Characters[10] = PicChar10; Characters[22]= PicChar22; Characters[34] = PicChar34;
            Characters[11] = PicChar11; Characters[23]= PicChar23;

            //Only Enable Duel Master K as defaul
            GameData.UnlockCharacter(Character.Duel_Master_K);

            //Load all the unlocked characters
            for (int x = 0; x < Characters.Length; x++)
            {
                if (GameData.IsCharacterUnlocked((Character)x))
                {
                    Borders[x].Visible = true;
                    ImageServer.ClearImage(Characters[x]);
                    Characters[x].Image = ImageServer.CharacterIcon(((Character)x).ToString());
                    Characters[x].Visible = true;

                    Characters[x].MouseEnter += OnMouseEnterPicture;
                    Characters[x].MouseLeave += OnMouseLeavePicture;
                    Characters[x].Click += CharacterPicture_Click;
                }
            }
        }
        #endregion

        #region Data
        private PictureBox[] Borders = new PictureBox[35];
        private PictureBox[] Characters = new PictureBox[35];
        private int _CurrentSelectedCharacterID = 0;
        #endregion

        #region Events
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void OnMouseEnterPicture(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            PictureBox thisPicture = (PictureBox)sender;
            int id = Convert.ToInt32(thisPicture.Tag);


            Borders[id].BackColor = Color.Yellow;
            _CurrentSelectedCharacterID = id;

            //Display the character info
            lblCharacterName.Text = GameData.CharacterName((Character)id);
            lblWins.Text = GameData.GetCharacterWins((Character)id).ToString();
            lblLoss.Text = GameData.GetCharacterLoss((Character)id).ToString();
            lblCharacterName.Visible = true;
            lblWins.Visible = true;
            lblLoss.Visible = true;
        }
        private void OnMouseLeavePicture(object sender, EventArgs e)
        {
            PictureBox thisPicture = (PictureBox)sender;
            int id = Convert.ToInt32(thisPicture.Tag);


            Borders[id].BackColor = Color.Transparent;

            lblCharacterName.Visible = false;
            lblWins.Visible = false;
            lblLoss.Visible = false;
        }
        private void CharacterPicture_Click(object sender, EventArgs e)
        {
            Character thisCharacter = (Character)_CurrentSelectedCharacterID;
            string characterName = GameData.CharacterName(thisCharacter);

            //TODO: Start a duel with this character
            SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            /*
            //Generate the Player Objects
            PlayerData red = new PlayerData("Player", "1", "1", DecksData.GetDeckAtIndex(0).GetCopy());
            PlayerData blue = new PlayerData(characterName, "1", "1", DecksData.GetDeckAtIndex(0).GetCopy());

            BoardForm BF = new BoardForm(red, blue);
            //BoardForm BF = new BoardForm(red, blue, true);
            Dispose();
            BF.Show();*/
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
