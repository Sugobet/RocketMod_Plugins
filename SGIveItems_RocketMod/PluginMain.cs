using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Events;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using UnityEngine;
using static Rocket.Unturned.Events.UnturnedPlayerEvents;
using Logger = Rocket.Core.Logging.Logger;

namespace SGIveItems_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        protected override void Load()
        {
            UnturnedPlayerEvents.OnPlayerRevive += OnPlayerRevive;

            Logger.Log("SGiveItems v1.0.0 loaded Author: Sugobet");
        }

        protected override void Unload()
        {
            UnturnedPlayerEvents.OnPlayerRevive -= OnPlayerRevive;
        }

        private void OnPlayerRevive(UnturnedPlayer player, Vector3 position, byte angle)
        {
            foreach (ushort id in Configuration.Instance.物品列表)
            {
                Thread.Sleep(1000);
                player.Inventory.forceAddItemAuto(new Item(id, EItemOrigin.NATURE), true, true, true);
            }

        }
    }
}
