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
        public ItemJar[] handsItemJars;

        public ItemShirtAsset shirtAsset;

        public byte shirtQuality;

        public byte[] shirtState;

        public ItemPantsAsset pantsAsset;

        public byte pantsQuality;

        public byte[] pantsState;

        public ItemJar[] shirtItemJar;

        public ItemJar[] pantsItemJar;

        public byte HandWidth { get; set; }
        public byte HandHeight { get; set; }

        public bool IsDead { get; set; }

        public PlayerData(UnturnedPlayer unturnedPlayer, bool isDead) 
        {
            IsDead = isDead;

            HandWidth = unturnedPlayer.Inventory.items[PlayerInventory.SLOTS].width;
            HandHeight = unturnedPlayer.Inventory.items[PlayerInventory.SLOTS].height;

            handsItemJars = unturnedPlayer.Inventory.items[PlayerInventory.SLOTS].items.ToArray();

            shirtAsset = unturnedPlayer.Player.clothing.shirtAsset;
            pantsAsset = unturnedPlayer.Player.clothing.pantsAsset;

            shirtItemJar = unturnedPlayer.Inventory.items[PlayerInventory.SHIRT].items.ToArray();
            pantsItemJar = unturnedPlayer.Inventory.items[PlayerInventory.PANTS].items.ToArray();

            shirtQuality = unturnedPlayer.Player.clothing.shirtQuality;
            pantsQuality = unturnedPlayer.Player.clothing.pantsQuality;

            shirtState = unturnedPlayer.Player.clothing.shirtState;
            pantsState = unturnedPlayer.Player.clothing.pantsState;
        }
    }
}
