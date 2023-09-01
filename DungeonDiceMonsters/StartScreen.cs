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
            List<rawcardinfo> _RawCardList = Newtonsoft.Json.JsonConvert.DeserializeObject<List<rawcardinfo>>(rawdata);

            //convert thre raw data base into the actual (and Clean) CardInfo object list
            List<CardInfo> CardDataBase = new List<CardInfo>();
            foreach (rawcardinfo rawcardinfo in _RawCardList)
            {
                CardDataBase.Add(new CardInfo(rawcardinfo));
            }          
        }
    }
}
