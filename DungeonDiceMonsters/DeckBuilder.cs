//Joel Campos
//9/8/2023
//DeckBuilder Class

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;

namespace DungeonDiceMonsters
{
    public partial class DeckBuilder : Form
    {
        public DeckBuilder()
        {
            InitializeComponent();


            //TEST: Add cards to the storage
            for(int x = 1; x <= 3; x++) 
            {
                for(int y = 1; y <= 12; y++) 
                {
                    StorageData.AddCard(y);
                }
            }


            InitializeStorageComponents();
            LoadStoragePage();
            //Initialize the ref to the current card selected with the first card in the
            //the storgae. This action is done via user interaction, so trigger it via code
            _CurrentCardSelected = CardDataBase.GetCardWithID(StorageData.Cards[0]);
            SetStorageSelector(0);
        }

        private void InitializeStorageComponents()
        {
            //Index will be save on the Image Object Tag value
            //so when it is clicked it knows which index card is being clicked.
            int pictureIndex = 0;

            int Y_Location = 2;
            for (int x = 0; x < 5; x++)
            {
                int X_Location = 2;
                for (int y = 0; y < 6; y++)
                {
                    //Initialize the border box Image
                    Panel CardBox = new Panel();
                    PanelStorage.Controls.Add(CardBox);
                    CardBox.Location = new Point(X_Location, Y_Location);
                    CardBox.BorderStyle = BorderStyle.FixedSingle;
                    CardBox.Size = new Size(58, 74);
                    _CardPanelList.Add(CardBox);

                    //Initialize the card Image
                    PictureBox CardImage = new PictureBox();
                    CardBox.Controls.Add(CardImage);
                    CardImage.Location = new Point(2, 2);
                    CardImage.BorderStyle = BorderStyle.FixedSingle;
                    CardImage.Size = new Size(52, 68);
                    CardImage.SizeMode = PictureBoxSizeMode.StretchImage;
                    _CardImageList.Add(CardImage);
                    CardImage.Tag = pictureIndex;
                    CardImage.Click += new EventHandler(StorageCard_click);


                    X_Location += 58;
                    pictureIndex++;
                }
                Y_Location += 73;
            }

        }
        private void LoadStoragePage()
        {
            int startIndex = (_CurrentPage * 30) - 30;

            for (int x = 0; x < 30; x++)
            {
                //set the itearator
                int iterator = startIndex + x;

                //If the itearator reached past the last card in the Card DB vanish the card from view
                if (iterator >= StorageData.Cards.Count)
                {
                    _CardPanelList[x].Visible = false;
                }
                else
                {
                    //Make the card panel visible
                    _CardPanelList[x].Visible = true;

                    //Get the card ID of the card to be displayed
                    int cardID = StorageData.Cards[iterator];

                    //Dispose the current image in this picture box (if there was one)
                    //to clear memory
                    if (_CardImageList[x].Image != null) { _CardImageList[x].Image.Dispose(); }

                    //Populate the card image with the card ID
                    _CardImageList[x].Image = ImageServer.FullCardImage(cardID);
                   
                }
            }
        }
        private void LoadCardInfoPanel()
        {
            //Get the CardInfo object to populate the UI
            //remember _currentCardSelected ref is set each time you click a card in the UI.
            CardInfo thisCard = _CurrentCardSelected;
            int cardID = thisCard.ID;

            //Populate the UI
            if (PicCardArtwork.Image != null) { PicCardArtwork.Image.Dispose(); }
            PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID);

            string idstring = cardID.ToString();
            if (cardID < 10) { idstring = "000" + cardID; }
            else if (cardID < 100) { idstring = "00" + cardID; }
            else if (cardID < 1000) { idstring = "0" + cardID; }

            lblID.Text = idstring;
            lblCardName.Text = thisCard.Name;

            string secondaryType = "";
            if (thisCard.IsFusion) { secondaryType = "/Fusion"; }
            if (thisCard.IsRitual) { secondaryType = "/Ritual"; }


            lblCardType.Text = thisCard.Type + secondaryType;

            if (thisCard.Category == "Monster") { lblCardLevel.Text = "Lv. " + thisCard.Level; }
            else { lblCardLevel.Text = ""; }

            if (thisCard.Category == "Monster") { lblAttribute.Text =  thisCard.Attribute; }
            else { lblAttribute.Text = ""; }



        }
        private void SetStorageSelector(int index)
        {
            //Clear all cards from being marked as selected
            for(int x = 0; x < 30; x ++) 
            {
                _CardPanelList[x].BackColor = Color.Transparent;
            }

            //Set the specific card selected
            _CardPanelList[index].BackColor = Color.Yellow;

            //Reload the Card Info Panel UI
            LoadCardInfoPanel();
        }

        private int _CurrentPage = 1;
        private List<Panel> _CardPanelList = new List<Panel>();
        private List<PictureBox> _CardImageList = new List<PictureBox>();
        private CardInfo _CurrentCardSelected = null;


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            //Application.Exit();
        }

        private void StorageCard_click(object sender, EventArgs e)
        {
            //Retrieve the index of the card image that was clicked
            PictureBox thisPictureBox = (PictureBox)sender;
            int thiPictureBoxIndex = Convert.ToInt32(thisPictureBox.Tag);

            //Save a ref to the current card in view
            int startIndex = (_CurrentPage * 30) - 30;
            int indexInStorasge = startIndex + thiPictureBoxIndex;
            int cardid = StorageData.Cards[indexInStorasge];
            _CurrentCardSelected = CardDataBase.GetCardWithID(cardid);

            SetStorageSelector(thiPictureBoxIndex);
        }
        private void btnNext_Click(object sender, EventArgs e)
        {
            _CurrentPage++;

            if(_CurrentPage == 6 ) { _CurrentPage = 1; }
            lblStorage.Text = "Storage Page: " + _CurrentPage;
            LoadStoragePage();
            SetStorageSelector(0);
        }
        private void btnPrevious_Click(object sender, EventArgs e)
        {
            _CurrentPage--;

            if (_CurrentPage == 0) { _CurrentPage = 5; }
            lblStorage.Text = "Storage Page: " + _CurrentPage;
            LoadStoragePage();
            SetStorageSelector(0);
        }
    }
}
