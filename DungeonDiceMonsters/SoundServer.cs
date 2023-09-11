using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace DungeonDiceMonsters
{
    public static class SoundServer
    {
        public static void PlayBackgroundMusic(Song song, bool TurnOn)
        {
            if (TurnOn)
            {
                string filepath = "\\Music\\" + song + ".m4a";
                CurrentBackGroundPlay.Open(new Uri(Directory.GetCurrentDirectory() + filepath));
                //This event will loop the song... once the song ends it plays again
                CurrentBackGroundPlay.MediaEnded += new EventHandler(Media_Ended);
                CurrentBackGroundPlay.Play();
                CurrentBackGroundPlay.Volume = 0.3;
            }
            else
            {
                CurrentBackGroundPlay.Stop();
            }

        }

        private static void Media_Ended(object sender, EventArgs e)
        {
            CurrentBackGroundPlay.Position = TimeSpan.Zero;
            CurrentBackGroundPlay.Play();
        }

        private static MediaPlayer CurrentBackGroundPlay = new MediaPlayer();
    }

    public enum  Song
    {
        TitleScreen,
        MainMenu,
        FreeDuelMenu,
        DeckBuildMenu,
    }
}
