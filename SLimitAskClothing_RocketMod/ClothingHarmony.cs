using HarmonyLib;
using JetBrains.Annotations;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SLimitAskClothing_RocketMod
{
    [UsedImplicitly]
    [HarmonyPatch]
    public class ClothingHarmony
    {

        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearBackpack),
                typeof(ItemBackpackAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearBackpack(PlayerClothing __instance, ref ItemBackpackAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱backpack
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.backpack == myClothing.backpackID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿backpack
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.backpackID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的backpack不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("backpack") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于backpack的套装
                if ((myClothing1.backpackID == myClothing.backpackID) && (myClothing.main == "backpack"))
                {
                    // 穿之前的backpack是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearGlasses),
            typeof(ItemGlassesAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearGlasses(PlayerClothing __instance, ItemGlassesAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱glasses
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.glasses == myClothing.glassesID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿glasses
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.glassesID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的glasses不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("glasses") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于glasses的套装
                if ((myClothing1.glassesID == myClothing.glassesID) && (myClothing.main == "glasses"))
                {
                    // 穿之前的glasses是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearHat),
            typeof(ItemHatAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearHat(PlayerClothing __instance, ItemHatAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱hat
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.hat == myClothing.hatID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿hat
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.hatID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的hat不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("hat") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于hat的套装
                if ((myClothing1.hatID == myClothing.hatID) && (myClothing.main == "hat"))
                {
                    // 穿之前的hat是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearMask),
            typeof(ItemMaskAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearMask(PlayerClothing __instance, ItemMaskAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱mask
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.mask == myClothing.maskID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿mask
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.maskID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的mask不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("mask") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于mask的套装
                if ((myClothing1.maskID == myClothing.maskID) && (myClothing.main == "mask"))
                {
                    // 穿之前的mask是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearPants),
            typeof(ItemPantsAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearPants(PlayerClothing __instance, ItemPantsAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱裤子
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.pants == myClothing.pantID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿pants
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.pantID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的pants不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("pant") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于pants的套装
                if ((myClothing1.pantID == myClothing.pantID) && (myClothing.main == "pant"))
                {
                    // 穿之前的pant是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearShirt),
            typeof(ItemShirtAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearShirt(PlayerClothing __instance, ItemShirtAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱上衣
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的衣服属于套装
                    if (clothing.shirt == myClothing.shirtID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿上衣
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.shirtID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的上衣不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("shirt") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于上衣的套装
                if ((myClothing1.shirtID == myClothing.shirtID) && (myClothing.main == "shirt"))
                {
                    // 穿之前的上衣是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.vestID != 0)
                    {
                        clothing.askWearVest(myClothing.vestID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }


        [UsedImplicitly]
        [HarmonyPatch(typeof(PlayerClothing), nameof(PlayerClothing.askWearVest),
            typeof(ItemVestAsset), typeof(byte), typeof(byte[]), typeof(bool))]
        [HarmonyPrefix]
        public static bool OnPreWearVest(PlayerClothing __instance, ItemVestAsset asset, byte quality, byte[] state)
        {
            var cancel = false;

            UnturnedPlayer player = UnturnedPlayer.FromPlayer(__instance.player);
            if (PluginMain.PlayerTZ[player.CSteamID.ToString()].issing)
            {
                return !cancel;
            }
            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = true;


            PluginConfig config = PluginMain.Config;
            PlayerClothing clothing = __instance;

            // 脱护甲
            if (asset == null)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    // 判断脱之前的护甲属于套装
                    if (clothing.vest == myClothing.vestID)
                    {
                        //判断套装核心 并处理到只剩套装核心,且将套装核心换下
                        PluginMain.ClrClothing(clothing, myClothing);
                        cancel = true;

                        PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                        return !cancel;
                    }
                }
                return !cancel;
            }

            // 穿vest
            MyClothing myClothing1 = new MyClothing(clothing);
            myClothing1.vestID = asset.id;

            foreach (MyClothing myClothing in config.套装)
            {
                // 新穿的vest不属于套装核心，且穿之前的衣服是套装 则禁止穿
                if ((myClothing1.askCore("vest") == "") && (new MyClothing(clothing).main != ""))
                {
                    cancel = true;
                    player.Inventory.forceAddItem(new Item(asset, EItemOrigin.NATURE), true);

                    PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
                    return !cancel;
                }


                // 穿的是基于vest的套装
                if ((myClothing1.vestID == myClothing.vestID) && (myClothing.main == "vest"))
                {
                    // 穿之前的vest是套装，则处理 只剩套装核心
                    if (new MyClothing(clothing).main != "")
                    {
                        var val = new MyClothing(clothing).main;
                        foreach (MyClothing newmyClothing in config.套装)
                        {
                            if (val == newmyClothing.main)
                            {
                                PluginMain.ClrClothing1(clothing, newmyClothing, myClothing);
                            }
                        }
                    }

                    if (myClothing.hatID != 0)
                    {
                        clothing.askWearHat(myClothing.hatID, quality, state, false);
                    }

                    if (myClothing.pantID != 0)
                    {
                        clothing.askWearPants(myClothing.pantID, quality, state, false);
                    }

                    if (myClothing.maskID != 0)
                    {
                        clothing.askWearMask(myClothing.maskID, quality, state, false);
                    }

                    if (myClothing.shirtID != 0)
                    {
                        clothing.askWearShirt(myClothing.shirtID, quality, state, false);
                    }

                    if (myClothing.backpackID != 0)
                    {
                        clothing.askWearBackpack(myClothing.backpackID, quality, state, false);
                    }

                    if (myClothing.glassesID != 0)
                    {
                        clothing.askWearGlasses(myClothing.glassesID, quality, state, false);
                    }
                }
            }

            PluginMain.PlayerTZ[player.CSteamID.ToString()].issing = false;
            return !cancel;
        }
    }
}
