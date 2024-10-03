//Joel Campos
//10/3/2024
//Setting Data Class

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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

            for(int x = 0; x < SettingsCount; x++)
            {
                Line = SR_SaveFile.ReadLine();
                string[] tokens = Line.Split('|');
                string settingName = tokens[0];
                string settingValue = tokens[1];
                bool settingBoolValue = Convert.ToBoolean(settingValue);

                switch (settingName) 
                {
                    case "MusicON":_MusicON = settingBoolValue; break;
                    case "SFXON": _SFXON = settingBoolValue; break;
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
            _SFXON= newValue;
            RewriteSettingSaveFile();
        }

        private static void RewriteSettingSaveFile()
        {
            List<string> outputdata = new List<string>();
            outputdata.Add("2");
            outputdata.Add("MusicON|" + _MusicON.ToString());
            outputdata.Add("SFXON|" + _SFXON.ToString());
            File.WriteAllLines(Directory.GetCurrentDirectory() + "\\Settings Files\\SettingsData.txt", outputdata);
        }

        public static bool IsMusicON { get { return _MusicON; } }
        public static bool IsSFXON { get { return _SFXON; } }


        private static bool _MusicON = true;
        private static bool _SFXON = true;
    }
}
