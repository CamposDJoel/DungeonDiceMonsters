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
    }
}
