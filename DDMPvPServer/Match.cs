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
            AddLogMessage("Empty Match Created!");
        }

        public void addPlayer(int playerid)
        {
            if (REDPlayerID == -1)
            {
                REDPlayerID = playerid;
                AddLogMessage(string.Format("Client ID: {0} joined the match as Player RED.", playerid));
            }
            else
            {
                BLUEPlayerID = playerid;
                AddLogMessage(string.Format("Client ID: {0} joined the match as Player BLUE.", playerid));
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
            switch (color)
            {
                case PlayerColor.RED: return REDPlayerDeckData;
                case PlayerColor.BLUE: return BLUEPlayerDeckData;
                default: return "NONE";
            }
        }
        public string GetPlayerLevel(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.RED: return REDPlayerLevel;
                case PlayerColor.BLUE: return BLUEPlayerLevel;
                default: return "NONE";
            }
        }
        public string GetPlayerAvatar(PlayerColor color)
        {
            switch (color)
            {
                case PlayerColor.RED: return REDPlayerAvatar;
                case PlayerColor.BLUE: return BLUEPlayerAvatar;
                default: return "NONE";
            }
        }
        public PlayerColor GetPlayerColor(int id)
        {
            if (REDPlayerID == id)
            {
                return PlayerColor.RED;
            }
            else
            {
                return PlayerColor.BLUE;
            }
        }
        public void SetPlayerInfo(PlayerColor color, string name, string level, string avatarId, string deckdata)
        {
            switch (color)
            {
                case PlayerColor.RED:
                    REDPlayerName = name;
                    REDPlayerLevel = level;
                    REDPlayerAvatar = avatarId;
                    REDPlayerDeckData = deckdata;
                    REDPlayerReady = true;
                    AddLogMessage(string.Format("RED Player data received, Player Name: {0}.{1}", name, Environment.NewLine));
                    break;
                case PlayerColor.BLUE:
                    BLUEPlayerName = name;
                    BLUEPlayerLevel = level;
                    BLUEPlayerAvatar = avatarId;
                    BLUEPlayerDeckData = deckdata;
                    BLUEPlayerReady = true;
                    AddLogMessage(string.Format("BLUE Player data received, Player Name: {0}.{1}", name, Environment.NewLine));
                    AddLogMessage(string.Format("Both Players data received, MATCH IS READY!{0}",  Environment.NewLine));
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
            LogTrace.Add(string.Format("LOG#{0}:{1}{2}{3}--------------------",LogTrace.Count, Environment.NewLine, message, Environment.NewLine));
        }
        public string GetLogs()
        {
            string output = "";
            foreach (string line in LogTrace)
            {
                output += line + Environment.NewLine;
            }
            return output;
        }
        public void RemovePlayerInWaiting()
        {
            AddLogMessage("RED Player disconnected: Removing it from the match! - RED Spot available again.");
            REDPlayerID = -1;
            BLUEPlayerID = -1;
            REDPlayerName = "NO SET";
            BLUEPlayerName = "NO SET";
            REDPlayerDeckData = "NO SET";
            BLUEPlayerDeckData = "NO SET";
            REDPlayerReady = false;
            BLUEPlayerReady = false;
        }   
        public void CloseMatch()
        {
            MatchClosed = true;
            AddLogMessage("Match is now closed!!!");
        }
        public bool IsClosed()
        {
            return MatchClosed;
        }

        private int MatchID;
        private int REDPlayerID = -1;
        private int BLUEPlayerID = -1;
        private string REDPlayerName = "NO SET";
        private string BLUEPlayerName = "NO SET";
        private string REDPlayerDeckData = "NO SET";
        private string BLUEPlayerDeckData = "NO SET";
        private string REDPlayerLevel = "NO SET";
        private string BLUEPlayerLevel = "NO SET";
        private string REDPlayerAvatar = "NO SET";
        private string BLUEPlayerAvatar = "NO SET";
        private bool REDPlayerReady = false;
        private bool BLUEPlayerReady = false;
        private bool MatchClosed = false;
        private List<string> LogTrace = new List<string>();

    }
    public enum PlayerColor
    {
        NONE = 0,
        RED,
        BLUE,
    }
}
