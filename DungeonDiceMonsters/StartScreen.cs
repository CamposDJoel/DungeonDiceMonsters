﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class StartScreen : Form
    {
        public StartScreen()
        {
            SoundServer.PlayBackgroundMusic(Song.TitleScreen, true);
            InitializeComponent();

            btnNewGame.MouseEnter += OnMouseEnterLabel;
            btnNewGame.MouseLeave += OnMouseLeaveLabel;
        }

        private void btnOpenDBManager_Click(object sender, EventArgs e)
        {
            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.Show();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Initialize the DB Read raw data from json file
            string jsonFilePath = Directory.GetCurrentDirectory() + "\\DB\\CardListDB.json";
            string rawdata = File.ReadAllText(jsonFilePath);
            CardDataBase.rawCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<rawcardinfo>>(rawdata);

            //convert thre raw data base into the actual (and Clean) CardInfo object list
            CardDataBase.CardList = new List<CardInfo>();
            foreach (rawcardinfo rawcardinfo in CardDataBase.rawCardList)
            {
                CardDataBase.CardList.Add(new CardInfo(rawcardinfo));
            }

            //Show the new game button
            btnStartGame.Visible = false;
            btnNewGame.Visible = true;

            //Check if there is a save file to show the load game
            try
            {
                StreamReader SR_SaveFile = new StreamReader(
                 Directory.GetCurrentDirectory() + "\\Save Files\\SaveFile.txt");
                SR_SaveFile.Close();

                btnLoadGame.Visible = true;
                btnNewGame.Size = new Size(200, 60);
                btnNewGame.Location = new Point(270, 335);
            }
            catch (Exception)
            {
                //do nothing continue
            }
        }

        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
            if(btnLoadGame.Visible == true)
            {
                lblWarning.Visible = true;
            }         
        }
        private void OnMouseLeaveLabel(object sender, EventArgs e)
        {
            lblWarning.Visible = false;
        }

        private void btnLoadGame_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //Read the savefile to load the decks and storage contents and the rest
            SaveFileManger.ReadSaveFile();

            //Open the main menu form
            SoundServer.PlayBackgroundMusic(Song.TitleScreen, false);
            MainMenu MM = new MainMenu();
            Hide();
            MM.Show();
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            //To start a new game, give the player the starter deck and a sample card in storage
            DecksData.Decks.Add(new Deck("Starter Deck"));

            DecksData.Decks[0].AddMainCard(1);
            DecksData.Decks[0].AddMainCard(1);
            DecksData.Decks[0].AddMainCard(1);
            DecksData.Decks[0].AddMainCard(4);
            DecksData.Decks[0].AddMainCard(4);
            DecksData.Decks[0].AddMainCard(4);
            DecksData.Decks[0].AddMainCard(5);
            DecksData.Decks[0].AddMainCard(5);
            DecksData.Decks[0].AddMainCard(5);
            DecksData.Decks[0].AddMainCard(7);
            DecksData.Decks[0].AddMainCard(7);
            DecksData.Decks[0].AddMainCard(7);
            DecksData.Decks[0].AddMainCard(8);
            DecksData.Decks[0].AddMainCard(8);
            DecksData.Decks[0].AddMainCard(9);
            DecksData.Decks[0].AddMainCard(9);
            DecksData.Decks[0].AddMainCard(11);
            DecksData.Decks[0].AddMainCard(11);
            DecksData.Decks[0].AddMainCard(12);
            DecksData.Decks[0].AddMainCard(12);

            StorageData.AddCard(3);
            StorageData.AddCard(6);

            //Open the main menu form
            SoundServer.PlayBackgroundMusic(Song.TitleScreen, false);
            MainMenu MM = new MainMenu();
            Hide();
            MM.Show();
        }
    }
}
