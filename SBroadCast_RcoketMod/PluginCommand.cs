using Rocket.API;
using Logger = Rocket.Core.Logging.Logger;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Rocket.Unturned.Extensions;

namespace SBroadCast_RcoketMod
{
    public class PluginCommand : IRocketCommand
    {
        public AllowedCaller AllowedCaller => AllowedCaller.Console;

        public string Name => "sbc";

        public string Help => "广播发送配置文件中的消息给所有玩家";

        public string Syntax => "/sbc <SteamID>";

        public List<string> Aliases => new List<string>();

        public List<string> Permissions => new List<string> { "SBroadCast.sbc" };
        
        public void Execute(IRocketPlayer caller, string[] command)
        {
            var config = PluginMain.Config;
            int fontSize = config.FontSize;
            Color fontColor = config.FontColor;

            try
            {
                ulong SteamID = ulong.Parse(command[0]);
                SteamPlayer sp = PlayerTool.getSteamPlayer(SteamID);
                string playerName = sp.ToUnturnedPlayer().DisplayName;
                string message = config.Message;
                message = message.Replace("player", playerName);
                string[] msgList = message.Split('|');

                foreach (string msg in msgList) 
                {
                    string nMsg = "<size=" + fontSize.ToString() + ">" + msg + "</size>";
                    ChatManager.say(nMsg, fontColor, true);
                }
            } catch {
                Logger.Log("SteamID错误或该玩家不在线");
            } 
        }
    }
}
