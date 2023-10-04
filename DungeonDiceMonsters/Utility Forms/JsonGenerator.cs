//Joel Campos
//9/8/2023
//JSONGenerator Class

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class JsonGenerator : Form
    {
        public JsonGenerator()
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



            ReloadDBList();

            //loadNewDB();
        }

        private void loadNewDB()
        {
            StreamReader SR_SaveFile = new StreamReader(
                Directory.GetCurrentDirectory() + "\\DB\\dbrawtxt.txt");

            //String that hold the data of one line of the txt file
            string line = SR_SaveFile.ReadLine();
            int lineCount = Convert.ToInt32(line);

            for(int x = 0; x < lineCount; x++)
            {
                line = SR_SaveFile.ReadLine();
                //rawcardlist.Add(new rawcardinfo(line));
            }

            //writeout the json
            string output = JsonConvert.SerializeObject(rawcardlist);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DB\\rawDBver2.json", output);

            //Save the url of this card is missing an image
            List<string> missingcardurls = new List<string>();
            for (int x = 0; x < rawcardlist.Count; x++)
            {
                string cardid = rawcardlist[x].id;
                if (!File.Exists(Directory.GetCurrentDirectory() + "\\images\\artwork\\" + cardid+ ".jpg")) { missingcardurls.Add("https://images.ygoprodeck.com/images/cards_cropped/" + cardid + ".jpg"); }
            }
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\DB\\missingartworkurls.txt", missingcardurls);
        }

        private List<rawcardinfo> rawcardlist = new List<rawcardinfo>();

        public void ReloadDBList()
        {
            listCardList.Items.Clear();

            foreach (rawcardinfo rawcardinfo in CardDataBase.rawCardList)
            {
                listCardList.Items.Add(rawcardinfo.name);
            }
            listCardList.SetSelected(0, true);
        }

        private void listCardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Extract the card info of the selected item
            int index = listCardList.SelectedIndex;
            rawcardinfo cardinfo = CardDataBase.rawCardList[index];

            //Update UI with the data of the current card
            numID.Text = cardinfo.id;
            txtCardName.Text = cardinfo.name;
            numLevel.Value = Convert.ToInt32(cardinfo.monsterLevel);
            txtATK.Text = cardinfo.atk.ToString();
            txtDef.Text = cardinfo.def.ToString();
            txtLp.Text = cardinfo.lp.ToString();
            int indexofCat = listCategory.FindString(cardinfo.category);
            listCategory.SetSelected(indexofCat, true);
            int indexofAtt = listAttribute.FindString(cardinfo.attribute);
            listAttribute.SetSelected(indexofAtt, true);
            int indexofType = listType.FindString(cardinfo.type);
            listType.SetSelected(indexofType, true);
            int indexofSecType = listSecType.FindString(cardinfo.sectype);
            listSecType.SetSelected(indexofSecType, true);
            //effects
            txtOnSumon.Text = cardinfo.onSummonEffect.ToString();
            txtContiEffect.Text = cardinfo.continuousEffect.ToString();
            txtIgnitionEffect.Text = cardinfo.ignitionEffect.ToString();
            txtAbility.Text = cardinfo.ability.ToString();

            //Update the UI of the Dice Info
            numDiceLevel.Value = Convert.ToInt32(cardinfo.diceLevel);

            //Face 1
            string face1 = cardinfo.face1;
            string face1Crest = face1.Split(' ')[0];
            string face1value = face1.Split(' ')[1];
            int indexofFace1Crest = listFace1Crest.FindString(face1Crest);
            int indexofFace1Value = listFace1Value.FindString(face1value);
            listFace1Crest.SetSelected(indexofFace1Crest, true);
            listFace1Value.SetSelected(indexofFace1Value, true);

            //Face 2
            string face2 = cardinfo.face2;
            string face2Crest = face2.Split(' ')[0];
            string face2value = face2.Split(' ')[1];
            int indexofFace2Crest = listFace2Crest.FindString(face2Crest);
            int indexofFace2Value = listFace2Value.FindString(face2value);
            listFace2Crest.SetSelected(indexofFace2Crest, true);
            listFace2Value.SetSelected(indexofFace2Value, true);

            //Face 3
            string face3 = cardinfo.face3;
            string face3Crest = face3.Split(' ')[0];
            string face3value = face3.Split(' ')[1];
            int indexofFace3Crest = listFace3Crest.FindString(face3Crest);
            int indexofFace3Value = listFace3Value.FindString(face3value);
            listFace3Crest.SetSelected(indexofFace3Crest, true);
            listFace3Value.SetSelected(indexofFace3Value, true);

            //Face 4
            string face4 = cardinfo.face4;
            string face4Crest = face4.Split(' ')[0];
            string face4value = face4.Split(' ')[1];
            int indexofFace4Crest = listFace4Crest.FindString(face4Crest);
            int indexofFace4Value = listFace4Value.FindString(face4value);
            listFace4Crest.SetSelected(indexofFace4Crest, true);
            listFace4Value.SetSelected(indexofFace4Value, true);

            //Face 5
            string face5 = cardinfo.face5;
            string face5Crest = face5.Split(' ')[0];
            string face5value = face5.Split(' ')[1];
            int indexofFace5Crest = listFace5Crest.FindString(face5Crest);
            int indexofFace5Value = listFace5Value.FindString(face5value);
            listFace5Crest.SetSelected(indexofFace5Crest, true);
            listFace5Value.SetSelected(indexofFace5Value, true);

            //Face 6
            string face6 = cardinfo.face6;
            string face6Crest = face6.Split(' ')[0];
            string face6value = face6.Split(' ')[1];
            int indexofFace6Crest = listFace6Crest.FindString(face6Crest);
            int indexofFace6Value = listFace6Value.FindString(face6value);
            listFace6Crest.SetSelected(indexofFace6Crest, true);
            listFace6Value.SetSelected(indexofFace6Value, true);
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            //Gather all the data to create a new rawcardinfo object
            string id = numID.Text;
            string name = txtCardName.Text;
            string level = numLevel.Value.ToString();
            string category = listCategory.SelectedItem.ToString();
            string attribute = listAttribute.SelectedItem.ToString();
            string type = listType.SelectedItem.ToString();
            string secType = listSecType.SelectedItem.ToString();
            string atk = txtATK.Text.ToString();
            string def = txtDef.Text.ToString();
            string lp = txtLp.Text.ToString();
            //effects
            string onSummonEffect = txtOnSumon.Text.ToString();
            string contiEfect = txtContiEffect.Text.ToString();
            string ignitionEffect = txtIgnitionEffect.Text.ToString();
            string ability = txtAbility.Text.ToString();
            //Dice info
            string diceLevel = numDiceLevel.Value.ToString();
            string face1Crest = listFace1Crest.Text.ToString();
            string face2Crest = listFace2Crest.Text.ToString();
            string face3Crest = listFace3Crest.Text.ToString();
            string face4Crest = listFace4Crest.Text.ToString();
            string face5Crest = listFace5Crest.Text.ToString();
            string face6Crest = listFace6Crest.Text.ToString();
            string face1Value = listFace1Value.Text.ToString();
            string face2Value = listFace2Value.Text.ToString();
            string face3Value = listFace3Value.Text.ToString();
            string face4Value = listFace4Value.Text.ToString();
            string face5Value = listFace5Value.Text.ToString();
            string face6Value = listFace6Value.Text.ToString();

            
            //add a new rawcardinfo instance to the DB list based on the data above.
            rawcardinfo newcard = new rawcardinfo();
            newcard.id = id;
            newcard.name = name;
            newcard.monsterLevel = level;
            newcard.category = category;
            newcard.attribute = attribute;
            newcard.type = type;
            newcard.sectype = secType;
            newcard.monsterLevel = atk;
            newcard.def = def;
            newcard.lp = lp;
            //TODO EFECTS
            newcard.onSummonEffect = onSummonEffect;
            newcard.continuousEffect = contiEfect;
            newcard.ignitionEffect = ignitionEffect;
            newcard.ability = ability;
            newcard.diceLevel = diceLevel;
            newcard.face1 = face1Crest + " " + face1Value;
            newcard.face2 = face2Crest + " " + face2Value;
            newcard.face3 = face3Crest + " " + face3Value;
            newcard.face4 = face4Crest + " " + face4Value;
            newcard.face5 = face5Crest + " " + face5Value;
            newcard.face6 = face6Crest + " " + face6Value;

            CardDataBase.rawCardList.Add(newcard);

            //Override the JSON file based on the new rawlist
            string output = JsonConvert.SerializeObject(CardDataBase.rawCardList);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DB\\CardListDB.json", output);

            //Reload the DB list
            ReloadDBList();
        }

        private void btnEditSelected_Click(object sender, EventArgs e)
        {
            //Extract the card info of the selected item
            int index = listCardList.SelectedIndex;
            rawcardinfo cardinfo = CardDataBase.rawCardList[index];

            //Override the info with the current set in the UI
            string id = numID.Text;
            string name = txtCardName.Text;
            string level = numLevel.Value.ToString();
            string category = listCategory.SelectedItem.ToString();
            string attribute = listAttribute.SelectedItem.ToString();
            string type = listType.SelectedItem.ToString();
            string secType = listSecType.SelectedItem.ToString();
            string atk = txtATK.Text.ToString();
            string def = txtDef.Text.ToString();
            string lp = txtLp.Text.ToString();
            //effects
            string onSummonEffect = txtOnSumon.Text.ToString();
            string contiEfect = txtContiEffect.Text.ToString();
            string ignitionEffect = txtIgnitionEffect.Text.ToString();
            string ability = txtAbility.Text.ToString();
            //Dice info
            string diceLevel = numDiceLevel.Value.ToString();
            string face1Crest = listFace1Crest.Text.ToString();
            string face2Crest = listFace2Crest.Text.ToString();
            string face3Crest = listFace3Crest.Text.ToString();
            string face4Crest = listFace4Crest.Text.ToString();
            string face5Crest = listFace5Crest.Text.ToString();
            string face6Crest = listFace6Crest.Text.ToString();
            string face1Value = listFace1Value.Text.ToString();
            string face2Value = listFace2Value.Text.ToString();
            string face3Value = listFace3Value.Text.ToString();
            string face4Value = listFace4Value.Text.ToString();
            string face5Value = listFace5Value.Text.ToString();
            string face6Value = listFace6Value.Text.ToString();

            //override the data
            cardinfo.id = id;
            cardinfo.name = name;
            cardinfo.monsterLevel = level;
            cardinfo.category = category;
            cardinfo.attribute = attribute;
            cardinfo.type = type;
            cardinfo.sectype = secType;
            cardinfo.atk = atk;
            cardinfo.def = def;
            cardinfo.lp = lp;
            //EFECTS
            cardinfo.onSummonEffect = onSummonEffect;
            cardinfo.continuousEffect = contiEfect;
            cardinfo.ignitionEffect = ignitionEffect;
            cardinfo.ability = ability;
            //Dice info
            cardinfo.diceLevel = diceLevel;
            cardinfo.face1 = face1Crest + " " + face1Value;
            cardinfo.face2 = face2Crest + " " + face2Value;
            cardinfo.face3 = face3Crest + " " + face3Value;
            cardinfo.face4 = face4Crest + " " + face4Value;
            cardinfo.face5 = face5Crest + " " + face5Value;
            cardinfo.face6 = face6Crest + " " + face6Value;

            //Override the JSON file based on the new rawlist
            string output = JsonConvert.SerializeObject(CardDataBase.rawCardList);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DB\\CardListDB.json", output);

            //Reload the DB list
            ReloadDBList();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //Extract the index of the selected item
            int index = listCardList.SelectedIndex;

            //remove card from the database
            CardDataBase.rawCardList.RemoveAt(index);

            //Override the JSON file based on the new rawlist
            string output = JsonConvert.SerializeObject(CardDataBase.rawCardList);
            File.WriteAllText(Directory.GetCurrentDirectory() + "\\DB\\CardListDB.json", output);

            //Reload the DB list
            ReloadDBList();
        }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
