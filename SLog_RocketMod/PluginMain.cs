using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SLog_RocketMod
{
    public class PluginMain : RocketPlugin
    {
        public static Dictionary<string, List<uint>> PlayerDropedItems { get; set; }

        protected override void Load()
        {
            PlayerDropedItems = new Dictionary<string, List<uint>>();

            string dirPath = Path.Combine(System.Environment.CurrentDirectory, "SLog");
            if (!System.IO.Directory.Exists(dirPath))
            {
                System.IO.Directory.CreateDirectory(dirPath);
            }

            BarricadeManager.onDamageBarricadeRequested += Events.OnDamageBarricadeRequested;
            StructureManager.onDamageStructureRequested += Events.OnDamageStructureRequested;
            VehicleManager.onEnterVehicleRequested += Events.OnEnterVehicleRequested;
            VehicleManager.onExitVehicleRequested += Events.OnExitVehicleRequested;
            VehicleManager.onDamageVehicleRequested += Events.OnDamageVehicleRequested;

            Logger.Log("SLog_RocketMod v1.0.0 loaded    Author: Sugobet");
        }


        protected override void Unload()
        {
            BarricadeManager.onDamageBarricadeRequested -= Events.OnDamageBarricadeRequested;
            StructureManager.onDamageStructureRequested -= Events.OnDamageStructureRequested;
            VehicleManager.onEnterVehicleRequested -= Events.OnEnterVehicleRequested;
            VehicleManager.onExitVehicleRequested -= Events.OnExitVehicleRequested;
            VehicleManager.onDamageVehicleRequested -= Events.OnDamageVehicleRequested;
        }
    }
}
