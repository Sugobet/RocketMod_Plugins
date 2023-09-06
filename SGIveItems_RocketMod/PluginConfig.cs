using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SGIveItems_RocketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public List<ushort> 物品列表;

        public void LoadDefaults()
        {
            物品列表 = new List<ushort> { 18004 };
        }
    }
}
