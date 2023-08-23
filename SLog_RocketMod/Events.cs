using MyOpenModPlugin;
using Rocket.Unturned.Items;
using Rocket.Unturned.Player;
using SDG.Unturned;
using Steamworks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace SLog_RocketMod
{
    public class Events
    {
        public static void OnDamageBarricadeRequested(CSteamID instigatorSteamID, Transform barricadeTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            BarricadeDrop barricadeDrop = BarricadeManager.FindBarricadeByRootTransform(barricadeTransform);
            ushort barricadeHealth = barricadeDrop.GetServersideData().barricade.health;
            
            if ((barricadeHealth - pendingTotalDamage) > 0) { return; }

            // 路障
            BarricadeData barricadeData = barricadeDrop.GetServersideData();
            string barricadeName = barricadeDrop.asset.name;
            ushort barricadeId = barricadeDrop.asset.id;

            // 攻击者
            SteamPlayer insPlayer = PlayerTool.getSteamPlayer(instigatorSteamID);
            if (insPlayer == null) { return; }

            string insName = insPlayer.playerID.characterName;
            Vector3 insPos = insPlayer.player.transform.position;

            // 路障所有者
            CSteamID ownerSteamID = new CSteamID(barricadeData.owner);
            SteamPlayer ownerPlayer = PlayerTool.getSteamPlayer(ownerSteamID);
            string ownerName = ownerPlayer.playerID.characterName;

            string nowTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            FileCTL.AppendAllText($"{nowTime} - 玩家：{insName} SteamID：{instigatorSteamID} 在坐标：{insPos} 摧毁了 玩家：{ownerName} SteamID:{ownerSteamID} 的路障 {barricadeName} 物品ID：{barricadeId} 伤害来源：{damageOrigin}");
        }


        public static void OnDamageStructureRequested(CSteamID instigatorSteamID, Transform structureTransform, ref ushort pendingTotalDamage, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            StructureDrop structureDrop = StructureManager.FindStructureByRootTransform(structureTransform);
            ushort structureHealth = structureDrop.GetServersideData().structure.health;

            if ((structureHealth - pendingTotalDamage) > 0) { return; }

            // 结构
            StructureData structureData = structureDrop.GetServersideData();
            string structureName = structureDrop.asset.name;
            ushort structureId = structureDrop.asset.id;

            // 攻击者
            SteamPlayer insPlayer = PlayerTool.getSteamPlayer(instigatorSteamID);
            if (insPlayer == null) { return; }

            string insName = insPlayer.playerID.characterName;
            Vector3 insPos = insPlayer.player.transform.position;

            // 结构所有者
            CSteamID ownerSteamID = new CSteamID(structureData.owner);
            SteamPlayer ownerPlayer = PlayerTool.getSteamPlayer(ownerSteamID);
            string ownerName = ownerPlayer.playerID.characterName;

            string nowTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            FileCTL.AppendAllText($"{nowTime} - 玩家：{insName} SteamID：{instigatorSteamID} 在坐标：{insPos} 摧毁了 玩家：{ownerName} SteamID:{ownerSteamID} 的结构 {structureName} 物品ID：{structureId} 伤害来源：{damageOrigin}");
        }


        public static void OnEnterVehicleRequested(Player player, InteractableVehicle vehicle, ref bool shouldAllow)
        {
            CSteamID playerSteamID = player.channel.owner.playerID.steamID;
            string playerName = player.channel.owner.playerID.characterName;
            Vector3 playerPos = player.transform.position;

            string vehicleName = vehicle.asset.name;
            ushort vehicleID = vehicle.id;
            uint vehicleInstanceID = vehicle.instanceID;
            string vehicleEng = vehicle.asset.engine.ToString();

            string nowTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            if (vehicle.isLocked)
            {
                CSteamID locker = vehicle.lockedOwner;
                SteamPlayer lockerPlayer = PlayerTool.getSteamPlayer(locker);
                string lockerName = lockerPlayer.playerID.characterName;

                FileCTL.AppendAllText($"{nowTime} - 玩家：{playerName} SteamID：{playerSteamID} 在坐标：{playerPos} 进入了车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked} 上锁玩家：{lockerName} 上锁玩家SteamID：{locker}");
                return;
            }

            FileCTL.AppendAllText($"{nowTime} - 玩家：{playerName} SteamID：{playerSteamID} 在坐标：{playerPos} 进入了车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked}");
        }


        public static void OnExitVehicleRequested(Player player, InteractableVehicle vehicle, ref bool shouldAllow, ref Vector3 pendingLocation, ref float pendingYaw)
        {
            CSteamID playerSteamID = player.channel.owner.playerID.steamID;
            string playerName = player.channel.owner.playerID.characterName;
            Vector3 playerPos = player.transform.position;

            string vehicleName = vehicle.asset.name;
            ushort vehicleID = vehicle.id;
            uint vehicleInstanceID = vehicle.instanceID;
            string vehicleEng = vehicle.asset.engine.ToString();

            string nowTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            if (vehicle.isLocked)
            {
                CSteamID locker = vehicle.lockedOwner;
                SteamPlayer lockerPlayer = PlayerTool.getSteamPlayer(locker);
                string lockerName = lockerPlayer.playerID.characterName;

                FileCTL.AppendAllText($"{nowTime} - 玩家：{playerName} SteamID：{playerSteamID} 在坐标：{playerPos} 离开了车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked} 上锁玩家：{lockerName} 上锁玩家SteamID：{locker}");
                return;
            }

            FileCTL.AppendAllText($"{nowTime} - 玩家：{playerName} SteamID：{playerSteamID} 在坐标：{playerPos} 离开了车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked}");
        }


        public static void OnDamageVehicleRequested(CSteamID instigatorSteamID, InteractableVehicle vehicle, ref ushort pendingTotalDamage, ref bool canRepair, ref bool shouldAllow, EDamageOrigin damageOrigin)
        {
            ushort vehicleHealth = vehicle.health;
            if ((vehicleHealth - pendingTotalDamage) > 0) { return; }

            // 攻击者
            Player insPlayer = PlayerTool.getPlayer(instigatorSteamID);
            if (insPlayer == null) { return; }

            string insName = insPlayer.channel.owner.playerID.characterName;
            Vector3 insPos = insPlayer.transform.position;

            string nowTime = DateTime.Now.ToString("yyyy年MM月dd日 HH时mm分ss秒");

            // 车辆所有者
            string vehicleName = vehicle.asset.name;
            ushort vehicleID = vehicle.id;
            uint vehicleInstanceID = vehicle.instanceID;
            string vehicleEng = vehicle.asset.engine.ToString();

            if (vehicle.isLocked)
            {
                CSteamID locker = vehicle.lockedOwner;
                SteamPlayer lockerPlayer = PlayerTool.getSteamPlayer(locker);
                string lockerName = lockerPlayer.playerID.characterName;

                FileCTL.AppendAllText($"{nowTime} - 玩家：{insName} SteamID：{instigatorSteamID} 在坐标：{insPos} 打爆了 玩家：{lockerName} SteamID：{locker} 的车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked}");
                return;
            }

            FileCTL.AppendAllText($"{nowTime} - 玩家：{insName} SteamID：{instigatorSteamID} 在坐标：{insPos} 打爆了车辆：{vehicleName} 车辆引擎：{vehicleEng} 车辆ID：{vehicleID} 车辆实例ID：{vehicleInstanceID} 车辆是否上锁：{vehicle.isLocked}");
        }
    }
}
