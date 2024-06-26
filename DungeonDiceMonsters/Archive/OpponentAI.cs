//Joel Campos
//11/10/2023
//Opponent AI Class

using System.Collections.Generic;

namespace DungeonDiceMonsters
{
    public static class OpponentAI
    {
        public static List<int> GetDiceToRollSelection(Deck deck)
        {
            //List of the final selections
            List<int> selections = new List<int>();

            //Copy the deck data locally           
            Deck localDeck = deck.GetCopy();
            for (int x = 0; x < deck.MainDeckSize; x++)
            {
                localDeck.AddMainCard(deck.GetMainCardIDAtIndex(x));
            }
            int DeckSize = localDeck.MainDeckSize;


            //if the deck size is 3 or less, just run those cards
            if (DeckSize <= 3)
            {
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

                //Step 2: determine if a dice level was Set.
                //-If it was set, pull as many card of that dice level
                //In the case of only being 2 cards of that dice level, pick the last card at random
                if (diceLevelSelection != 0)
                {
                    List<int> cardWithSetDiceLevel = localDeck.GetCardsWithDiceLevelAndRemove(diceLevelSelection);

                    //if list is > 3: pick 3 cards at random
                    if (cardWithSetDiceLevel.Count > 3)
                    {
                        List<int> RNDSelection = Rand.Get3DiffRange(0, cardWithSetDiceLevel.Count);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[0]]);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[1]]);
                        selections.Add(cardWithSetDiceLevel[RNDSelection[2]]);
                    }

                    //if there are exactly 3 just select those
                    if (cardWithSetDiceLevel.Count == 3)
                    {
                        selections.Add(cardWithSetDiceLevel[0]);
                        selections.Add(cardWithSetDiceLevel[1]);
                        selections.Add(cardWithSetDiceLevel[2]);
                    }

                    //If ONLY 2 cards were pulled, select a third card at random
                    if (cardWithSetDiceLevel.Count == 2)
                    {
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
                    //Pick the 3 cards
                    List<int> RNDSelection = Rand.Get3DiffRange(0, localDeck.MainDeckSize - 1);
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[0]));
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[1]));
                    selections.Add(localDeck.GetMainCardIDAtIndex(RNDSelection[2]));
                }
            }

            return selections;
        }
    }
}
