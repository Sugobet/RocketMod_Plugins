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
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Rocket.Unturned.Events.UnturnedEvents;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using static SDG.Provider.UnturnedEconInfo;
using static UnityEngine.Random;
using Logger = Rocket.Core.Logging.Logger;

namespace SLimitAskClothing_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        private Dictionary<string, MyClothing> PlayerTZ;

        public static  PluginConfig Config { get; private set; }


        protected override void Load()
        {
            Config = Configuration.Instance;
            PlayerTZ = new Dictionary<string, MyClothing>();

            UnturnedPlayerEvents.OnPlayerWear += OnPlayerWear;

            U.Events.OnPlayerConnected += OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead += OnPlayerDead;

            Logger.Log("SLimitAskClothing v1.0.0 loead    Author: Sugobet");
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerWear -= OnPlayerWear;

            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead -= OnPlayerDead;
        }

        private void OnPlayerWear(UnturnedPlayer player, Wearables wear, ushort id, byte? quality)
        {
            if (PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return;
            }
            PlayerTZ[player.CSteamID.ToString()].issing = true;

            PluginConfig config = Configuration.Instance;
            PlayerClothing clothing = player.Player.clothing;


            // 脱衣服， 判断脱之前是否穿的是套装
            if (id == 0)
            {
                foreach (MyClothing myClothing in config.套装)
                {

                    if ((PlayerTZ[player.CSteamID.ToString()].shirtID == myClothing.shirtID) && (myClothing.main == "shirt"))
                    {
                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }

                        // 将上衣脱下
                        // 待修复
                        clothing.askWearShirt(0, 0, new byte[0], false);

                        PlayerTZ[player.CSteamID.ToString()] = new MyClothing(clothing.player.clothing);
                        PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return;
                    }
                }
            }


            // 穿衣服
            MyClothing myClothing1 = new MyClothing(clothing);
            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的衣服不属于套装，且穿之前的衣服是套装 则换回
                if ((myClothing1.main == "") && (PlayerTZ[player.CSteamID.ToString()].main != ""))
                {
                    // 换回后从背包删除物品
                    MyClothing pTZ = PlayerTZ[player.CSteamID.ToString()];
                    InventorySearch inventorySearch = null;
                    if (wear.ToString() == "Hat")
                    {
                        clothing.askWearHat(pTZ.hatID, pTZ.hatQuality, pTZ.hatState, false);
                        inventorySearch = player.Inventory.has(pTZ.hatID);
                    }
                    if (wear.ToString() == "Mask")
                    {
                        clothing.askWearMask(pTZ.maskID, pTZ.maskQuality, pTZ.maskState, false);
                        inventorySearch = player.Inventory.has(pTZ.maskID);
                    }
                    if (wear.ToString() == "Vest")
                    {
                        clothing.askWearVest(pTZ.vestID, pTZ.vestQuality, pTZ.vestState, false);
                        inventorySearch = player.Inventory.has(pTZ.vestID);
                    }
                    if (wear.ToString() == "Pants")
                    {
                        clothing.askWearPants(pTZ.pantID, pTZ.pantsQuality, pTZ.pantsState, false);
                        inventorySearch = player.Inventory.has(pTZ.pantID);
                    }
                    if (wear.ToString() == "Shirt")
                    {
                        clothing.askWearShirt(pTZ.shirtID, pTZ.shirtQuality, pTZ.shirtState, false);
                        inventorySearch = player.Inventory.has(pTZ.shirtID);
                    }
                    if (wear.ToString() == "Glasses")
                    {
                        clothing.askWearGlasses(pTZ.glassesID, pTZ.glassesQuality, pTZ.glassesState, false);
                        inventorySearch = player.Inventory.has(pTZ.glassesID);
                    }
                    if (wear.ToString() == "Backpack")
                    {
                        clothing.askWearBackpack(pTZ.backpackID, pTZ.backpackQuality, pTZ.backpackState, false);
                        inventorySearch = player.Inventory.has(pTZ.backpackID);
                    }

                    // player.Inventory.forceAddItem();
                    // 待修复
                    // player.Inventory.ReceiveItemRemove(inventorySearch.page, inventorySearch.jar.x, inventorySearch.jar.y);
                    
                    PlayerTZ[player.CSteamID.ToString()] = new MyClothing(player.Player.clothing.player.clothing);
                    PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return;
                }


                // 穿之前的衣服是套装，则处理 只剩套装核心
                if (PlayerTZ[player.CSteamID.ToString()].main != "")
                {
                    if ((PlayerTZ[player.CSteamID.ToString()].maskID == myClothing.maskID) && (myClothing.main == "mask"))
                    {
                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].hatID == myClothing.hatID) && (myClothing.main == "hat"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].shirtID == myClothing.shirtID) && (myClothing.main == "shirt"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].pantID == myClothing.pantID) && (myClothing.main == "pant"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].vestID == myClothing.vestID) && (myClothing.main == "vest"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].glassesID == myClothing.glassesID) && (myClothing.main == "glasses"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.backpackID != 0)
                        {
                            clothing.thirdClothes.backpack = 0;
                            clothing.askWearBackpack(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }
                    }

                    if ((PlayerTZ[player.CSteamID.ToString()].backpackID == myClothing.backpackID) && (myClothing.main == "backpack"))
                    {
                        if (myClothing.maskID != 0)
                        {
                            clothing.thirdClothes.mask = 0;
                            clothing.askWearMask(0, 0, new byte[0], false);
                        }

                        if (myClothing.shirtID != 0)
                        {
                            clothing.thirdClothes.shirt = 0;
                            clothing.askWearShirt(0, 0, new byte[0], false);
                        }

                        if (myClothing.hatID != 0)
                        {
                            clothing.thirdClothes.hat = 0;
                            clothing.askWearHat(0, 0, new byte[0], false);
                        }

                        if (myClothing.pantID != 0)
                        {
                            clothing.thirdClothes.pants = 0;
                            clothing.askWearPants(0, 0, new byte[0], false);
                        }

                        if (myClothing.glassesID != 0)
                        {
                            clothing.thirdClothes.glasses = 0;
                            clothing.askWearGlasses(0, 0, new byte[0], false);
                        }

                        if (myClothing.vestID != 0)
                        {
                            clothing.thirdClothes.vest = 0;
                            clothing.askWearVest(0, 0, new byte[0], false);
                        }
                    }
                }


                // 穿的是上衣，且是基于上衣的套装
                if ((wear.ToString() == "Shirt") && (clothing.shirt == myClothing.shirtID) && (myClothing.main == "shirt"))
                {
                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, clothing.shirtQuality, clothing.shirtState, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, clothing.shirtQuality, clothing.shirtState, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, clothing.shirtQuality, clothing.shirtState, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, clothing.shirtQuality, clothing.shirtState, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, clothing.shirtQuality, clothing.shirtState, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, clothing.shirtQuality, clothing.shirtState, false);
                    }
                }
            }


            PlayerTZ[player.CSteamID.ToString()] = new MyClothing(player.Player.clothing.player.clothing);
            PlayerTZ[player.CSteamID.ToString()].issing = false;
        }

        private void OnPlayerConnected(UnturnedPlayer player)
        {
            PlayerTZ[player.CSteamID.ToString()] = new MyClothing(player.Player.clothing);
        }

        private void OnPlayerDead(UnturnedPlayer player, Vector3 position)
        {
            PlayerTZ[player.CSteamID.ToString()] = new MyClothing(player.Player.clothing);
        }

        private void OnPlayerDeath(UnturnedPlayer player, EDeathCause cause, ELimb limb, CSteamID murderer)
        {
            PluginConfig config = Configuration.Instance;

            foreach (MyClothing myClothing in config.套装)
            {
                if ((player.Player.clothing.mask == myClothing.maskID) && (myClothing.main == "mask"))
                {
                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.hat == myClothing.hatID) && (myClothing.main == "hat"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.shirt == myClothing.shirtID) && (myClothing.main == "shirt"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.pants == myClothing.pantID) && (myClothing.main == "pant"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.vest == myClothing.vestID) && (myClothing.main == "vest"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.glasses == myClothing.glassesID) && (myClothing.main == "glasses"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        player.Player.clothing.thirdClothes.backpack = 0;
                        player.Player.clothing.askWearBackpack(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }
                    return;
                }

                if ((player.Player.clothing.backpack == myClothing.backpackID) && (myClothing.main == "backpack"))
                {
                    if (myClothing.maskID != 0)
                    {
                        player.Player.clothing.thirdClothes.mask = 0;
                        player.Player.clothing.askWearMask(0, 0, new byte[0], false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        player.Player.clothing.thirdClothes.shirt = 0;
                        player.Player.clothing.askWearShirt(0, 0, new byte[0], false);
                    }

                    if (myClothing.hatID != 0)
                    {
                        player.Player.clothing.thirdClothes.hat = 0;
                        player.Player.clothing.askWearHat(0, 0, new byte[0], false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        player.Player.clothing.thirdClothes.pants = 0;
                        player.Player.clothing.askWearPants(0, 0, new byte[0], false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        player.Player.clothing.thirdClothes.glasses = 0;
                        player.Player.clothing.askWearGlasses(0, 0, new byte[0], false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        player.Player.clothing.thirdClothes.vest = 0;
                        player.Player.clothing.askWearVest(0, 0, new byte[0], false);
                    }
                    return;
                }
            }
        }

    }
}
