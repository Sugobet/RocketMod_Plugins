using Newtonsoft.Json;
using Rocket.Core.Logging;
using SDG.Framework.IO.Deserialization;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SLimitSteamUser_RocketMod
{
    public static class Limit
    {
        private static readonly string key = PluginMain.Config.SteamKey;

        public static bool HasSteamVAC(string steamID)
        {
            if (!PluginMain.Config.启用存在VAC记录限制进服) { return false; }

            using WebClient wc = new WebClient();
            string jsonString = "";
            try
            {
                jsonString = wc.DownloadString($"http://api.steampowered.com/ISteamUser/GetPlayerBans/v1/?key={key}&steamids={steamID}");
            } catch
            {
                Logger.Log("Steam API请求失败，请检查Key是否失效或配置错误！", ConsoleColor.Red);
                return false;
            }

            VACRootObject jsonData = JsonConvert.DeserializeObject<VACRootObject>(jsonString);

            var player = jsonData.players[0];
            int numberOfGameBans = player.NumberOfGameBans;

            if (numberOfGameBans <= 0)
            {
                return false;
            }

            return true;
        }

        public static bool IfSteamLevel(string steamID)
        {
            if (PluginMain.Config.Steam等级限制进服 == -1) { return false; }

            using WebClient wc = new WebClient();
            string jsonString = "";
            try
            {
                jsonString = wc.DownloadString($"https://api.steampowered.com/IPlayerService/GetSteamLevel/v1/?key={key}&steamid={steamID}");
            }
            catch
            {
                Logger.Log("Steam API请求失败，请检查Key是否失效或配置错误！", ConsoleColor.Red);
                return false;
            }

            LevelRootObject jsonData = JsonConvert.DeserializeObject<LevelRootObject>(jsonString);
            var res = jsonData.response;
            int level = res.player_level;

            if (level < PluginMain.Config.Steam等级限制进服)
            {
                return true;
            }

            return false;
        }

        public static bool IfUnTime(string steamID)
        {
            if (PluginMain.Config.Unturned游戏时长限制 == -1) { return false; }

            using WebClient wc = new WebClient();
            string jsonString = "";
            try
            {
                jsonString = wc.DownloadString($"https://api.steampowered.com/IPlayerService/GetRecentlyPlayedGames/v1/?key={key}&steamid={steamID}");
            }
            catch
            {
                Logger.Log("Steam API请求失败，请检查Key是否失效或配置错误！", ConsoleColor.Red);
                return false;
            }

            if (!jsonString.Contains("total_count"))
            {
                if (PluginMain.Config.是否禁止游戏时长为私密进服)
                {
                    return true;
                }
                else { return false; }
            }

            TimeRootObject jsonData = JsonConvert.DeserializeObject<TimeRootObject>(jsonString);
            var res = jsonData.response;

            var games = res.games;
            if (games.Count == 0)
            {
                return false;
            }

            foreach ( var game in games )
            {
                if (game.appid == 304930)
                {
                    int time = game.playtime_forever / 60;
                    if (time < PluginMain.Config.Unturned游戏时长限制)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        public static bool IfSteamSM(string steamID)
        {
            if (!PluginMain.Config.是否禁止私密账号进服) { return false; }

            using WebClient wc = new WebClient();
            string jsonString = "";
            try
            {
                jsonString = wc.DownloadString($"https://api.steampowered.com/ISteamUser/GetPlayerSummaries/v2/?key={key}&steamids={steamID}");
            }
            catch
            {
                Logger.Log("Steam API请求失败，请检查Key是否失效或配置错误！", ConsoleColor.Red);
                return false;
            }

            SMRoot jsonData = JsonConvert.DeserializeObject<SMRoot>(jsonString);
            var res = jsonData.response;
            var players = res.players;

            foreach (var player in players)
            {
                if (player.personastate == 0 && jsonString.Contains("realname"))
                {
                    return false;
                }
                else if (player.personastate == 0 && !jsonString.Contains("realname"))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
