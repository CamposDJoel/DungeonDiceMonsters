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
    public partial class DiceSelectionAITest : Form
    {
        public DiceSelectionAITest()
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

            //Deck Creation

            DecksData.Decks[0] = new Deck();
            DecksData.Decks[1] = new Deck();
            DecksData.Decks[2] = new Deck();

            //LV 1 cards
            DecksData.Decks[0].AddMainCard(83464209);
            DecksData.Decks[0].AddMainCard(92731455);
            DecksData.Decks[0].AddMainCard(71625222);

            //LV 2 cards
            DecksData.Decks[0].AddMainCard(76184692);
            DecksData.Decks[0].AddMainCard(44287299);
            //DecksData.Decks[0].AddMainCard(68401546);

            //LV3 cards
            DecksData.Decks[0].AddMainCard(67284908);
            DecksData.Decks[0].AddMainCard(85326399);
            DecksData.Decks[0].AddMainCard(69780745);

            //LV 4 cards
            DecksData.Decks[0].AddMainCard(46986414);
            DecksData.Decks[0].AddMainCard(17658803);
            DecksData.Decks[0].AddMainCard(98434877);

            //LV 5 cards
            DecksData.Decks[0].AddMainCard(1);
            DecksData.Decks[0].AddMainCard(2);
            DecksData.Decks[0].AddMainCard(3);

            //extras
            /*DecksData.Decks[0].AddMainCard(88819587);
            DecksData.Decks[0].AddMainCard(23771716);
            DecksData.Decks[0].AddMainCard(28279543);
            DecksData.Decks[0].AddMainCard(25955164);
            DecksData.Decks[0].AddMainCard(48579379);*/
        }

        public List<int> GetDiceToRollSelection(Deck deck)
        {
            //List of the final selections
            List<int> selections = new List<int>();

            //Copy the deck data locally           
            Deck localDeck = new Deck();
            for (int x = 0; x < deck.MainDeckSize; x++)
            {
                localDeck.AddMainCard(deck.GetMainCardIDAtIndex(x));
            }
            int DeckSize = localDeck.MainDeckSize;


            //if the deck size is 3 or less, just run those cards
            if (DeckSize <= 3)
            {
                lblOutCome.Text = "Outcome: Deck size is 3 or less. Picking those cards.";
                if (DeckSize == 3)
                {
                    selections.Add(localDeck.GetMainCardIDAtIndex(2));
                    selections.Add(localDeck.GetMainCardIDAtIndex(1));
                    selections.Add(localDeck.GetMainCardIDAtIndex(0));
                }
                if (DeckSize == 2)
                {
                    selections.Add(localDeck.GetMainCardIDAtIndex(1));
                    selections.Add(localDeck.GetMainCardIDAtIndex(0));
                }
                if (DeckSize == 1)
                {
                    selections.Add(localDeck.GetMainCardIDAtIndex(0));
                }
            }
            else
            {
                //Determe the selection
                bool levelSelectionSet = false;
                int diceLevelSelection = 0;

                //Step 1: Determine the Dice Level to roll
                //-Pick a dice level at random, if the deck contains at least 2 cards with that dice level - set this level as the target
                List<int> DiceLevelPool = new List<int>() { 1, 2, 3, 4, 5 };
                while (!levelSelectionSet)
                {
                    int rnd = Rand.Range(0, DiceLevelPool.Count);
                    int diceLevel = DiceLevelPool[rnd];

                    int cardsWithDiceLevel = localDeck.GetCardCountWithDiceLevel(diceLevel);

                    if (cardsWithDiceLevel >= 2)
                    {
                        //Set this level as the target Dice Level to roll
                        diceLevelSelection = diceLevel;
                        levelSelectionSet = true;
                    }
                    else
                    {
                        DiceLevelPool.RemoveAt(rnd);

                        if (DiceLevelPool.Count == 0)
                        {
                            levelSelectionSet = true;
                        }
                    }
                }

                lblDiceLevelSelector.Text = "Dice Level: " + diceLevelSelection;

                //Step 2: determine if a dice level was Set.
                //-If it was set, pull as many card of that dice level
                //In the case of only being 2 cards of that dice level, pick the last card at random
                if (diceLevelSelection != 0)
                {
                    lblOutCome.Text = "Outcome: dice level selected. Getting cards from deck of that level";
                    List<int> cardWithSetDiceLevel = localDeck.GetCardsWithDiceLevelAndRemove(diceLevelSelection);

                    //if list is > 3: pick 3 cards at random
                    if (cardWithSetDiceLevel.Count > 3)
                    {
                        lblOutCome.Text = "Outcome: dice level selected. Getting cards from deck of that level - cards in deck with that lv > 3; pick 3 of them";
                        List<int> RNDSelection = Rand.Get3DiffRange(0, cardWithSetDiceLevel.Count);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[0]]);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[1]]);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[2]]);
                    }

                    //if there are exactly 3 just select those
                    if (cardWithSetDiceLevel.Count == 3)
                    {
                        lblOutCome.Text = "Outcome: dice level selected. Getting cards from deck of that level - cards in deck with that level == 3; pick those 3";
                        selections.Add(cardWithSetDiceLevel[0]);
                        selections.Add(cardWithSetDiceLevel[1]);
                        selections.Add(cardWithSetDiceLevel[2]);
                    }

                    //If ONLY 2 cards were pulled, select a third card at random
                    if (cardWithSetDiceLevel.Count == 2)
                    {
                        lblOutCome.Text = "Outcome: dice level selected. Getting cards from deck of that level - card in deck with that level == 2; third one is random";
                        //Add the 2 cards
                        selections.Add(cardWithSetDiceLevel[0]);
                        selections.Add(cardWithSetDiceLevel[1]);

                        //Pick the third one at rnd
                        int rnd2 = Rand.Range(0, localDeck.MainDeckSize);
                        int thirdCardID = localDeck.GetMainCardIDAtIndex(rnd2);
                        selections.Add(thirdCardID);
                    }

                }
                //if the dice level was not set it means there are NO 2 cards of the same level,
                //which mean player cannot longer summon or set... just pick 3 cards at random to get crests
                else
                {
                    lblOutCome.Text = "Outcome: No dice level selected. Picking 3 rnd cards.";
                    //Pick the 3 cards
                    List<int> RNDSelection = Rand.Get3DiffRange(0, localDeck.MainDeckSize - 1);
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[0]));
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[1]));
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[2]));
                }
            }

            return selections;
        }

        private void btnRunTest_Click(object sender, EventArgs e)
        {
            List<int> selection = GetDiceToRollSelection(DecksData.Decks[0]);

            lblSelectionCount.Text = "Selection Count: " + selection.Count;

            if(selection.Count == 3)
            {
                Pic1.Image = ImageServer.FullCardImage(selection[0].ToString());
                Pic2.Image = ImageServer.FullCardImage(selection[1].ToString());
                Pic3.Image = ImageServer.FullCardImage(selection[2].ToString());
            }

            if (selection.Count == 2)
            {
                Pic1.Image = ImageServer.FullCardImage(selection[0].ToString());
                Pic2.Image = ImageServer.FullCardImage(selection[1].ToString());
                Pic3.Image = ImageServer.FullCardImage("0");
            }

            if (selection.Count == 1)
            {
                Pic1.Image = ImageServer.FullCardImage(selection[0].ToString());
                Pic2.Image = ImageServer.FullCardImage("0");
                Pic3.Image = ImageServer.FullCardImage("0");
            }
        }

        private void btntestrand_Click(object sender, EventArgs e)
        {
            for(int x = 0; x < 10; x++)
            {
                int rnd = Rand.Range(0, 5);

                lblRandResults.Text = lblRandResults.Text + "|" +rnd.ToString();
            }
        }
    }
}
