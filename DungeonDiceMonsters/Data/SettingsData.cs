//Joel Campos
//10/3/2024
//Setting Data Class

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace DungeonDiceMonsters
{
    public static class SettingsData
    {
        public static void InitializeSettings()
        {
            //This is where we read the settings file and initialize the data
            StreamReader SR_SaveFile = new StreamReader(
                 Directory.GetCurrentDirectory() + "\\Settings Files\\SettingsData.txt");

            string Line = SR_SaveFile.ReadLine();
            int SettingsCount = Convert.ToInt32(Line);

            for (int x = 0; x < SettingsCount; x++)
            {
                Line = SR_SaveFile.ReadLine();
                string[] tokens = Line.Split('|');
                string settingName = tokens[0];
                string settingValue = tokens[1];
                bool settingBoolValue = false;
                if(settingValue == "True" || settingValue == "False") { settingBoolValue = Convert.ToBoolean(settingValue); }

                switch (settingName)
                {
                    case "MusicON": _MusicON = settingBoolValue; break;
                    case "SFXON": _SFXON = settingBoolValue; break;
                    case "4": int songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "5": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "6": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "7": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "8": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "9": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "10": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "11": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "12": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "13": songID = Convert.ToInt32(settingName); if (settingBoolValue) { _IncludeSongList.Add((Song)songID); } else { _ExcludeSongList.Add((Song)songID); } break;
                    case "PvPOnline": _PvPModeOnline = settingBoolValue; break;
                    case "LocalHostIPAddreess":_LocalHostIPAddress = settingValue; break;
                    default: throw new Exception("Invalid setting name to initialize: " + settingName);
                }
            }

        }

        public static void SetMusicONSetting(bool newValue)
        {
            _MusicON = newValue;
            RewriteSettingSaveFile();
        }
        public static void SetSFXONSetting(bool newValue)
        {
            _SFXON = newValue;
            RewriteSettingSaveFile();
        }
        public static void MoveSongToIncludeList(int index)
        {
            Song thisSong = _ExcludeSongList[index];
            _ExcludeSongList.RemoveAt(index);
            _IncludeSongList.Add(thisSong);
            RewriteSettingSaveFile();
        }
        public static void MoveSongToExcludeList(int index)
        {
            Song thisSong = _IncludeSongList[index];
            _IncludeSongList.RemoveAt(index);
            _ExcludeSongList.Add(thisSong);
            RewriteSettingSaveFile();
        }
        public static void SetPvPOnlineMode(bool newValue)
        {
            _PvPModeOnline= newValue;
            RewriteSettingSaveFile();
        }
        public static void SetLocalHostIPAddres(string newIP)
        {
            _LocalHostIPAddress = newIP;
            RewriteSettingSaveFile();
        }

        private static void RewriteSettingSaveFile()
        {
            List<string> outputdata = new List<string>();
            outputdata.Add("14");
            outputdata.Add("MusicON|" + _MusicON.ToString());
            outputdata.Add("SFXON|" + _SFXON.ToString());
            if (IncludeSongList.Contains((Song)4)) { outputdata.Add("4|True"); } else { outputdata.Add("4|False"); }
            if (IncludeSongList.Contains((Song)5)) { outputdata.Add("5|True"); } else { outputdata.Add("5|False"); }
            if (IncludeSongList.Contains((Song)6)) { outputdata.Add("6|True"); } else { outputdata.Add("6|False"); }
            if (IncludeSongList.Contains((Song)7)) { outputdata.Add("7|True"); } else { outputdata.Add("7|False"); }
            if (IncludeSongList.Contains((Song)8)) { outputdata.Add("8|True"); } else { outputdata.Add("8|False"); }
            if (IncludeSongList.Contains((Song)9)) { outputdata.Add("9|True"); } else { outputdata.Add("9|False"); }
            if (IncludeSongList.Contains((Song)10)) { outputdata.Add("10|True"); } else { outputdata.Add("10|False"); }
            if (IncludeSongList.Contains((Song)11)) { outputdata.Add("11|True"); } else { outputdata.Add("11|False"); }
            if (IncludeSongList.Contains((Song)12)) { outputdata.Add("12|True"); } else { outputdata.Add("12|False"); }
            if (IncludeSongList.Contains((Song)13)) { outputdata.Add("13|True"); } else { outputdata.Add("13|False"); }
            outputdata.Add("PvPOnline|" + _PvPModeOnline.ToString());
            outputdata.Add("LocalHostIPAddreess|" + _LocalHostIPAddress.ToString());
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Settings Files\\SettingsData.txt", outputdata);
        }

        public static bool IsMusicON { get { return _MusicON; } }
        public static bool IsSFXON { get { return _SFXON; } }
        public static List<Song> IncludeSongList { get { return _IncludeSongList; }  }
        public static List<Song> ExcludeSongList { get { return _ExcludeSongList; }  }
        public static bool IsPvPInOnlineMode { get { return _PvPModeOnline; } }
        public static string PvPIPAddress 
        {
            get 
            {
                if (_PvPModeOnline) { return _OnlineIPAddress; } else { return _LocalHostIPAddress; }
            }
        }


        private static bool _MusicON = true;
        private static bool _SFXON = true;
        private static List<Song> _IncludeSongList = new List<Song>();
        private static List<Song> _ExcludeSongList = new List<Song>();
        private static bool _PvPModeOnline = true;
        private static string _OnlineIPAddress = "192.168.1.48";
        private static string _LocalHostIPAddress = "invalid";
    }
}
