//Joel Campos
//9/12/2023
//Sound Server Class

using System;
using System.IO;
using System.Windows.Media;

namespace DungeonDiceMonsters
{
    public static class SoundServer
    {
        public static void PlayBackgroundMusic(Song song, bool TurnOn)
        {
            if (TurnOn)
            {
                string filepath = "\\Music\\Songs\\" + song + ".m4a";
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
        public static void PlaySoundEffect(SoundEffect sound)
        {
            string filepath = "\\Music\\SFX\\" + sound + ".wav";
            Effect.Open(new Uri(Directory.GetCurrentDirectory() + filepath));
            Effect.Play();
            Effect.Volume = 0.3;
        }
        public static void PlayPvPBackgroundMusic()
        {
            //Set the song to play at random
            int songIndex = Rand.V(SongsPlaylist.Length);
            _CurrentSongPlaying = SongsPlaylist[songIndex];
            string song = _CurrentSongPlaying.ToString();
            string filepath = "\\Music\\Songs\\" + song + ".m4a";
            CurrentBackGroundPlay.Open(new Uri(Directory.GetCurrentDirectory() + filepath));

            //This event will loop the song... once the song ends it plays again
            CurrentBackGroundPlay.MediaEnded += new EventHandler(Media_Ended);
            CurrentBackGroundPlay.Play();
            CurrentBackGroundPlay.Volume = CurrentVolumeLevel;
        }

        public static string GetCurrentSongPlaying()
        {
            switch(_CurrentSongPlaying)
            {
                case Song.TitleScreen: return "Tittle Screen";
                case Song.MainMenu: return "Main Menu";
                case Song.FreeDuelMenu: return "Free Duel Menu";
                case Song.DeckBuildMenu: return "Deck Build Menu";
                case Song.YouWin: return "You Win";
                case Song.YouLose: return "You Lose";
                case Song.LibraryMenu: return "Library Menu";
                case Song.DUEL_FreeDuel: return "♫ Free Duel (Forbidden Memories) ♫";
                case Song.DUEL_FinalsMatch: return "♫ Final's Match (Forbidden Memories) ♫";
                case Song.DUEL_SetosTheme: return "♫ Seto's Theme (Forbidden Memories) ♫";
                case Song.DUEL_HeshinsInvasion: return "♫ Heshin's Invasion (Forbidden Memories) ♫";
                case Song.DUEL_HeishinTheme: return "♫ Heshin's Theme (Forbidden Memories) ♫";
                case Song.DUEL_HighMagesTheme: return "♫ High Mages' Theme (Forbidden Memories) ♫";
                case Song.DUEL_KaibaTheme: return "♫ Kaiba's Theme (Forbidden Memories) ♫";
                case Song.DUEL_EgyptianDuel: return "♫ Egyptian Duel (Forbidden Memories) ♫";
                case Song.DUEL_MageDuel: return "♫ Mage Duel (Forbidden Memories) ♫";
                case Song.DUEL_VsNitemareDOR: return "♫ VS Nitemare (Duelists of the Roses) ♫";
                default: throw new Exception("Song hgas no String name");
            }
        }

        private static void Media_Ended(object sender, EventArgs e)
        {
            CurrentBackGroundPlay.Position = TimeSpan.Zero;
            CurrentBackGroundPlay.Play();
        }

        private static MediaPlayer CurrentBackGroundPlay = new MediaPlayer();
        private static MediaPlayer Effect = new MediaPlayer();

        private static Song[] SongsPlaylist = new Song[]
        {
            Song.DUEL_FreeDuel,
            Song.DUEL_FinalsMatch,
            Song.DUEL_SetosTheme,
            Song.DUEL_HeshinsInvasion,
            Song.DUEL_HeishinTheme,
            Song.DUEL_HighMagesTheme,
            Song.DUEL_KaibaTheme,
            Song.DUEL_EgyptianDuel,
            Song.DUEL_MageDuel,
            Song.DUEL_VsNitemareDOR,
        };
        private static double CurrentVolumeLevel = 0.3;
        private static Song _CurrentSongPlaying;
    }

    public enum  Song
    {
        TitleScreen,
        MainMenu,
        FreeDuelMenu,
        DeckBuildMenu,
        DUEL_FreeDuel,
        DUEL_FinalsMatch,
        DUEL_SetosTheme,
        DUEL_HeshinsInvasion,
        DUEL_HeishinTheme,
        DUEL_HighMagesTheme,
        DUEL_KaibaTheme,
        DUEL_EgyptianDuel,
        DUEL_MageDuel,
        DUEL_VsNitemareDOR,
        YouWin,
        YouLose,
        LibraryMenu,
    }
    public enum SoundEffect
    {
        Hover,
        Click,
        Click2,
        MoveCard,
        InvalidClick,
        Attack,
        LPReduce,
        CardDestroyed,
        SummonMonster,
        EffectMenu,
        SetCard,
        Cancel,
        EffectApplied,
        TriggerEffect,
        GoBack,
        Confirm,
        CardToDeck,
        CardToStorage,
        Target,
        FusionSummon,
        TransformSummon,
        SelectTarget,
        Spellbound,
        EndTurn
    }
}
