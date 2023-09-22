//Joel Campos
//9/8/2023
//ImageServer Class

using System.Drawing;
using System.IO;

namespace DungeonDiceMonsters
{
    public static class ImageServer
    {
        public static Image FullCardImage(int id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Full Size Cards\\" + id + ".jpeg");
        }

        public static Image CardArtworkImage(int id)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Artwork\\" + id + ".png");
        }

        public static Image DiceFace(int diceLevel, string faceType, int faceValue)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Dices\\Level " + diceLevel + "\\" + faceType + faceValue + ".png");
        }

        public static Image DeckStatusIcon(bool status)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\Icons\\" + status + ".jpg");
        }
        public static Image CharacterIcon(Character c)
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
        public static Image DimensionForm(DimensionForms form)
        {
            return Image.FromFile(Directory.GetCurrentDirectory() + "\\Images\\DimensionForms\\" + form + ".jpg");
        }
    }
}
