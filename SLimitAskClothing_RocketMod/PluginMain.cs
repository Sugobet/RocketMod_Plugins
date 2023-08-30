using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using static Rocket.Unturned.Events.UnturnedEvents;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using Logger = Rocket.Core.Logging.Logger;

namespace SLimitAskClothing_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        private Dictionary<string, PlayerClothing> PlayerClothings;

        protected override void Load()
        {
            PlayerClothings = new Dictionary<string, PlayerClothing>();

            U.Events.OnPlayerConnected += OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDead += OnPlayerDead;
            PlayerClothing.OnHatChanged_Global += OnHatChanged_Global;
            PlayerClothing.OnShirtChanged_Global += OnShirtChanged_Global;
            PlayerClothing.OnPantsChanged_Global += OnPantsChanged_Global;

            Logger.Log("SLimitAskClothing v1.0.0 loead    Author: Sugobet");
        }

        protected override void Unload()
        {
            U.Events.OnPlayerConnected -= OnPlayerConnected;
            UnturnedPlayerEvents.OnPlayerDead -= OnPlayerDead;
            PlayerClothing.OnHatChanged_Global -= OnHatChanged_Global;
            PlayerClothing.OnShirtChanged_Global -= OnShirtChanged_Global;
            PlayerClothing.OnPantsChanged_Global -= OnPantsChanged_Global;
        }


        private void OnPlayerConnected(UnturnedPlayer player)
        {
            PlayerClothings[player.CSteamID.ToString()] = player.Player.clothing;
        }

        private void OnPlayerDead(UnturnedPlayer player, UnityEngine.Vector3 position)
        {
            PlayerClothings[player.CSteamID.ToString()] = player.Player.clothing;
        }

        private void OnHatChanged_Global(PlayerClothing clothing)
        {
            if (clothing.hat == 0) { return; }

            PluginConfig config = Configuration.Instance;

            string hatIsTZ = "";
            string shirtIsTZ = "";
            string pantIsTZ = "";
            foreach (MyClothing myClothing in config.套装)
            {
                if (clothing.hat == myClothing.hatID)
                {
                    hatIsTZ = myClothing.name;
                }

                if (clothing.shirt == myClothing.shirtID)
                {
                    shirtIsTZ = myClothing.name;
                }

                if (clothing.pants == myClothing.pantID)
                {
                    pantIsTZ = myClothing.name;
                }
            }
            if (clothing.shirt == 0) { shirtIsTZ = hatIsTZ; }
            if (clothing.pants == 0) { pantIsTZ = hatIsTZ; }

            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromPlayer(clothing.player);
            var pc = PlayerClothings[unturnedPlayer.CSteamID.ToString()];

            // hat不属于套装
            if (hatIsTZ == "")
            {
                if (shirtIsTZ != "")
                {
                    clothing.askWearHat(pc.hat, pc.hatQuality, pc.hatState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该帽子，因为它不属于{shirtIsTZ}", Color.red, false);
                    return;
                }
                if (pantIsTZ != "")
                {
                    clothing.askWearHat(pc.hat, pc.hatQuality, pc.hatState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该帽子，因为它不属于{pantIsTZ}", Color.red, false);
                    return;
                }
            }

            // hat属于套装
            if (hatIsTZ != "")
            {

                if (shirtIsTZ != hatIsTZ)
                {
                    clothing.askWearHat(pc.hat, pc.hatQuality, pc.hatState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该帽子，因为它不属于{shirtIsTZ}", Color.red, false);
                    return;
                }
                if (pantIsTZ != hatIsTZ)
                {
                    clothing.askWearHat(pc.hat, pc.hatQuality, pc.hatState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该帽子，因为它不属于{pantIsTZ}", Color.red, false);
                    return;
                }
            }
        }


        private void OnShirtChanged_Global(PlayerClothing clothing)
        {
            if (clothing.shirt == 0) { return; }

            PluginConfig config = Configuration.Instance;

            string hatIsTZ = "";
            string shirtIsTZ = "";
            string pantIsTZ = "";
            foreach (MyClothing myClothing in config.套装)
            {
                if (clothing.hat == myClothing.hatID)
                {
                    hatIsTZ = myClothing.name;
                }

                if (clothing.shirt == myClothing.shirtID)
                {
                    shirtIsTZ = myClothing.name;
                }

                if (clothing.pants == myClothing.pantID)
                {
                    pantIsTZ = myClothing.name;
                }
            }
            if (clothing.hat == 0) { hatIsTZ = shirtIsTZ; }
            if (clothing.pants == 0) { pantIsTZ = shirtIsTZ; }

            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromPlayer(clothing.player);
            var pc = PlayerClothings[unturnedPlayer.CSteamID.ToString()];

            // shirt不属于套装
            if (shirtIsTZ == "")
            {
                if (hatIsTZ != "")
                {
                    clothing.askWearShirt(pc.shirt, pc.shirtQuality, pc.shirtState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该衬衫，因为它不属于{hatIsTZ}", Color.red, false);
                    return;
                }
                if (pantIsTZ != "")
                {
                    clothing.askWearShirt(pc.shirt, pc.shirtQuality, pc.shirtState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该衬衫，因为它不属于{pantIsTZ}", Color.red, false);
                    return;
                }
            }

            // shirt属于套装
            if (shirtIsTZ != "")
            {

                if (hatIsTZ != shirtIsTZ)
                {
                    clothing.askWearShirt(pc.shirt, pc.shirtQuality, pc.shirtState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该衬衫，因为它不属于{hatIsTZ}", Color.red, false);
                    return;
                }
                if (pantIsTZ != shirtIsTZ)
                {
                    clothing.askWearShirt(pc.shirt, pc.shirtQuality, pc.shirtState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该衬衫，因为它不属于{pantIsTZ}", Color.red, false);
                    return;
                }
            }
        }


        private void OnPantsChanged_Global(PlayerClothing clothing)
        {
            if (clothing.shirt == 0) { return; }

            PluginConfig config = Configuration.Instance;

            string hatIsTZ = "";
            string shirtIsTZ = "";
            string pantIsTZ = "";
            foreach (MyClothing myClothing in config.套装)
            {
                if (clothing.hat == myClothing.hatID)
                {
                    hatIsTZ = myClothing.name;
                }

                if (clothing.shirt == myClothing.shirtID)
                {
                    shirtIsTZ = myClothing.name;
                }

                if (clothing.pants == myClothing.pantID)
                {
                    pantIsTZ = myClothing.name;
                }
            }
            if (clothing.hat == 0) { hatIsTZ = pantIsTZ; }
            if (clothing.shirt == 0) { shirtIsTZ = pantIsTZ; }

            UnturnedPlayer unturnedPlayer = UnturnedPlayer.FromPlayer(clothing.player);
            var pc = PlayerClothings[unturnedPlayer.CSteamID.ToString()];

            // pant不属于套装
            if (pantIsTZ == "")
            {
                if (hatIsTZ != "")
                {
                    clothing.askWearPants(pc.pants, pc.pantsQuality, pc.pantsState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该裤子，因为它不属于{hatIsTZ}", Color.red, false);
                    return;
                }
                if (shirtIsTZ != "")
                {
                    clothing.askWearPants(pc.pants, pc.pantsQuality, pc.pantsState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该裤子，因为它不属于{shirtIsTZ}", Color.red, false);
                    return;
                }
            }

            // pant属于套装
            if (pantIsTZ != "")
            {

                if (hatIsTZ != pantIsTZ)
                {
                    clothing.askWearPants(pc.pants, pc.pantsQuality, pc.pantsState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该裤子，因为它不属于{hatIsTZ}", Color.red, false);
                    return;
                }
                if (shirtIsTZ != pantIsTZ)
                {
                    clothing.askWearPants(pc.pants, pc.pantsQuality, pc.pantsState, false);
                    UnturnedChat.Say(unturnedPlayer, $"无法装备该裤子，因为它不属于{shirtIsTZ}", Color.red, false);
                    return;
                }
            }
        }
    }
}
