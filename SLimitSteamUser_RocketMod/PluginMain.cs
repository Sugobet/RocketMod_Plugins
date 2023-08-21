using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SLimitSteamUser_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        public static PluginMain Instance { get; private set; }
        public static PluginConfig Config { get; private set; }

        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;

            U.Events.OnBeforePlayerConnected += OnBeforePlayerConnected;

            Logger.Log("SLimitSteamUser v1.0.0 loaded    Author: Sugobet");
        }

        protected override void Unload()
        {
            U.Events.OnBeforePlayerConnected -= OnBeforePlayerConnected;
        }

        private void OnBeforePlayerConnected(UnturnedPlayer player)
        {
            if (Limit.HasSteamVAC(player.CSteamID.ToString()))
            {
                UnturnedChat.Say($"玩家: {player.CharacterName} 因账号存在VAC记录被禁止进本服", Color.red);
                player.Kick("你因账号存在VAC记录被禁止进本服");
                return;
            }

            if (Limit.IfSteamSM(player.CSteamID.ToString()))
            {
                UnturnedChat.Say($"玩家: {player.CharacterName} 因Steam账号为私密账号被禁止进本服", Color.red);
                player.Kick($"你因Steam账号为私密账号被禁止进本服");
                return;
            }

            if (Limit.IfSteamLevel(player.CSteamID.ToString()))
            {
                UnturnedChat.Say($"玩家: {player.CharacterName} 因Steam等级不满{Config.Steam等级限制进服}级被禁止进本服", Color.red);
                player.Kick($"你因Steam等级不满{Config.Steam等级限制进服}级被禁止进本服");
                return;
            }

            if (Limit.IfUnTime(player.CSteamID.ToString()))
            {
                UnturnedChat.Say($"玩家: {player.CharacterName} 因Unturned游戏时长不满{Config.Unturned游戏时长限制}小时被禁止进本服", Color.red);
                player.Kick($"你因Unturned游戏时长不满{Config.Unturned游戏时长限制}小时被禁止进本服");
                return;
            }
        }
    }
}
