//Joel Campos
//6/28/2024
//Library Form Menu

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class LibraryMenu : Form
    {
        #region Constructors
        public LibraryMenu()
        {
            SoundServer.PlayBackgroundMusic(Song.LibraryMenu);
            InitializeComponent();
            InitializeLibraryComponents();
            UpdateCollectionTotals();
            LoadLibraryPage();
        }
        #endregion

        #region Data
        private List<Panel> _LibCardBoxBordes = new List<Panel>();
        private List<PictureBox> _LibCardBoxPictures = new List<PictureBox>();
        private int _CurrentPage = 1;
        private const int CARDS_PER_PAGE = 100;
        private CardInfo _CurrentCardSelected;
        #endregion

        #region Private Methods/Events
        private void LoadCardInfoPanel()
        {
            //Get the CardInfo object to populate the UI
            //remember _currentCardSelected ref is set each time you click a card in the UI.
            CardInfo thisCard = _CurrentCardSelected;
            int cardID = thisCard.ID;

            //Populate the UI
            ImageServer.ClearImage(PicCardArtwork);
            PicCardArtwork.Image = ImageServer.FullCardImage(cardID.ToString());

            lblID.Text = "ID: " + cardID.ToString();
            lblCardName.Text = thisCard.Name;

            //Card Number
            lblCardNumber.Text = "No. " + thisCard.CardNumber.ToString();

            string secondaryType = thisCard.SecType.ToString();
            lblCardType.Text = thisCard.TypeAsString + "/" + secondaryType;
            if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " spell"; }
            if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " trap"; }

            if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Monster LV. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            ImageServer.ClearImage(PicCardAttribute);
            ImageServer.ClearImage(PicCardMonsterType);
            PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.Attribute);
            PicCardMonsterType.Image = ImageServer.MonsterTypeIcon(thisCard.TypeAsString);

            lblDiceLevel.Text = "Dice LV. " + thisCard.DiceLevel;

            if (thisCard.Category == Category.Monster)
            {
                lblStats.Text = "   LP " + thisCard.LP;
                lblStats2.Text = "ATK " + thisCard.ATK;
                lblStats3.Text = "DEF " + thisCard.DEF;
            }
            else 
            { 
                lblStats.Text = ""; 
                lblStats2.Text = ""; 
                lblStats3.Text = ""; 
            }

            string fullcardtext = "";
            if (thisCard.SecType == SecType.Fusion)
            {
                string fusionMaterials = thisCard.FusionMaterial1 + " + " + thisCard.FusionMaterial2;
                if (thisCard.FusionMaterial3 != "-") { fusionMaterials = fusionMaterials + " + " + thisCard.FusionMaterial3; }
                fullcardtext = fullcardtext + fusionMaterials + "\n\n";
            }

            if (thisCard.HasOnSummonEffect)
            {
                fullcardtext = fullcardtext + "(ON SUMMON) - " + thisCard.OnSummonEffect + "\n\n";
            }

            if (thisCard.HasAbility)
            {
                fullcardtext = fullcardtext + "(ABILITY) - " + thisCard.Ability + "\n\n";
            }

            if (thisCard.HasContinuousEffect)
            {
                fullcardtext = fullcardtext + "(CONTINUOUS) - " + thisCard.ContinuousEffect + "\n\n";
            }


            if (thisCard.HasIgnitionEffect)
            {
                fullcardtext = fullcardtext + "(EFFECT) - " + thisCard.IgnitionEffect + "\n\n";
            }

            if (thisCard.HasTriggerEffect)
            {
                fullcardtext = fullcardtext + "(TRIGGER) - " + thisCard.TriggerEffect + "\n\n";
            }

            if (thisCard.HasEquipEffect)
            {
                fullcardtext = fullcardtext + "(EQUIP) - " + thisCard.EquipEffect + "\n\n";
            }

            lblCardText.Text = fullcardtext;

            //Dice Faces
            ImageServer.ClearImage(PicDiceFace1);
            ImageServer.ClearImage(PicDiceFace2);
            ImageServer.ClearImage(PicDiceFace3);
            ImageServer.ClearImage(PicDiceFace4);
            ImageServer.ClearImage(PicDiceFace5);
            ImageServer.ClearImage(PicDiceFace6);
            PicDiceFace1.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(0).ToString(), thisCard.DiceFaceValue(0));
            PicDiceFace2.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(1).ToString(), thisCard.DiceFaceValue(1));
            PicDiceFace3.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(2).ToString(), thisCard.DiceFaceValue(2));
            PicDiceFace4.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(3).ToString(), thisCard.DiceFaceValue(3));
            PicDiceFace5.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(4).ToString(), thisCard.DiceFaceValue(4));
            PicDiceFace6.Image = ImageServer.DiceFace(thisCard.DiceLevel, thisCard.DiceFace(5).ToString(), thisCard.DiceFaceValue(5));
        }
        private void UpdateCollectionTotals()
        {
            int collectionCount = GameData.GetLibraryCollectionCount();
            int Dbtotal = CardDataBase.CardCount;
            lblCollectionTotals.Text = "COLLECTION: " + collectionCount + "/" + Dbtotal;
            lblStorage.Text = "Page: " + _CurrentPage;
        }
        private void InitializeLibraryComponents()
        {
            int pictureIndex = 0;

            int Y_Location = 17;
            for (int x = 0; x < 10; x++)
            {
                int X_Location = 19;
                for (int y = 0; y < 10; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelContainer.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(30, 32);
                    _LibCardBoxBordes.Add(CardBox);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(24, 26);
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    _LibCardBoxPictures.Add(CardImage);
                    CardImage.Tag = pictureIndex;
                    //CardImage.Click += new EventHandler(StorageCard_click);
                    CardImage.MouseEnter += new EventHandler(OnMouseEnterStorageCard);

                    X_Location += 29;
                    pictureIndex++;
                }
                Y_Location += 31;
            }

        }
        private void LoadLibraryPage()
        {
            int startIndex = (_CurrentPage * CARDS_PER_PAGE) - CARDS_PER_PAGE;

            for (int x = 0; x < CARDS_PER_PAGE; x++)
            {
                //set the itearator
                int iterator = startIndex + x;

                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (iterator >= CardDataBase.CardCount)
                {
                    _LibCardBoxBordes[x].Visible = false;
                }
                else
                {
                    //if the card has been obtained
                    if(GameData.IsLibraryCardObtainedAtIndex(iterator))
                    {
                        //Make the card panel visible
                        _LibCardBoxBordes[x].Visible = true;

                        //Get the card ID of the card to be displayed
                        CardInfo thisCardIfo = CardDataBase.GetCardWithCardNo(iterator + 1);

                        //Populate the card image with the card ID
                        ImageServer.ClearImage(_LibCardBoxPictures[x]);
                        _LibCardBoxPictures[x].Image = ImageServer.LibraryCardIcon(thisCardIfo.Category, thisCardIfo.SecType);
                    }
                    else
                    {
                        _LibCardBoxBordes[x].Visible = false;
                    }                   
                }
            }
        }
        private int GetLastPage()
        {
            int CurrentCardListCount = CardDataBase.CardCount;
            double pages = ((double)CurrentCardListCount / (double)CARDS_PER_PAGE);
            int lastpage = (int)pages;
            double remaining = pages - (int)pages;
            if (remaining > 0) { lastpage++; }
            return lastpage;
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void OnMouseEnterStorageCard(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);

            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Save a ref to the current card in view
            int startIndex = (_CurrentPage * CARDS_PER_PAGE) - CARDS_PER_PAGE;
            int indexInStorasge = startIndex + thiPictureBoxIndex;

            _CurrentCardSelected = CardDataBase.GetCardWithCardNo(indexInStorasge + 1);

            foreach(Panel p in _LibCardBoxBordes)
            {
                p.BackColor = Color.Black;
            }

            _LibCardBoxBordes[thiPictureBoxIndex].BackColor = Color.Yellow;
            LoadCardInfoPanel();
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);
            int lastpage = GetLastPage();

            if (lastpage != 0)
            {
                if (_CurrentPage == lastpage) { _CurrentPage = 1; }
                else { _CurrentPage++; }

                lblStorage.Text = "Page: " + _CurrentPage;
                LoadLibraryPage();

                //Clear all cards from being marked as selected
                foreach (Panel p in _LibCardBoxBordes)
                {
                    p.BackColor = Color.Black;
                }
            }
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click);

            int lastpage = GetLastPage();
            if (lastpage != 0)
            {
                if (_CurrentPage == 1) { _CurrentPage = lastpage; }
                else { _CurrentPage--; }

                lblStorage.Text = "Page: " + _CurrentPage;
                LoadLibraryPage();

                //Clear all cards from being marked as selected
                foreach (Panel p in _LibCardBoxBordes)
                {
                    p.BackColor = Color.Black;
                }
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        #endregion
    }
}
