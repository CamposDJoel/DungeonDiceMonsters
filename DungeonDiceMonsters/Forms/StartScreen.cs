//Joel Campos
//10/4/2023
//Start Screen Class

using System;
using System.Collections.Generic;
using System.Drawing.Drawing2D;
using System.IO;
using System.Net.NetworkInformation;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class StartScreen : Form
    {
        #region Constructors
        public StartScreen()
        {            
            InitializeComponent();
            SettingsData.InitializeSettings();
            SoundServer.PlayBackgroundMusic(Song.TitleScreen);

            RadioNewGameOption.MouseEnter += new EventHandler(MouseEnterRadioHover);            
            RadioLoadGameOption.MouseEnter += new EventHandler(MouseEnterRadioHover);
            RadioNewGameOption.Click += new EventHandler(ClickRadio);
            RadioLoadGameOption.Click += new EventHandler(ClickRadio);
        }
        #endregion

        private bool SaveFileExists = false;

        #region Events
        private void MouseEnterRadioHover(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        private void ClickRadio(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
        }
        private void btnOpenTestForm_Click(object sender, EventArgs e)
        {
            DiceSelectionAITest dtest = new DiceSelectionAITest();
            Hide();
            dtest.Show();
        }
        private void btnOpenDBManager_Click(object sender, EventArgs e)
        {
            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.Show();
        }
        private void btnStartGame_Click(object sender, EventArgs e)
        {
            //Hide the Start Game Button
            btnStartGame.Visible = false;

            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Initialize the DB Read raw data from json file
            string jsonFilePath = Directory.GetCurrentDirectory() + "\\DB\\CardListDB.json";
            string rawdata = File.ReadAllText(jsonFilePath);
            CardDataBase.rawCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<rawcardinfo>>(rawdata);

            //convert thre raw data base into the actual (and Clean) CardInfo object list
            foreach (rawcardinfo rawcardinfo in CardDataBase.rawCardList)
            {
                CardDataBase.AddCardToDB(new CardInfo(rawcardinfo));
            }

            //Check if a save file exists
            SaveFileExists = File.Exists(Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt");          
            if(SaveFileExists)
            {
                RadioLoadGameOption.Checked = true;
                //Preview the SaveFile Data
                StreamReader SR_SaveFile = new StreamReader(
                 Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt");

                string Line = SR_SaveFile.ReadLine();
                string[] tokens = Line.Split('|');
                lblPreviewPlayerName.Text = tokens[0];
                lblPreviewStarchips.Text = tokens[1];
                SR_SaveFile.Close();
                //Show the warning in the new game menu
                lblWarning.Visible = true;
            }
            else
            {
                RadioLoadGameOption.Visible = false;
            }

            //Show the New/Load Panel
            PanelSelectionA.Visible = true;
        }
        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Read the savefile to load the decks and storage contents and the rest
            SaveFileManger.ReadSaveFile();

            //Open the main menu form
            SoundServer.PlayBackgroundMusic(Song.TitleScreen);
            MainMenu MM = new MainMenu();
            Hide();
            MM.Show();
        }
        private void btnNewGame_Click(object sender, EventArgs e)
        {
            //Validate the New Player Input name
            string nameinput = txtPlayerName.Text;
            if (nameinput.Length == 0 || nameinput.Contains("|"))
            {
                //ERROR
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblInvalidNameBanner.Visible = true;
                BoardForm.WaitNSeconds(2000);
                lblInvalidNameBanner.Visible = false;
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //Initialize the Player name
                GameData.SetPlayerName(nameinput);
                int RandomCardID = CardDataBase.GetRandomCardID();
                //To start a new game, give the player the starter deck and a sample card in storage
                Deck StarterDeck = DecksData.GetStarterDeck();
                DecksData.AddDeck(StarterDeck);
                StorageData.AddCard(RandomCardID);
                //Mark all cards obtained for the library
                GameData.MarkDeckCardsAsLibraryObtained(StarterDeck);
                GameData.MarkLibraryCardObtainedbyID(RandomCardID);
                //Create the save file
                SaveFileManger.WriteSaveFile();

                //Open the main menu form
                MainMenu MM = new MainMenu();
                Hide();
                MM.Show();
            }          
        }
        private void RadioNewGameOption_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioNewGameOption.Checked)
            {
                RadioLoadGameOption.Checked = false;
                PanelNewGameOptions.Visible = true;
                PanelLoadGameOptions.Visible = false;
            }
        }
        private void RadioLoadGameOption_CheckedChanged(object sender, EventArgs e)
        {
            if (RadioLoadGameOption.Checked)
            {
                RadioNewGameOption.Checked = false;
                PanelNewGameOptions.Visible = false;
                PanelLoadGameOptions.Visible = true;
            }
        }
        #endregion
    }
}