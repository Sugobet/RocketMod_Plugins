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


        protected override void Load()
        {
            PlayerTZ = new Dictionary<string, MyClothing>();

            PlayerClothing.OnShirtChanged_Global += OnShirtChanged_Global;

            U.Events.OnPlayerConnected += OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath += OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead += OnPlayerDead;

            Logger.Log("SLimitAskClothing v1.0.0 loead    Author: Sugobet");
        }

        protected override void Unload()
        {
            PlayerClothing.OnShirtChanged_Global -= OnShirtChanged_Global;

            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDeath -= OnPlayerDeath;
            UnturnedPlayerEvents.OnPlayerDead -= OnPlayerDead;
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

        private void OnShirtChanged_Global(PlayerClothing clothing)
        {

            PluginConfig config = Configuration.Instance;
            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromPlayer(clothing.player);

            if (PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtID == clothing.shirt)
            {
                return;
            }

            if (clothing.shirt == 0)
            {
                foreach (MyClothing myClothing in config.套装)
                {
                    if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtID == myClothing.shirtID) && (myClothing.main == "shirt"))
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

                        PlayerTZ[unturnedPlayer.CSteamID.ToString()] = new MyClothing(clothing.player.clothing);
                        return;
                    }
                }
            }

            foreach (MyClothing myClothing in config.套装)
            {
                if ((clothing.shirt == myClothing.shirtID) && (myClothing.main == "shirt"))
                {
                    foreach (MyClothing myClothing1 in config.套装)
                    {
                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].maskID == myClothing1.maskID) && (myClothing1.main == "mask"))
                        {
                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].hatID == myClothing1.hatID) && (myClothing1.main == "hat"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtID == myClothing1.shirtID) && (myClothing1.main == "shirt"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].pantID == myClothing1.pantID) && (myClothing1.main == "pant"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].vestID == myClothing1.vestID) && (myClothing1.main == "vest"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].glassesID == myClothing1.glassesID) && (myClothing1.main == "glasses"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.backpackID != 0)
                            {
                                clothing.thirdClothes.backpack = 0;
                                clothing.askWearBackpack(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }
                            break;
                        }

                        if ((PlayerTZ[unturnedPlayer.CSteamID.ToString()].backpackID == myClothing1.backpackID) && (myClothing1.main == "backpack"))
                        {
                            if (myClothing1.maskID != 0)
                            {
                                clothing.thirdClothes.mask = 0;
                                clothing.askWearMask(0, 0, new byte[0], false);
                            }

                            if (myClothing1.shirtID != 0)
                            {
                                clothing.thirdClothes.shirt = 0;
                                clothing.askWearShirt(0, 0, new byte[0], false);
                            }

                            if (myClothing1.hatID != 0)
                            {
                                clothing.thirdClothes.hat = 0;
                                clothing.askWearHat(0, 0, new byte[0], false);
                            }

                            if (myClothing1.pantID != 0)
                            {
                                clothing.thirdClothes.pants = 0;
                                clothing.askWearPants(0, 0, new byte[0], false);
                            }

                            if (myClothing1.glassesID != 0)
                            {
                                clothing.thirdClothes.glasses = 0;
                                clothing.askWearGlasses(0, 0, new byte[0], false);
                            }

                            if (myClothing1.vestID != 0)
                            {
                                clothing.thirdClothes.vest = 0;
                                clothing.askWearVest(0, 0, new byte[0], false);
                            }
                            break;
                        }

                    }

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

                    PlayerTZ[unturnedPlayer.CSteamID.ToString()] = new MyClothing(clothing.player.clothing);
                    return;
                }
            }


            bool beacon = false;
            foreach (MyClothing myClothing1 in config.套装)
            {
                if ((clothing.shirt == myClothing1.shirtID) && (myClothing1.main == "shirt"))
                {
                    beacon = true;
                    break;
                }
            }
            if (!beacon)
            {
                clothing.askWearShirt(PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtID,
                    PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtQuality,
                    PlayerTZ[unturnedPlayer.CSteamID.ToString()].shirtState, false);
                return;
            }

            PlayerTZ[unturnedPlayer.CSteamID.ToString()] = new MyClothing(clothing.player.clothing);
        }
    }
}
