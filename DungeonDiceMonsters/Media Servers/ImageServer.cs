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
        public static void ClearImage(PictureBox box)
        {
            if(box.Image != null) { box.Image.Dispose(); }
            box.Image = null;
        }
        public static void ClearImage(Panel box)
        {
            if (box.BackgroundImage != null) { box.BackgroundImage.Dispose(); }
            box.BackgroundImage = null;
        }
        public static Image FullCardImage(string id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Full Size Cards\\" + id + ".jpeg");
        }
        public static Image CardArtworkImage(string id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Artwork\\" + id + ".jpg");
        }
        public static Image DeckStatusIcon(string status)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\" + status + ".jpg");
        }
        public static Image CrestIcon(string icon)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\" + icon + ".png");
        }
        public static Image CharacterIcon(string c)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Characters\\" + c + ".png");
        }
        public static Image Symbol(string symbol)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Symbols\\" + symbol + ".png");
        }
        public static Image FullCardSymbol(string symbol)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Full Size Cards\\" + symbol + " Symbol.jpeg");
        }
        public static Image DimensionForm(string form)
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
        public static Image FieldTile(string field) 
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Field Tiles\\" + field + ".jpg");
        }
        public static Image AttackTargetIcon()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\Attack Target.png");
        }
        public static Image EffectTargetIcon()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\Effect Target.png");
        }
        public static Image SpellTrapZone()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\SpellTrapZone.png");
        }
        public static Image FaceDownSetCard()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\SetFacedownCard.png");
        }
        public static Image FusionMaterialTarget()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\FusionTarget.png");
        }
        public static Image AttributeIcon(Attribute thisAtt)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\Attributes\\" + thisAtt + ".png");
        }
        public static Image MonsterTypeIcon(string type)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\Monster Types\\" + type + ".png");
        }
        public static Image SpellboundIcon()
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\SpellboundIcon.png");
        }
    }
}
