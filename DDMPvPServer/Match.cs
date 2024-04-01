//Joel Campos
//4/1/2024
//Match Class

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDMPvPServer
{
    public class Match
    {
        public Match(int id)
        {
            MatchID = id;
        }

        public void addPlayer(int playerid)
        {
            if (REDPlayerID == -1)
            {
                REDPlayerID = playerid;
                LogTrace.Add(string.Format("Client ID: {0} joined the match as Player RED.{1}", playerid, Environment.NewLine));
            }
            else
            {
                BLUEPlayerID = playerid;
                LogTrace.Add(string.Format("Client ID: {0} joined the match as Player BLUE.{1}", playerid, Environment.NewLine));
            }
        }
        public bool IsMatchFull()
        {
            return REDPlayerID != -1 && BLUEPlayerID != -1;
        }
        public bool ContainsPlayer(int id)
        {
            return (REDPlayerID == id || BLUEPlayerID == id);
        }
        public string GetPlayerName(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.RED: return REDPlayerName;
                case PlayerColor.BLUE: return BLUEPlayerName;
                default: return "NONE";
            }
        }
        public string GetDeckData(PlayerColor color)
        {
            switch(color) 
            {
                case PlayerColor.RED: return REDPlayerDeckData;
                case PlayerColor.BLUE: return BLUEPlayerDeckData;
                default: return "NONE";
            }
        }
        public PlayerColor GetPlayerColor(int id)
        {
            if(REDPlayerID == id)
            {
                return PlayerColor.RED;
            }
            else
            {
                return PlayerColor.BLUE;
            }
        }
        public void SetPlayerInfo(PlayerColor color, string name, string deckdata)
        {
            switch (color)
            {
                case PlayerColor.RED:
                    REDPlayerName = name;
                    REDPlayerDeckData = deckdata;
                    REDPlayerReady = true;
                    LogTrace.Add(string.Format("RED Player data received, Player Name: {0}.{1}", name, Environment.NewLine));
                    break;
                case PlayerColor.BLUE:
                    BLUEPlayerName = name;
                    BLUEPlayerDeckData  = deckdata;
                    BLUEPlayerReady = true;
                    LogTrace.Add(string.Format("BLUE Player data received, Player Name: {0}.{1}", name, Environment.NewLine));
                    LogTrace.Add(string.Format("Both Players data received, MATCH IS READY!{0}", name, Environment.NewLine));
                    break;
            }
        }
        public bool AreBothPlayersReady()
        {
            return REDPlayerReady && BLUEPlayerReady;
        }
        public int GetOpponentClientID(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.RED: return BLUEPlayerID;
                case PlayerColor.BLUE: return REDPlayerID;
                default: return -1;
            }
        }
        public void AddLogMessage(string message)
        {
            LogTrace.Add(message);
        }
        public string GetLogs()
        {
            string output = "";
            foreach(string line in LogTrace)
            {
                output += line + Environment.NewLine;
            }
            return output;
        }


        private int MatchID;
        private int REDPlayerID = -1;
        private int BLUEPlayerID = -1;
        private string REDPlayerName = "NO SET";
        private string BLUEPlayerName = "NO SET";
        private string REDPlayerDeckData = "NO SET";
        private string BLUEPlayerDeckData = "NO SET";
        private bool REDPlayerReady = false;
        private bool BLUEPlayerReady = false;
        private List<string> LogTrace = new List<string>();      
    }

    public enum PlayerColor
    {
        NONE = 0,
        RED,
        BLUE,
    }
}
