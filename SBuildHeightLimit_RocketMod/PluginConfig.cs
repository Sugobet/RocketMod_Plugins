using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SBuildHeightLimit_RocketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public short 高度限制 { get; set; }
        public double 高度限制提示临界点比例 { get; set; }

        public void LoadDefaults()
        {
            高度限制 = 400;
            高度限制提示临界点比例 = 0.9;
        }
    }
}
