using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
