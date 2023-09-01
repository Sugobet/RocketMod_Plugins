using HarmonyLib;
using JetBrains.Annotations;
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
using System.Diagnostics.Eventing;
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
        public static Dictionary<string, MyClothing> PlayerTZ;

        public static  PluginConfig Config { get; private set; }

        private Harmony harmony;


        protected override void Load()
        {
            harmony = new Harmony(Name);
            harmony.PatchAll();

            Config = Configuration.Instance;
            PlayerTZ = new Dictionary<string, MyClothing>();

            // UnturnedPlayerEvents.OnPlayerWear += OnPlayerWear;

            U.Events.OnPlayerConnected += OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead += OnPlayerDead;

            Logger.Log("SLimitAskClothing v1.0.0 loead    Author: Sugobet");
        }

        protected override void Unload()
        {
            harmony.UnpatchAll();

            // UnturnedPlayerEvents.OnPlayerWear -= OnPlayerWear;

            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead -= OnPlayerDead;
        }

        public static void ClrClothing(PlayerClothing clothing, MyClothing myClothing)
        {
            /*
             * 判断套装核心 并处理到只剩套装核心,且将套装核心换下
             * myClothing must is PluginConfig instance
             */
            if (myClothing.main == "shirt")
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
                clothing.askWearShirt(0, 0, new byte[0], false);
            }

            if (myClothing.main == "hat")
            {
                if (myClothing.shirtID != 0)
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
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
                clothing.askWearHat(0, 0, new byte[0], false);
            }

            if (myClothing.main == "pant")
            {
                if (myClothing.hatID != 0)
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if (myClothing.shirtID != 0)
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
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
                clothing.askWearPants(0, 0, new byte[0], false);
            }

            if (myClothing.main == "mask")
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
                clothing.askWearMask(0, 0, new byte[0], false);
            }

            if (myClothing.main == "vesk")
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

                if (myClothing.shirtID != 0)
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
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
                clothing.askWearVest(0, 0, new byte[0], false);
            }

            if (myClothing.main == "backpack")
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

                if (myClothing.shirtID != 0)
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if (myClothing.glassesID != 0)
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }
                clothing.askWearBackpack(0, 0, new byte[0], false);
            }

            if (myClothing.main == "glasses")
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

                if (myClothing.shirtID != 0)
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }
                clothing.askWearGlasses(0, 0, new byte[0], false);
            }
        }

        public static void ClrClothing1(PlayerClothing clothing, MyClothing myClothing, MyClothing nextClothing)
        {
            /*
             * 判断套装核心 并处理到只剩套装核心,且将套装核心换下1
             * myClothing must is PluginConfig instance1
             */
            if (myClothing.main == "shirt")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearShirt(0, 0, new byte[0], false);
            }

            if (myClothing.main == "hat")
            {
                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearHat(0, 0, new byte[0], false);
            }

            if (myClothing.main == "pant")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearPants(0, 0, new byte[0], false);
            }

            if (myClothing.main == "mask")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearMask(0, 0, new byte[0], false);
            }

            if (myClothing.main == "vest")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearVest(0, 0, new byte[0], false);
            }

            if (myClothing.main == "backpack")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                if ((myClothing.glassesID != 0) && (nextClothing.glassesID == 0))
                {
                    clothing.thirdClothes.glasses = 0;
                    clothing.askWearGlasses(0, 0, new byte[0], false);
                }

                clothing.askWearBackpack(0, 0, new byte[0], false);
            }

            if (myClothing.main == "glasses")
            {
                if ((myClothing.hatID != 0) && (nextClothing.hatID == 0))
                {
                    clothing.thirdClothes.hat = 0;
                    clothing.askWearHat(0, 0, new byte[0], false);
                }

                if ((myClothing.pantID != 0) && (nextClothing.pantID == 0))
                {
                    clothing.thirdClothes.pants = 0;
                    clothing.askWearPants(0, 0, new byte[0], false);
                }

                if ((myClothing.maskID != 0) && (nextClothing.maskID == 0))
                {
                    clothing.thirdClothes.mask = 0;
                    clothing.askWearMask(0, 0, new byte[0], false);
                }

                if ((myClothing.vestID != 0) && (nextClothing.vestID == 0))
                {
                    clothing.thirdClothes.vest = 0;
                    clothing.askWearVest(0, 0, new byte[0], false);
                }

                if ((myClothing.backpackID != 0) && (nextClothing.backpackID == 0))
                {
                    clothing.thirdClothes.backpack = 0;
                    clothing.askWearBackpack(0, 0, new byte[0], false);
                }

                if ((myClothing.shirtID != 0) && (nextClothing.shirtID == 0))
                {
                    clothing.thirdClothes.shirt = 0;
                    clothing.askWearShirt(0, 0, new byte[0], false);
                }

                clothing.askWearGlasses(0, 0, new byte[0], false);
            }
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
