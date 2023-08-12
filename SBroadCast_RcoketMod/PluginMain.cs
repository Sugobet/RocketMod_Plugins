using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBroadCast_RcoketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        public static PluginMain Instance;
        public static PluginConfig Config;

        protected override void Load()
        {
            Instance = this;
            Config = Configuration.Instance;

            Logger.Log("SBroadCast_RocketMod v1.0.0 loaded    Author: Sugobet");
        }
    }
}
