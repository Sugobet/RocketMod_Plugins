using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;

namespace SAdminBuild_RocketMod1
{
    public class PluginMain : RocketPlugin<MyPluginConfiguration>
    {
        protected override void Load()
        {
            // BarricadeManager.onDeployBarricadeRequested += OnDeployBarricadeRequested;
            BarricadeManager.onDamageBarricadeRequested += OnDamageBarricadeRequested;
            StructureManager.onDamageStructureRequested += OnDamageStructureRequested;

            Logger.Log("SAdminBuild_RocketMod v1.0.0 loaded Author: Sugobet");
        }

        protected override void Unload()
        {
            BarricadeManager.onDamageBarricadeRequested -= OnDamageBarricadeRequested;
            StructureManager.onDamageStructureRequested -= OnDamageStructureRequested;
        }

        private void OnDamageStructureRequested(CSteamID instigatorSteamID, UnityEngine.Transform structureTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            var config = Configuration.Instance;
            StructureDrop sd = StructureManager.FindStructureByRootTransform(structureTransform);
            StructureData ownerData = sd.GetServersideData();
            ulong ownerSteamID = ownerData.owner;

            if (!config.SteamIDs.Contains(ownerSteamID)) { return; }

            UnturnedChat.Say(instigatorSteamID, config.提示);
            pendingTotalDamage = 0;
            shouldAllow = false;
        }

        private void OnDamageBarricadeRequested(CSteamID instigatorSteamID, UnityEngine.Transform barricadeTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            var config = Configuration.Instance;
            BarricadeDrop bd = BarricadeManager.FindBarricadeByRootTransform(barricadeTransform);
            BarricadeData ownerData = bd.GetServersideData();
            ulong ownerSteamID = ownerData.owner;

            if (!config.SteamIDs.Contains(ownerSteamID)) { return; }

            UnturnedChat.Say(new CSteamID(ownerSteamID), config.提示);
            pendingTotalDamage = 0;
            shouldAllow = false;
        }

    }
}
