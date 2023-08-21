using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLimitSteamUser_RocketMod
{
        public class VACPlayer
        {
            public string SteamId { get; set; }
            public bool CommunityBanned { get; set; }
            public bool VACBanned { get; set; }
            public int NumberOfVACBans { get; set; }
            public int DaysSinceLastBan { get; set; }
            public int NumberOfGameBans { get; set; }
            public string EconomyBan { get; set; }
        }

        public class VACRootObject
        {
            public List<VACPlayer> players { get; set; }
        }


    public class LevelResponse
    {
        public int player_level { get; set; }
    }

    public class LevelRootObject
    {
        public LevelResponse response { get; set; }
    }


    public class GameTime
    {
        public int appid { get; set; }
        public string name { get; set; }
        public int playtime_2weeks { get; set; }
        public int playtime_forever { get; set; }
        public string img_icon_url { get; set; }
    }

    public class TimeResponse
    {
        public int total_count { get; set; }
        public List<GameTime> games { get; set; }
    }

    public class TimeRootObject
    {
        public TimeResponse response { get; set; }
    }

    public class SMRoot
    {
        public SMResponse response { get; set; }
    }

    public class SMResponse
    {
        public List<SMPlayer> players { get; set; }
    }

    public class SMPlayer
    {
        public string steamid { get; set; }
        public int communityvisibilitystate { get; set; }
        public int profilestate { get; set; }
        public string personaname { get; set; }
        public int commentpermission { get; set; }
        public string profileurl { get; set; }
        public string avatar { get; set; }
        public string avatarmedium { get; set; }
        public string avatarfull { get; set; }
        public string avatarhash { get; set; }
        public int personastate { get; set; }
    }
}
