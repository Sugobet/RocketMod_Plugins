using Rocket.API;
using Rocket.API.Extensions;
using Rocket.Core.Extensions;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Enumerations;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Rocket.Unturned.Events.UnturnedEvents;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using Logger = Rocket.Core.Logging.Logger;

namespace SNoLoot_RocketMod
{
    public class PluginMain : RocketPlugin
    {
        private Dictionary<string, PlayerData> UnturnedPlayers { get; set; }
        // private Dictionary<string, bool> PlayerDeathState { get; set; }

        protected override void Load()
        {

            UnturnedPlayers = new Dictionary<string, PlayerData>();

            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            PlayerLife.OnRevived_Global += OnRevived_Global;
            U.Events.OnPlayerConnected += OnPlayerConnected;

            Logger.Log("SNoLoot v1.0.5 loaded    Author: Sugobet");
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            if (!UnturnedPlayers.ContainsKey(player.CSteamID.ToString())) { return; }

            Items handsItems = player.Inventory.items[PlayerInventory.SLOTS];
            byte hcount = handsItems.getItemCount();

            if (hcount > 0) { return; }

            PlayerData d_player = UnturnedPlayers[player.CSteamID.ToString()];

            if (!d_player.IsDead) { return; }

            player.Inventory.items[PlayerInventory.SLOTS].resize(d_player.HandWidth, d_player.HandHeight);
            foreach (var itemJar in d_player.handsItemJars)
            {
                player.Inventory.items[PlayerInventory.SLOTS].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
            }


            // 恢复shirt
            player.Player.clothing.askWearShirt(d_player.shirtAsset, d_player.shirtQuality, d_player.shirtState, false);

            // 恢复pants
            player.Player.clothing.askWearPants(d_player.pantsAsset, d_player.pantsQuality, d_player.pantsState, false);
        }

        private void OnRevived_Global(PlayerLife life)
        {

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(life.player);

            PlayerData d_player = UnturnedPlayers[player.CSteamID.ToString()];
            UnturnedPlayers[player.CSteamID.ToString()].IsDead = false;

            player.Inventory.items[PlayerInventory.SLOTS].resize(d_player.HandWidth, d_player.HandHeight);
            foreach (var itemJar in d_player.handsItemJars)
            {
                player.Inventory.items[PlayerInventory.SLOTS].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
            }


            // 恢复shirt
            player.Player.clothing.askWearShirt(d_player.shirtAsset, d_player.shirtQuality, d_player.shirtState, false);

            // 恢复pants
            player.Player.clothing.askWearPants(d_player.pantsAsset, d_player.pantsQuality, d_player.pantsState, false);
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            PlayerLife.OnRevived_Global -= OnRevived_Global;
            U.Events.OnPlayerConnected -= OnPlayerConnected;
        }


/*        private void OnPlayerInventoryResized(UnturnedPlayer player, InventoryGroup inventoryGroup, byte O, byte U)
        {
            if (inventoryGroup != InventoryGroup.Hands) { return; }

            if (!PlayerDeathState.ContainsKey(player.CSteamID.ToString())) { return; }
            if (!PlayerDeathState[player.CSteamID.ToString()]) { return; }

            PlayerDeathState[player.CSteamID.ToString()] = false;

            *//*PlayerData d_player = UnturnedPlayers[player.CSteamID.ToString()];
            foreach (var itemJar in d_player.handsItemJars)
            {
                player.Inventory.items[PlayerInventory.SLOTS].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
            }

            // 恢复shirt
            player.Player.clothing.askWearShirt(d_player.shirtAsset, d_player.shirtQuality, d_player.shirtState, false);

            // 恢复pants
            player.Player.clothing.askWearPants(d_player.pantsAsset, d_player.pantsQuality, d_player.pantsState, false);*//*
        }*/


        // 已死亡
        /*private void OnPlayerDead(UnturnedPlayer player, UnityEngine.Vector3 position)
        {

            // PlayerData d_player = UnturnedPlayers[player.CSteamID.ToString()];

*//*            // 恢复shirt
            player.Player.clothing.askWearShirt(d_player.shirtAsset, d_player.shirtQuality, d_player.shirtState, false);

            // 恢复pants
            player.Player.clothing.askWearPants(d_player.pantsAsset, d_player.pantsQuality, d_player.pantsState, false);
*/
            /*            // 恢复 shirt items
                        foreach (var itemJar in d_player.shirtItemJar)
                        {
                            player.Inventory.items[PlayerInventory.SHIRT].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
                        }

                        // 恢复 pant items
                        foreach (var itemJar in d_player.pantsItemJar)
                        {
                            player.Inventory.items[PlayerInventory.PANTS].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
                        }*//*

            // 恢复hands items
            *//*            foreach (var itemJar in d_player.handsItemJars)
                        {
                            player.Inventory.items[PlayerInventory.SLOTS].addItem(itemJar.x, itemJar.y, itemJar.rot, itemJar.item);
                        }*//*
        }*/


        // 死亡前
        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            UnturnedPlayers[player.CSteamID.ToString()] = new PlayerData(player, true);
/*            Items shirtItems = player.Inventory.items[PlayerInventory.SHIRT];
            byte scount = shirtItems.getItemCount();
            for (byte index = 0; index < scount; index++)
            {
                shirtItems.removeItem(0);
            }

            Items pantsItems = player.Inventory.items[PlayerInventory.PANTS];
            byte pcount = pantsItems.getItemCount();
            for (byte index = 0; index < pcount; index++)
            {
                pantsItems.removeItem(0);
            }*/

            player.Player.clothing.thirdClothes.shirt = 0;
            player.Player.clothing.thirdClothes.pants = 0;

            Items handsItems = player.Inventory.items[PlayerInventory.SLOTS];
            byte hcount = handsItems.getItemCount();
            for (byte index = 0; index < hcount; index++)
            {
                handsItems.removeItem(0);
            }
        }
    }
}
