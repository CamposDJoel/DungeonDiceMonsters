//Joel Campos
//6/26/2024
//Decks Manager Form

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class DecksManager : Form
    {
        #region Constructors
        public DecksManager(bool playMusic)
        {
            if (playMusic) { SoundServer.PlayBackgroundMusic(Song.DeckBuildMenu, true); }
            InitializeComponent();
            InitializeDeckComponents();
            ReloadDeckList();

            btnRename.MouseEnter += OnMouseHover;
            checkEnableDelete.MouseEnter += OnMouseHover;
            btnDeleteDeck.MouseEnter += OnMouseHover;
            btnCreate.MouseEnter += OnMouseHover;
            btnEditDeck.MouseEnter += OnMouseHover;
            btnExit.MouseEnter += OnMouseHover;
        }
        #endregion

        #region Private Methods
        private void InitializeDeckComponents()
        {
            //Index will be save on the Image Object Tag value
            //so when it is clicked it knows which index card is being clicked.
            int pictureIndex = 0;

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
                    _DeckCardPanelList.Add(CardBox);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(52, 68);
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    _DeckCardImageList.Add(CardImage);
                    CardImage.Tag = pictureIndex;
                    //CardImage.Click += new EventHandler(DeckCard_click);
                    //CardImage.MouseEnter += new EventHandler(OnMouseEnterDeckCard);

                    X_Location += 58;
                    pictureIndex++;
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
                _DeckCardPanelList.Add(CardBox);

                //Initialize the card Image
                PictureBox CardImage = new PictureBox();
                CardBox.Controls.Add(CardImage);
                CardImage.Location = new Point(2, 2);
                CardImage.BorderStyle = BorderStyle.FixedSingle;
                CardImage.Size = new Size(52, 68);
                CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                _DeckCardImageList.Add(CardImage);
                CardImage.Tag = pictureIndex;
                //CardImage.Click += new EventHandler(DeckCard_click);
                //CardImage.MouseEnter += new EventHandler(OnMouseEnterDeckCard);

                X_Location2 += 58;
                pictureIndex++;
            }
        }
        private void ReloadDeckList()
        {
            listDecks.Items.Clear();
            int iterator = 1;
            foreach (Deck thisDeck in DecksData.DecksList)
            {
                listDecks.Items.Add(iterator + "." + thisDeck.Name);
                iterator++;
            }
            listDecks.SetSelected(0, true);

            UpdateDeckLimitCapacityUI();
        }
        private void LoadDeckPage()
        {
            //Set the deck name
            lblDeckName.Text = _CurrentDeckSelected.Name;

            //Load the Main Deck
            for (int x = 0; x < 20; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _CurrentDeckSelected.MainDeckSize)
                {
                    _DeckCardPanelList[x].Visible = false;
                    _DeckIDsInCurrentPage[x] = 0;
                }
                else
                {
                    //if the ID of the card to be place in this card slot 
                    //is the same as the one before dont do anything...
                    if (_DeckIDsInCurrentPage[x] != _CurrentDeckSelected.GetMainCardIDAtIndex(x))
                    {
                        _DeckIDsInCurrentPage[x] = _CurrentDeckSelected.GetMainCardIDAtIndex(x);

                        //Make the card panel visible
                        _DeckCardPanelList[x].Visible = true;

                        //Get the card ID of the card to be displayed
                        int cardID = _CurrentDeckSelected.GetMainCardIDAtIndex(x);

                        //Populate the card image with the card ID
                        ImageServer.ClearImage(_DeckCardImageList[x]);
                        _DeckCardImageList[x].Image = ImageServer.FullCardImage(cardID.ToString());
                    }
                }
            }

            //Load the fusion deck
            for (int x = 0; x < 3; x++)
            {
                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (x >= _CurrentDeckSelected.FusionDeckSize)
                {
                    _DeckCardPanelList[x + 20].Visible = false;
                }
                else
                {
                    //Make the card panel visible
                    _DeckCardPanelList[x + 20].Visible = true;

                    //Get the card ID of the card to be displayed
                    int cardID = _CurrentDeckSelected.GetFusionCardIDAtIndex(x);

                    //Populate the card image with the card ID
                    ImageServer.ClearImage(_DeckCardImageList[x + 20]);
                    _DeckCardImageList[x + 20].Image = ImageServer.FullCardImage(cardID.ToString());
                }
            }

            //Update the Symbol
            _CurrentSymbolSelection = _CurrentDeckSelected.Symbol;
            ImageServer.ClearImage(PicSymbol);
            PicSymbol.Image = ImageServer.Symbol(_CurrentSymbolSelection.ToString());
            lblSymbolName.Text = _CurrentSymbolSelection.ToString();

            switch (_CurrentSymbolSelection)
            {
                case Attribute.DARK: lblSymbolName.ForeColor = Color.Purple; break;
                case Attribute.LIGHT: lblSymbolName.ForeColor = Color.LightCyan; break;
                case Attribute.WATER: lblSymbolName.ForeColor = Color.RoyalBlue; break;
                case Attribute.FIRE: lblSymbolName.ForeColor = Color.Crimson; break;
                case Attribute.EARTH: lblSymbolName.ForeColor = Color.Orange; break;
                case Attribute.WIND: lblSymbolName.ForeColor = Color.PaleGreen; break;
            }
            //uncheck the Delete menu
            checkEnableDelete.Checked = false;

            //Set the Ready flag
            ImageServer.ClearImage(PicDeckStatus);
            PicDeckStatus.Image = ImageServer.DeckStatusIcon(_CurrentDeckSelected.UseStatus.ToString());
        }
        private void UpdateDeckLimitCapacityUI()
        {
            if (DecksData.DecksList.Count == 50)
            {
                btnCreate.Enabled = false;
                btnCopyDeck.Enabled = false;
                lblWarning.Visible = true;
                lblWarning.Text = "Max Capacity Reached.";
            }
            else
            {
                btnCreate.Enabled = true;
                btnCopyDeck.Enabled = true;
                lblWarning.Visible = false;
            }
        }
        #endregion

        #region Data
        private List<Panel> _DeckCardPanelList = new List<Panel>();
        private List<PictureBox> _DeckCardImageList = new List<PictureBox>();
        private Deck _CurrentDeckSelected;
        private int[] _DeckIDsInCurrentPage = new int[20];
        private Attribute _CurrentSymbolSelection = Attribute.DARK;
        #endregion

        #region Events
        private void OnMouseHover(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
        }
        private void listDecks_SelectedIndexChanged(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            _CurrentDeckSelected = DecksData.GetDeckAtIndex(listDecks.SelectedIndex);
            checkEnableDelete.Checked = false;
            txtRenameNameInput.Text = _CurrentDeckSelected.Name;
            //Reload the Deck contents
            LoadDeckPage();
        }
        private void btnCreate_Click(object sender, EventArgs e)
        {
            string nameInput = txtDeckNameInput.Text;

            if (nameInput == "")
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblWarning.Text = "Name cannot be empty.";
                lblWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblWarning.Visible = false;
            }
            else if (nameInput.Contains("|"))
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblWarning.Text = "Name cannot contain \"|\".";
                lblWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblWarning.Visible = false;
            }
            else if (nameInput.Length > 20)
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblWarning.Text = "Must be 20 chars or less.";
                lblWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblWarning.Visible = false;
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                btnCreate.Visible = false;
                Deck newDeck = new Deck(nameInput, Attribute.DARK);
                DecksData.AddDeck(newDeck);
                listDecks.Items.Add((listDecks.Items.Count + 1) + "." + newDeck.Name);
                listDecks.SetSelected((listDecks.Items.Count - 1), true);
                UpdateDeckLimitCapacityUI();
                SaveFileManger.WriteSaveFile();
                btnCreate.Visible = true;
            }
        }
        private void btnRename_Click(object sender, EventArgs e)
        {
            string nameInput = txtRenameNameInput.Text;

            if (nameInput == "")
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblRenameWarning.Text = "Name cannot be empty.";
                lblRenameWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblRenameWarning.Visible = false;
            }
            else if (nameInput.Contains("|"))
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblRenameWarning.Text = "Name cannot contain \"|\".";
                lblRenameWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblRenameWarning.Visible = false;
            }
            else if (nameInput.Length > 20)
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
                lblRenameWarning.Text = "Must be 20 chars or less.";
                lblRenameWarning.Visible = true;
                BoardPvP.WaitNSeconds(1000);
                lblRenameWarning.Visible = false;
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                btnRename.Visible = false;
                _CurrentDeckSelected.Rename(nameInput);
                int indexSelected = listDecks.SelectedIndex;
                ReloadDeckList();
                listDecks.SetSelected(indexSelected, true);
                SaveFileManger.WriteSaveFile();
                btnRename.Visible = true;
            }
        }
        private void btnCopyDeck_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            btnCopyDeck.Visible = false;
            Deck newDeck = _CurrentDeckSelected.GetCopy();
            DecksData.AddDeck(newDeck);
            listDecks.Items.Add((listDecks.Items.Count + 1) + "." + newDeck.Name);
            listDecks.SetSelected((listDecks.Items.Count - 1), true);
            UpdateDeckLimitCapacityUI();
            SaveFileManger.WriteSaveFile();
            btnCopyDeck.Visible = true;
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void checkEnableDelete_CheckedChanged(object sender, EventArgs e)
        {
            if (checkEnableDelete.Checked)
            {
                btnDeleteDeck.Visible = true;
                if(listDecks.Items.Count == 1)
                {
                    btnDeleteDeck.Enabled = false;
                    lblDeleteWarning.Visible = true;
                    lblStorageWarning.Visible = false;
                }
                else
                {
                    btnDeleteDeck.Enabled = true;
                    lblDeleteWarning.Visible = false;
                    lblStorageWarning.Visible = true;
                }
            }
            else
            {
                btnDeleteDeck.Visible = false;
                lblDeleteWarning.Visible = false;
                lblStorageWarning.Visible = false;
            }
        }
        private void btnDeleteDeck_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
            int index = listDecks.SelectedIndex;
            _CurrentDeckSelected.SendAllCardsToStorage();
            DecksData.RemoveDeck(index);
            ReloadDeckList();
            SaveFileManger.WriteSaveFile();
        }
        private void btnEditDeck_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            DeckBuilder DB = new DeckBuilder(_CurrentDeckSelected);
            Dispose();
            DB.Show();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        #endregion
    }
}