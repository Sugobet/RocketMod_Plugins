using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SBroadCast_RcoketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public int FontSize { get; set; }
        public Color FontColor { get; set; }
        public string Message { get; set; }

        public void LoadDefaults()
        {
            FontSize = 20;
            FontColor = Color.red;
            Message = "后台播报：请 player 五分钟内加群|配合管理员查挂";
        }
    }
}
