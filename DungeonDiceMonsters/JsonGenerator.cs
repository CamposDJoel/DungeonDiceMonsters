using Microsoft.Win32;
using Newtonsoft.Json;
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
    public partial class JsonGenerator : Form
    {
        public JsonGenerator()
        {
            InitializeComponent();
            ReloadDBList();
        }

        public void ReloadDBList()
        {
            listCardList.Items.Clear();

            foreach (rawcardinfo rawcardinfo in CardDataBase.rawCardList)
            {
                string cardid = rawcardinfo.id.ToString();
                if (rawcardinfo.id < 10) { cardid = "000" + cardid; }
                else if (rawcardinfo.id < 100) { cardid = "00" + cardid; }
                else if (rawcardinfo.id < 1000) { cardid = "0" + cardid; }

                listCardList.Items.Add(cardid + "-" + rawcardinfo.name);
            }
            listCardList.SetSelected(0, true);
        }

        private void listCardList_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Extract the card info of the selected item
            int index = listCardList.SelectedIndex;
            rawcardinfo cardinfo = CardDataBase.rawCardList[index];

            //Update UI with the data of the current card
            numID.Value = cardinfo.id;
            txtCardName.Text = cardinfo.name;
            numLevel.Value = cardinfo.level;
            txtATK.Text = cardinfo.atk.ToString();
            txtDef.Text = cardinfo.def.ToString();
            txtLp.Text = cardinfo.lp.ToString();
            int indexofCat = listCategory.FindString(cardinfo.category);
            listCategory.SetSelected(indexofCat, true);
            int indexofAtt = listAttribute.FindString(cardinfo.attribute);
            listAttribute.SetSelected(indexofAtt, true);
            int indexofType = listType.FindString(cardinfo.type);
            listType.SetSelected(indexofType, true);
            txtCardText.Text = cardinfo.cardtext.ToString();
            int indexofSet = listSet.FindString(cardinfo.setpack);
            listSet.SetSelected(indexofSet, true);
            int indexofRarity = listRarity.FindString(cardinfo.rarity);
            listRarity.SetSelected(indexofRarity, true);
            checkIsFusion.Checked = cardinfo.fusion;

            //Update the UI of the Dice Info
            numDiceLevel.Value = Convert.ToInt32(cardinfo.diceinforaw[0].level);
            int indexofFace1Crest = listFace1Crest.FindString(cardinfo.diceinforaw[0].crest1);
            int indexofFace2Crest = listFace2Crest.FindString(cardinfo.diceinforaw[0].crest2);
            int indexofFace3Crest = listFace3Crest.FindString(cardinfo.diceinforaw[0].crest3);
            int indexofFace4Crest = listFace4Crest.FindString(cardinfo.diceinforaw[0].crest4);
            int indexofFace5Crest = listFace5Crest.FindString(cardinfo.diceinforaw[0].crest5);
            int indexofFace6Crest = listFace6Crest.FindString(cardinfo.diceinforaw[0].crest6);

            int indexofFace1Value = listFace1Value.FindString(cardinfo.diceinforaw[0].value1);
            int indexofFace2Value = listFace2Value.FindString(cardinfo.diceinforaw[0].value2);
            int indexofFace3Value = listFace3Value.FindString(cardinfo.diceinforaw[0].value3);
            int indexofFace4Value = listFace4Value.FindString(cardinfo.diceinforaw[0].value4);
            int indexofFace5Value = listFace5Value.FindString(cardinfo.diceinforaw[0].value5);
            int indexofFace6Value = listFace6Value.FindString(cardinfo.diceinforaw[0].value6);

            listFace1Crest.SetSelected(indexofFace1Crest, true);
            listFace2Crest.SetSelected(indexofFace2Crest, true);
            listFace3Crest.SetSelected(indexofFace3Crest, true);
            listFace4Crest.SetSelected(indexofFace4Crest, true);
            listFace5Crest.SetSelected(indexofFace5Crest, true);
            listFace6Crest.SetSelected(indexofFace6Crest, true);

            listFace1Value.SetSelected(indexofFace1Value, true);
            listFace2Value.SetSelected(indexofFace2Value, true);
            listFace3Value.SetSelected(indexofFace3Value, true);
            listFace4Value.SetSelected(indexofFace4Value, true);
            listFace5Value.SetSelected(indexofFace5Value, true);
            listFace6Value.SetSelected(indexofFace6Value, true);
        }

        private void btnAddCard_Click(object sender, EventArgs e)
        {
            //Gather all the data to create a new rawcardinfo object
            string id = numID.Value.ToString();
            string name = txtCardName.Text;
            string level = numLevel.Value.ToString();
            string category = listCategory.SelectedItem.ToString();
            string attribute = listAttribute.SelectedItem.ToString();
            string type = listType.SelectedItem.ToString();
            string atk = txtATK.Text.ToString();
            string def = txtDef.Text.ToString();
            string lp = txtLp.Text.ToString();
            string cardtext = txtCardText.Text;
            string setPack = listSet.SelectedItem.ToString();
            string rarity = listRarity.SelectedItem.ToString();
            string isFusion = checkIsFusion.Checked.ToString();
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
            newcard.id = Convert.ToInt32(id);
            newcard.name = name;
            newcard.level = Convert.ToInt32(level);
            newcard.category = category;
            newcard.attribute = attribute;
            newcard.type = type;
            newcard.atk = Convert.ToInt32(atk);
            newcard.def = Convert.ToInt32(def);
            newcard.lp = Convert.ToInt32(lp);
            newcard.cardtext = cardtext;
            newcard.setpack = setPack;
            newcard.rarity = rarity;
            newcard.fusion = Convert.ToBoolean(isFusion);
            newcard.diceinforaw.Add(new rawdiceinfo(diceLevel, face1Crest, face2Crest, face3Crest, face4Crest, face5Crest, face6Crest,
                face1Value, face2Value, face3Value, face4Value, face5Value, face6Value));

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
            string id = numID.Value.ToString();
            string name = txtCardName.Text;
            string level = numLevel.Value.ToString();
            string category = listCategory.SelectedItem.ToString();
            string attribute = listAttribute.SelectedItem.ToString();
            string type = listType.SelectedItem.ToString();
            string atk = txtATK.Text.ToString();
            string def = txtDef.Text.ToString();
            string lp = txtLp.Text.ToString();
            string cardtext = txtCardText.Text;
            string setPack = listSet.SelectedItem.ToString();
            string rarity = listRarity.SelectedItem.ToString();
            string isFusion = checkIsFusion.Checked.ToString();
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
            cardinfo.id = Convert.ToInt32(id);
            cardinfo.name = name;
            cardinfo.level = Convert.ToInt32(level);
            cardinfo.category = category;
            cardinfo.attribute = attribute;
            cardinfo.type = type;
            cardinfo.atk = Convert.ToInt32(atk);
            cardinfo.def = Convert.ToInt32(def);
            cardinfo.lp = Convert.ToInt32(lp);
            cardinfo.cardtext = cardtext;
            cardinfo.setpack = setPack;
            cardinfo.rarity = rarity;
            cardinfo.fusion = Convert.ToBoolean(isFusion);
            cardinfo.diceinforaw.Clear();
            cardinfo.diceinforaw.Add(new rawdiceinfo(diceLevel, face1Crest, face2Crest, face3Crest, face4Crest, face5Crest, face6Crest,
                face1Value, face2Value, face3Value, face4Value, face5Value, face6Value));

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
    }
}
