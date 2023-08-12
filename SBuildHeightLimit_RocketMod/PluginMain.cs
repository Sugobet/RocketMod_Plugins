using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Chat;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SBuildHeightLimit_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        protected override void Load()
        {
            BarricadeManager.onDeployBarricadeRequested += OnDeployBarricadeRequested;
            StructureManager.onDeployStructureRequested += OnDeployStructureRequested;

            Logger.Log("SBuildHeightLimit_RocketMod v1.0.0 loaded    Author: Sugobet");
        }

        protected override void Unload()
        {
            BarricadeManager.onDeployBarricadeRequested -= OnDeployBarricadeRequested;
            StructureManager.onDeployStructureRequested -= OnDeployStructureRequested;
        }

        private void OnDeployStructureRequested(Structure structure, ItemStructureAsset asset, ref Vector3 point, ref float angle_x, ref float angle_y, ref float angle_z, ref ulong owner, ref ulong group, ref bool shouldAllow)
        {
            var config = Configuration.Instance;
            short s_y = Convert.ToInt16(point.y);

            if (s_y >= config.高度限制)
            {
                UnturnedChat.Say(new CSteamID(owner), "此高度禁止放置建筑");
                shouldAllow = false;
                return;
            }

            short preHeight = Convert.ToInt16(config.高度限制 * config.高度限制提示临界点比例);
            if (s_y >= preHeight)
            {
                UnturnedChat.Say(new CSteamID(owner), $"当前高度：{s_y}米，再向上{config.高度限制 - s_y}米后将无法放置任何建筑");
            }
        }

        private void OnDeployBarricadeRequested(Barricade barricade, ItemBarricadeAsset asset, Transform hit, ref Vector3 point, ref float angle_x, ref float angle_y, ref float angle_z, ref ulong owner, ref ulong group, ref bool shouldAllow)
        {
            var config = Configuration.Instance;
            short b_y = Convert.ToInt16(point.y);

            if (b_y >= config.高度限制)
            {
                UnturnedChat.Say(new CSteamID(owner), "此高度禁止放置建筑");
                shouldAllow = false;
                return;
            }

            short preHeight = Convert.ToInt16(config.高度限制 * config.高度限制提示临界点比例);
            if (b_y >= preHeight)
            {
                UnturnedChat.Say(new CSteamID(owner), $"当前高度：{b_y}米，再向上{config.高度限制 - b_y}米后将无法放置任何建筑");
            }
        }
    }
}
