using Rocket.API;
using System;
using System.Collections.Generic;

namespace SAdminBuild_RocketMod1
{
    public class MyPluginConfiguration : IRocketPluginConfiguration
    {
        public List<ulong> SteamIDs { get; set; }
        public string 提示 { get; set; }

        public void LoadDefaults()
        {
            SteamIDs = new List<ulong> { 0, 0 };
            提示 = "无法破坏该建筑，因为该建筑受到保护";
        }
    }
}