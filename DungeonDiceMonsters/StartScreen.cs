using System;
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
            InitializeComponent();

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
        }

        private void btnOpenDBManager_Click(object sender, EventArgs e)
        {
            JsonGenerator jsonGenerator = new JsonGenerator();
            jsonGenerator.Show();
        }

        private void btnStartGame_Click(object sender, EventArgs e)
        {
            DeckBuilder DB = new DeckBuilder();
            Hide();
            DB.Show();
        }
    }
}
