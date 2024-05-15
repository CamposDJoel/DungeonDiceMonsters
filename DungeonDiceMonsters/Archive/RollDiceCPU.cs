using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class RollDiceCPU : Form
    {
        public RollDiceCPU(PlayerData playerdata, BoardForm board)
        {
            InitializeComponent();
            _PlayerData = playerdata;
            _Board = board;

            //Initialize the flag of free summon tiles, this is going to be used to determine if the player can set Spell/Traps
            //_UnoccupiedSummonTiles = _Board.GetUnoccupiedSummoningTiles(PlayerColor.RED).Count > 0;

            InitializeDeckComponents();
            GenrateValidDimension();
            DetermineIfOpenSummonTilesForSetting();
            UpdateCrestPool();
        }

        private void InitializeDeckComponents()
        {
            //Generate the card images for the deck. Card WONT be revealed but Player can still see the deck size.

            int Y_Location = 2;
            for (int x = 0; x < 4; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 5; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelDeck.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(58, 74);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(52, 68);
                    ImageServer.LoadImage(CardImage, CardImageType.FullCardImage, "0");
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;

                    X_Location += 58;
                }
                Y_Location += 73;
            }

            //Initialize the 3 Cards for the Fusion Deck
            Y_Location = 314;
            int X_Location2 = 2;
            for (int x = 0; x < 3; x++)
            {
                //Initialize the border box Image
                Panel CardBox = new Panel();
                PanelDeck.Controls.Add(CardBox);
                CardBox.Location = new Point(X_Location2, Y_Location);
                CardBox.BorderStyle = BorderStyle.FixedSingle;
                CardBox.Size = new Size(58, 74);

                //Initialize the card Image
                PictureBox CardImage = new PictureBox();
                CardBox.Controls.Add(CardImage);
                CardImage.Location = new Point(2, 2);
                CardImage.BorderStyle = BorderStyle.FixedSingle;
                CardImage.Size = new Size(52, 68);
                ImageServer.LoadImage(CardImage, CardImageType.FullCardImage, "0");
                CardImage.SizeMode = PictureBoxSizeMode.StretchImage;

                X_Location2 += 58;
            }
        }
        private void GenrateValidDimension()
        {
            //Generate the valid dimension tiles
            _ValidDimensions = new List<Dimension>();

            //Each each tile with each dimension form
            List<Tile> BoardTiles = _Board.GetTiles();

            //There are 23 dimension form
            for (int f = 0; f < 23; f++)
            {
                DimensionForms thisForm = (DimensionForms)f;

                //Check all the tiles....
                for (int t = 0; t < BoardTiles.Count; t++)
                {
                    Tile thisTile = BoardTiles[t];
                    Tile[] dimensionTiles = thisTile.GetDimensionTiles(thisForm);
                    Dimension thisDimension = new Dimension(dimensionTiles, thisForm, PlayerColor.BLUE);

                    if (thisDimension.IsValid)
                    {
                        _ValidDimensions.Add(thisDimension);
                    }
                }
            }

            //Flag if there are valid dimensions
            _ValidDimensionAvailable = _ValidDimensions.Count > 0;

            //Display the no dimension warning
            if (!_ValidDimensionAvailable)
            {
                lblNoDimensionTilesWarning.Visible = true;
            }
        }
        private void DetermineIfOpenSummonTilesForSetting()
        {
            //if the card sent is a spell/trap and the player has not summon tiles open, display warning.
            if (_UnoccupiedSummonTiles)
            {
                lblNoSummonTilesWarning.Visible = true;
            }
        }
        private void UpdateCrestPool()
        {
            //Load the Crest Pool counter
            lblMOVCount.Text = _PlayerData.Crests_MOV.ToString();
            lblATKCount.Text = _PlayerData.Crests_ATK.ToString();
            lblDEFCount.Text = _PlayerData.Crests_DEF.ToString();
            lblMAGCount.Text = _PlayerData.Crests_MAG.ToString();
            lblTRAPCount.Text = _PlayerData.Crests_TRAP.ToString();
        }

        private BoardForm _Board;
        private PlayerData _PlayerData;
        private bool _ValidDimensionAvailable = false;
        private List<Dimension> _ValidDimensions;
        private bool _UnoccupiedSummonTiles = false;

        private void btnCPURoll_Click(object sender, EventArgs e)
        {
            btnCPURoll.Visible = false;

            //Call the AI Tool to get the 3 (or less) dice selection
            List<int> diceSelection = OpponentAI.GetDiceToRollSelection(_PlayerData.Deck);

            //Place cards in the selection
            if (diceSelection.Count == 1)
            {
                ImageServer.LoadImage(PicDice1, CardImageType.FullCardImage, "0");
            }
            if (diceSelection.Count == 2)
            {
                ImageServer.LoadImage(PicDice1, CardImageType.FullCardImage, "0");
                ImageServer.LoadImage(PicDice2, CardImageType.FullCardImage, "0");
            }
            if (diceSelection.Count == 3)
            {
                ImageServer.LoadImage(PicDice1, CardImageType.FullCardImage, "0");
                ImageServer.LoadImage(PicDice2, CardImageType.FullCardImage, "0");
                ImageServer.LoadImage(PicDice3, CardImageType.FullCardImage, "0");
            }

            //Another delay
            BoardForm.WaitNSeconds(1000);

            //Roll the dice
            int[] diceIndex = new int[3] { -1, -1, -1 };
            Crest[] diceFace = new Crest[3] { Crest.NONE, Crest.NONE, Crest.NONE };
            int[] diceValue = new int[3] { 0, 0, 0 };

            //display the result faces
            diceIndex[0] = Rand.DiceRoll();
            CardInfo Dice1 = CardDataBase.GetCardWithID(diceSelection[0]);

            for (int x = 0; x < 6; x++)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Attack);
                PicDiceResult1.Image = null;
                PicDiceResult1.BackColor = Color.White;
                BoardForm.WaitNSeconds(100);
                PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, Dice1.DiceFace(x).ToString(), Dice1.DiceFaceValue(x));
                BoardForm.WaitNSeconds(100);
            }
            diceFace[0] = Dice1.DiceFace(diceIndex[0]);
            diceValue[0] = Dice1.DiceFaceValue(diceIndex[0]);
            PicDiceResult1.Image = ImageServer.DiceFace(Dice1.DiceLevel, diceFace[0].ToString(), diceValue[0]);


            if (diceSelection.Count > 1)
            {
                diceIndex[1] = Rand.DiceRoll();
                CardInfo Dice2 = CardDataBase.GetCardWithID(diceSelection[1]);
                for (int x = 0; x < 6; x++)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Attack);
                    PicDiceResult2.Image = null;
                    PicDiceResult2.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, Dice2.DiceFace(x).ToString(), Dice2.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[1] = Dice2.DiceFace(diceIndex[1]);
                diceValue[1] = Dice2.DiceFaceValue(diceIndex[1]);
                PicDiceResult2.Image = ImageServer.DiceFace(Dice2.DiceLevel, diceFace[1].ToString(), diceValue[1]);
            }
            else
            {
                PicDiceResult2.Image = null;
            }

            if (diceSelection.Count > 2)
            {
                diceIndex[2] = Rand.DiceRoll();
                CardInfo Dice3 = CardDataBase.GetCardWithID(diceSelection[2]);
                for (int x = 0; x < 6; x++)
                {
                    SoundServer.PlaySoundEffect(SoundEffect.Attack);
                    PicDiceResult3.Image = null;
                    PicDiceResult3.BackColor = Color.White;
                    BoardForm.WaitNSeconds(100);
                    PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, Dice3.DiceFace(x).ToString(), Dice3.DiceFaceValue(x));
                    BoardForm.WaitNSeconds(100);
                }
                diceFace[2] = Dice3.DiceFace(diceIndex[2]);
                diceValue[2] = Dice3.DiceFaceValue(diceIndex[2]);
                PicDiceResult3.Image = ImageServer.DiceFace(Dice3.DiceLevel, diceFace[2].ToString(), diceValue[2]);
            }
            else
            {
                PicDiceResult3.Image = null;
            }

            //Call the logic           
            List<CardInfo> finalSelection = new List<CardInfo>();
            foreach (int id  in diceSelection) 
            {
                finalSelection.Add(CardDataBase.GetCardWithID(id));
            }
            int[] results = RollDiceMenu.GetDiceSummonSetStatus(finalSelection, diceFace, diceValue);

            //Calculate the crests to add to the pool
            int movToAdd = 0;
            int atkToAdd = 0;
            int defToAdd = 0;
            int magToAdd = 0;
            int trapToAdd = 0;
            for (int x = 0; x < 3; x++)
            {
                if (results[x] == 0)
                {
                    switch (diceFace[x])
                    {
                        case Crest.MOV: movToAdd += diceValue[x]; break;
                        case Crest.ATK: atkToAdd += diceValue[x]; break;
                        case Crest.DEF: defToAdd += diceValue[x]; break;
                        case Crest.MAG: magToAdd += diceValue[x]; break;
                        case Crest.TRAP: trapToAdd += diceValue[x]; break;
                    }
                }
            }

            //Do a little delay here to pace the animation
            BoardForm.WaitNSeconds(1000);

            //Add them to the pool
            for (int x = 0; x < movToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.MOV, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMOVCount.ForeColor = Color.Green;
                lblMOVCount.Text = _PlayerData.Crests_MOV.ToString();
            }
            for (int x = 0; x < atkToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.ATK, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblATKCount.ForeColor = Color.Green;
                lblATKCount.Text = _PlayerData.Crests_ATK.ToString();
            }
            for (int x = 0; x < defToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.DEF, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblDEFCount.ForeColor = Color.Green;
                lblDEFCount.Text = _PlayerData.Crests_DEF.ToString();
            }
            for (int x = 0; x < magToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.MAG, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblMAGCount.ForeColor = Color.Green;
                lblMAGCount.Text = _PlayerData.Crests_MAG.ToString();
            }
            for (int x = 0; x < trapToAdd; x++)
            {
                _PlayerData.AddCrests(Crest.TRAP, 1);
                SoundServer.PlaySoundEffect(SoundEffect.LPReduce);
                BoardForm.WaitNSeconds(200);
                lblTRAPCount.ForeColor = Color.Green;
                lblTRAPCount.Text = _PlayerData.Crests_TRAP.ToString();
            }

            //Display the Summon Set buttons for the dice that qualifyfir
            bool canSummonSet = false;
            switch (results[0])
            {
                //Normal Summon (Note that if there are not dimesion spaces, you cannot summon)
                case 1: if (_ValidDimensionAvailable) { btnDice1Summon.Visible = true; canSummonSet = true; } break;
                //Set (Note that if there are not free summon tiles you cant set)
                case 2: if (_UnoccupiedSummonTiles) { btnDice1Set.Visible = true; canSummonSet = true; } break;
                //Ritual Summon (Note that if there are not dimesion spaces, you cannot summon)
                case 4: if (_ValidDimensionAvailable) { btnDice1Ritual.Visible = true; canSummonSet = true; } break;
            }
            switch (results[1])
            {
                //Normal Summon
                case 1: if (_ValidDimensionAvailable) { btnDice2Summon.Visible = true; canSummonSet = true; } break;
                case 2: if (_UnoccupiedSummonTiles) { btnDice2Set.Visible = true; canSummonSet = true; } break;
                case 4: if (_ValidDimensionAvailable) { btnDice2Ritual.Visible = true; canSummonSet = true; } break;
            }
            switch (results[2])
            {
                //Normal Summon
                case 1: if (_ValidDimensionAvailable) { btnDice3Summon.Visible = true; canSummonSet = true; } break;
                case 2: if (_UnoccupiedSummonTiles) { btnDice3Set.Visible = true; canSummonSet = true; } break;
                case 4: if (_ValidDimensionAvailable) { btnDice3Ritual.Visible = true; canSummonSet = true; } break;
            }

            if (!canSummonSet)
            {
                //Display "Go to the board" button
                btnGoToBoard.Visible = true;
            }

            //Small delay before CPU takes an action
            BoardForm.WaitNSeconds(1000);

            //If cant summon or set, simply go to the board
            if (!canSummonSet)
            {
                SoundServer.PlaySoundEffect(SoundEffect.Click);
                //In the board reload the crest counts
                _Board.SetupCPUMainPhaseNoSummon();

                //Close this form and retrn to the board
                Dispose();
                _Board.Show();

                //Trigger the CPU actions on the board
                _Board.StartCPUMainPhaseActions();
            }    
            //else summon or set
            else
            {

            }
        }
    }
}
