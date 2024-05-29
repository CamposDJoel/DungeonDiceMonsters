//Joel Campos
//9/8/2023
//ImageServer Class

using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace DungeonDiceMonsters
{
    public static class ImageServer
    {
        public static void LoadImage(PictureBox box, CardImageType type, string arg1)
        {
            if(box.Image != null) { box.Image.Dispose(); }
            switch (type) 
            {
                case CardImageType.FullCardImage: box.Image = FullCardImage(arg1); break;
                case CardImageType.CardArtwork: box.Image = CardArtworkImage(arg1); break;
                case CardImageType.DeckStatusIcon: box.Image = DeckStatusIcon(arg1); break;
                case CardImageType.CharacterIcon: box.Image = CharacterIcon(arg1); break;
                case CardImageType.Symbol: box.Image = Symbol(arg1); break;
                case CardImageType.FullCardSymbol: box.Image = FullCardSymbol(arg1); break;
                case CardImageType.DimensionForm: box.Image = DimensionForm(arg1); break;
                case CardImageType.FieldTile: box.Image = FieldTile(arg1); break;
                case CardImageType.CrestIcon: box.Image = CrestIcon(arg1); break;
            }
        }
        public static void LoadImageToPanel(Panel box, CardImageType type, string arg1)
        {
            if (box.BackgroundImage != null) { box.BackgroundImage.Dispose(); }
            switch (type)
            {
                case CardImageType.FullCardImage: box.BackgroundImage = FullCardImage(arg1); break;
                case CardImageType.FullCardSymbol: box.BackgroundImage = FullCardSymbol(arg1); break;
                case CardImageType.FieldTile: box.BackgroundImage = FieldTile(arg1); break;
            }
        }
        public static void RemoveImageFromPanel(Panel box)
        {
            if (box.BackgroundImage != null) { box.BackgroundImage.Dispose(); }
            box.BackgroundImage = null;
        }

        private static Image FullCardImage(string id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Full Size Cards\\" + id + ".jpeg");
        }
        private static Image CardArtworkImage(string id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Artwork\\" + id + ".jpg");
        }
        private static Image DeckStatusIcon(string status)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\" + status + ".jpg");
        }
        private static Image CrestIcon(string icon)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\" + icon + ".png");
        }
        private static Image CharacterIcon(string c)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Characters\\" + c + ".png");
        }
        private static Image Symbol(string symbol)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Symbols\\" + symbol + ".png");
        }
        private static Image FullCardSymbol(string symbol)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Full Size Cards\\" + symbol + " Symbol.jpeg");
        }
        private static Image DimensionForm(string form)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\DimensionForms\\" + form + ".jpg");
        }
        public static Image PhaseBanner(PlayerColor player, string phase)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\PhaseBanners\\" + player + "\\" + phase + ".png");
        }
        public static Image DiceFace(int diceLevel, string faceType, int faceValue)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Dices\\Level " + diceLevel + "\\" + faceType + faceValue + ".png");
        }                  
        private static Image FieldTile(string field) 
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Field Tiles\\" + field + ".jpg");
        }
    }

    public enum CardImageType
    {
        FullCardImage,
        CardArtwork,
        DiceFace,
        DeckStatusIcon,
        CharacterIcon,
        Symbol,
        FullCardSymbol,
        DimensionForm,
        PhaseBanner,
        FieldTile,
        CrestIcon,
    }
}
