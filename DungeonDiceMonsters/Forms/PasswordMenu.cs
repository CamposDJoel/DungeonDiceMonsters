//Joel Campos
//7/2/2024
//Password Menu

using System;
using System.Drawing;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public partial class PasswordMenu : Form
    {
        public PasswordMenu()
        {
            SoundServer.PlayBackgroundMusic(Song.PasswordMenu);
            InitializeComponent();
            ReloadStarshipCount();

            PicUpDigit0x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit0x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit0x.Click += ClickArrow;
            PicUpDigit1x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit1x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit1x.Click += ClickArrow;
            PicUpDigit2x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit2x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit2x.Click += ClickArrow;
            PicUpDigit3x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit3x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit3x.Click += ClickArrow;
            PicUpDigit4x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit4x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit4x.Click += ClickArrow;
            PicUpDigit5x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit5x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit5x.Click += ClickArrow;
            PicUpDigit6x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit6x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit6x.Click += ClickArrow;
            PicUpDigit7x.MouseEnter += OnMouseEnterLabel;
            PicUpDigit7x.MouseLeave += OnMouseLeaveLabel;
            PicUpDigit7x.Click += ClickArrow;

            PicDownDigit0x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit0x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit0x.Click += ClickArrow;
            PicDownDigit1x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit1x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit1x.Click += ClickArrow;
            PicDownDigit2x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit2x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit2x.Click += ClickArrow;
            PicDownDigit3x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit3x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit3x.Click += ClickArrow;
            PicDownDigit4x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit4x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit4x.Click += ClickArrow;
            PicDownDigit5x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit5x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit5x.Click += ClickArrow;
            PicDownDigit6x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit6x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit6x.Click += ClickArrow;
            PicDownDigit7x.MouseEnter += OnMouseEnterLabel;
            PicDownDigit7x.MouseLeave += OnMouseLeaveLabel;
            PicDownDigit7x.Click += ClickArrow;

            LoadCardInfoPanel(null);
        }

        private int[] _PasscodeDigits = new int[8];
        private CardInfo _CardInDisplay;
        private int _CostOfCardInDisplay = 0;

        private void ReloadStarshipCount()
        {
            lblStarchipCount.Text = "x " + GameData.StarChips;
        }
        private void LoadCardInfoPanel(CardInfo thisCard)
        {
            if (thisCard == null)
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);

                //Card Images
                ImageServer.ClearImage(PicCardArtwork);
                ImageServer.ClearImage(PicCardDisplay);
                PicCardArtwork.Image = ImageServer.CardArtworkImage("0");
                PicCardDisplay.Image = ImageServer.FullCardImage("0");

                //Card ID, Name and Number
                lblID.Text = string.Empty;
                lblCardName.Text = string.Empty;
                lblCardNumber.Text = string.Empty;

                //Secondary Type
                lblCardType.Text = string.Empty;

                //Monster Lv
                lblCardLevel.Text = string.Empty;

                //Monster Type and Attribute icons
                PicCardAttribute.Visible = false;
                PicCardMonsterType.Visible = false;

                //Stats
                lblStats.Text = string.Empty;

                //Card Text
                lblCardText.Text = string.Empty;

                //Dice Lv
                lblDiceLevel.Text = string.Empty;

                //Dice faces
                PicDiceFace1.Visible = false;
                PicDiceFace2.Visible = false;
                PicDiceFace3.Visible = false;
                PicDiceFace4.Visible = false;
                PicDiceFace5.Visible = false;
                PicDiceFace6.Visible = false;
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.EffectApplied);
                int cardID = thisCard.ID;

                //Card Images
                ImageServer.ClearImage(PicCardArtwork);
                ImageServer.ClearImage(PicCardDisplay);
                PicCardArtwork.Image = ImageServer.CardArtworkImage(cardID.ToString());
                PicCardDisplay.Image = ImageServer.FullCardImage(cardID.ToString());

                //Card ID, Name and Number
                lblID.Text = "ID: " + cardID.ToString();
                lblCardName.Text = thisCard.Name;
                lblCardNumber.Text = "No. " + thisCard.CardNumber.ToString();

                //Secondary Type
                string secondaryType = thisCard.SecType.ToString();
                lblCardType.Text = thisCard.TypeAsString + " / " + secondaryType;
                if (thisCard.Category == Category.Spell) { lblCardType.Text = thisCard.TypeAsString + " Spell"; }
                if (thisCard.Category == Category.Trap) { lblCardType.Text = thisCard.TypeAsString + " Trap"; }

                //Monster Lv
                if (thisCard.Category == Category.Monster) { lblCardLevel.Text = "Monster LV. " + thisCard.Level; }
                else { lblCardLevel.Text = ""; }

                //Monster Type and Attribute icons
                ImageServer.ClearImage(PicCardAttribute);
                ImageServer.ClearImage(PicCardMonsterType);
                PicCardAttribute.Image = ImageServer.AttributeIcon(thisCard.Attribute);
                PicCardMonsterType.Image = ImageServer.MonsterTypeIcon(thisCard.TypeAsString);
                PicCardAttribute.Visible = true;
                PicCardMonsterType.Visible = true;

                //Stats
                if (thisCard.Category == Category.Monster)
                {
                    lblStats.Text = "ATK " + thisCard.ATK + " / DEF " + thisCard.DEF + " / LP " + thisCard.LP;
                }
                else { lblStats.Text = ""; }

                //Card Text
                lblCardText.Text = thisCard.CardText;

                //Dice Lv
                lblDiceLevel.Text = "Dice LV. " + thisCard.DiceLevel;

                //Dice faces
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
                PicDiceFace1.Visible = true;
                PicDiceFace2.Visible = true;
                PicDiceFace3.Visible = true;
                PicDiceFace4.Visible = true;
                PicDiceFace5.Visible = true;
                PicDiceFace6.Visible = true;
            }          
        }

        private void OnMouseEnterLabel(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Hover);
            PictureBox thisLabel = (PictureBox)sender;
            string code = thisLabel.Tag.ToString();

            switch(code)
            {
                case "0U": PicUpDigit0.BackColor = Color.Yellow; break;
                case "1U": PicUpDigit1.BackColor = Color.Yellow; break;
                case "2U": PicUpDigit2.BackColor = Color.Yellow; break;
                case "3U": PicUpDigit3.BackColor = Color.Yellow; break;
                case "4U": PicUpDigit4.BackColor = Color.Yellow; break;
                case "5U": PicUpDigit5.BackColor = Color.Yellow; break;
                case "6U": PicUpDigit6.BackColor = Color.Yellow; break;
                case "7U": PicUpDigit7.BackColor = Color.Yellow; break;
                case "0D": PicDownDigit0.BackColor = Color.Yellow; break;
                case "1D": PicDownDigit1.BackColor = Color.Yellow; break;
                case "2D": PicDownDigit2.BackColor = Color.Yellow; break;
                case "3D": PicDownDigit3.BackColor = Color.Yellow; break;
                case "4D": PicDownDigit4.BackColor = Color.Yellow; break;
                case "5D": PicDownDigit5.BackColor = Color.Yellow; break;
                case "6D": PicDownDigit6.BackColor = Color.Yellow; break;
                case "7D": PicDownDigit7.BackColor = Color.Yellow; break;
            }
        }
        private void OnMouseLeaveLabel(object sender, EventArgs e)
        {
            PictureBox thisLabel = (PictureBox)sender;
            string code = thisLabel.Tag.ToString();

            switch (code)
            {
                case "0U": PicUpDigit0.BackColor = Color.Black; break;
                case "1U": PicUpDigit1.BackColor = Color.Black; break;
                case "2U": PicUpDigit2.BackColor = Color.Black; break;
                case "3U": PicUpDigit3.BackColor = Color.Black; break;
                case "4U": PicUpDigit4.BackColor = Color.Black; break;
                case "5U": PicUpDigit5.BackColor = Color.Black; break;
                case "6U": PicUpDigit6.BackColor = Color.Black; break;
                case "7U": PicUpDigit7.BackColor = Color.Black; break;
                case "0D": PicDownDigit0.BackColor = Color.Black; break;
                case "1D": PicDownDigit1.BackColor = Color.Black; break;
                case "2D": PicDownDigit2.BackColor = Color.Black; break;
                case "3D": PicDownDigit3.BackColor = Color.Black; break;
                case "4D": PicDownDigit4.BackColor = Color.Black; break;
                case "5D": PicDownDigit5.BackColor = Color.Black; break;
                case "6D": PicDownDigit6.BackColor = Color.Black; break;
                case "7D": PicDownDigit7.BackColor = Color.Black; break;
            }
        }
        private void ClickArrow(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.Click2);
            PictureBox thisLabel = (PictureBox)sender;
            string code = thisLabel.Tag.ToString();
            switch (code)
            {
                case "0U": UpdateDigit(0, true); break;
                case "1U": UpdateDigit(1, true); break;
                case "2U": UpdateDigit(2, true); break;
                case "3U": UpdateDigit(3, true);  break;
                case "4U": UpdateDigit(4, true); break;
                case "5U": UpdateDigit(5, true); break;
                case "6U": UpdateDigit(6, true); break;
                case "7U": UpdateDigit(7, true); break;
                case "0D": UpdateDigit(0, false); break;
                case "1D": UpdateDigit(1, false); break;
                case "2D": UpdateDigit(2, false); break;
                case "3D": UpdateDigit(3, false); break;
                case "4D": UpdateDigit(4, false); break;
                case "5D": UpdateDigit(5, false); break;
                case "6D": UpdateDigit(6, false); break;
                case "7D": UpdateDigit(7, false); break;
            }

            UpdatePasscodeDisplay();


            void UpdateDigit(int digit, bool up)
            {
                if(up)
                {
                    if(_PasscodeDigits[digit] == 9)
                    {
                        _PasscodeDigits[digit] = 0;
                    }
                    else
                    {
                        _PasscodeDigits[digit]++;
                    }
                }
                else
                {
                    if (_PasscodeDigits[digit] == 0)
                    {
                        _PasscodeDigits[digit] = 9;
                    }
                    else
                    {
                        _PasscodeDigits[digit]--;
                    }
                }
                
            }
            void UpdatePasscodeDisplay()
            {
                lblDigit0.Text = _PasscodeDigits[0].ToString();
                lblDigit1.Text = _PasscodeDigits[1].ToString();
                lblDigit2.Text = _PasscodeDigits[2].ToString();
                lblDigit3.Text = _PasscodeDigits[3].ToString();
                lblDigit4.Text = _PasscodeDigits[4].ToString();
                lblDigit5.Text = _PasscodeDigits[5].ToString();
                lblDigit6.Text = _PasscodeDigits[6].ToString();
                lblDigit7.Text = _PasscodeDigits[7].ToString();
            }
        }
        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            Application.Exit();
        }
        private void btnExit_Click(object sender, EventArgs e)
        {
            SoundServer.PlaySoundEffect(SoundEffect.GoBack);
            MainMenu MM = new MainMenu();
            Dispose();
            MM.Show();
        }
        private void btnEnter_Click(object sender, EventArgs e)
        {
            lblExchangeConfirmation.Visible = false;
            int Passcode = GetPasscodeConverted();
            _CardInDisplay = CardDataBase.GetCardWithID(Passcode);
            ImageServer.ClearImage(PicCardDisplay);
            LoadCardInfoPanel(_CardInDisplay);

            if(_CardInDisplay == null)
            {
                PanelExchangeMenu.Visible = false;
            }
            else
            {
                //TODO: add to the DB the starchip cost
                _CostOfCardInDisplay = 12;
                PanelExchangeMenu.Visible = true;
                lblCardNoExcchange.Text = string.Format("CARD NUMBER: {0}", _CardInDisplay.CardNumber);
                lblExchangeCost.Text = string.Format("x {0}", _CostOfCardInDisplay);
                btnExchange.Visible = true;

                if(GameData.IsExchangeCardObtainedAtIndex(_CardInDisplay.CardNumber - 1))
                {
                    lblExchangeWarning.Visible = true;
                    btnExchange.BackColor = Color.Red;
                }
                else
                {
                    lblExchangeWarning.Visible = false;
                    if (GameData.StarChips >= _CostOfCardInDisplay)
                    {
                        btnExchange.BackColor = Color.Green;
                    }
                    else
                    {
                        btnExchange.BackColor = Color.Red;
                    }
                }

                
            }

            int GetPasscodeConverted()
            {
                int digit1Amount = _PasscodeDigits[0] * 10000000;
                int digit2Amount = _PasscodeDigits[1] * 1000000;
                int digit3Amount = _PasscodeDigits[2] * 100000;
                int digit4Amount = _PasscodeDigits[3] * 10000;
                int digit5Amount = _PasscodeDigits[4] * 1000;
                int digit6Amount = _PasscodeDigits[5] * 100;
                int digit7Amount = _PasscodeDigits[6] * 10;
                int digit8Amount = _PasscodeDigits[7];
                int total = digit1Amount + digit2Amount + digit3Amount + digit4Amount + digit5Amount + digit6Amount + digit7Amount + digit8Amount;
                return total;
            }
        }
        private void btnExchange_Click(object sender, EventArgs e)
        {
            if (btnExchange.BackColor == Color.Green) 
            {
                //TODO: Perform the exchange
                SoundServer.PlaySoundEffect(SoundEffect.Accept);
                //Add Card to storage
                StorageData.AddCard(_CardInDisplay.ID);
                //And Mark the card as obtained in the library and mark the card as exchanged
                GameData.MarkLibraryCardObtained(_CardInDisplay.CardNumber - 1);
                GameData.MarkExchangeCardObtained(_CardInDisplay.CardNumber - 1);
                //Update the UI to prevent another exchange on this card
                btnExchange.Visible = false;
                lblExchangeConfirmation.Visible = true;
                //Reduce the starchips used
                GameData.AdjustStarchipsAmount(-_CostOfCardInDisplay);
                ReloadStarshipCount();
                SaveFileManger.WriteSaveFile();
            }
            else
            {
                SoundServer.PlaySoundEffect(SoundEffect.InvalidClick);
            }
        }
    }
}
