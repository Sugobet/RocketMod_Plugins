using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using SNoLoot_RocketMod;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;

namespace SRustResizePatch
{
    public class PluginMain : RocketPlugin
    {
        private Dictionary<string, PlayerData> UnturnedPlayers { get; set; }

        protected override void Load()
        {
            UnturnedPlayers = new Dictionary<string, PlayerData>();

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            PlayerLife.OnRevived_Global += OnRevived_Global;
        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            UnturnedPlayers[player.CSteamID.ToString()] = new PlayerData(player);
        }

        private void OnRevived_Global(PlayerLife life)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(life.player);

            PlayerData d_player = UnturnedPlayers[player.CSteamID.ToString()];
            player.Inventory.items[PlayerInventory.SLOTS].resize(d_player.HandWidth, d_player.HandHeight);
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            PlayerLife.OnRevived_Global -= OnRevived_Global;
        }
    }
}
