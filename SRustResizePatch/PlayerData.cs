using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SNoLoot_RocketMod
{
    public class PlayerData
    {
        public byte HandWidth { get; set; }
        public byte HandHeight { get; set; }


        public PlayerData(UnturnedPlayer unturnedPlayer) 
        {
            HandWidth = unturnedPlayer.Inventory.items[PlayerInventory.SLOTS].width;
            HandHeight = unturnedPlayer.Inventory.items[PlayerInventory.SLOTS].height;
        }
    }
}
