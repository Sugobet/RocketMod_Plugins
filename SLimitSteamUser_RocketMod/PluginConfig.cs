using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLimitSteamUser_RocketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public string SteamKey { get; set; }
        public bool 启用存在VAC记录限制进服 { get; set; }
        public int Steam等级限制进服 { get; set; }
        public int Unturned游戏时长限制 { get; set; }
        public bool 是否禁止私密账号进服 { get; set; }
        public bool 是否禁止游戏时长为私密进服 { get; set; }

        public void LoadDefaults()
        {
            SteamKey = "请到这里注册Steam Key：https://steamcommunity.com/dev/registerkey";
            启用存在VAC记录限制进服 = false;
            Steam等级限制进服 = -1;
            Unturned游戏时长限制 = -1;
            是否禁止私密账号进服 = false;
            是否禁止游戏时长为私密进服 = false;
        }
    }
}
