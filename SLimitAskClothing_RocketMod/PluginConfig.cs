using Rocket.API;
using SDG.Unturned;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SLimitAskClothing_RocketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        public List<MyClothing> 套装 {  get; set; }

        public void LoadDefaults()
        {
            套装 = new List<MyClothing>();
            套装.Add(new MyClothing
            {
                name = "测试套装1",
                main = "shirt",
                hatID = 0,
                shirtID = 1179,
                pantID = 1180,
                vestID = 0,
                maskID = 0,
                glassesID = 1181,
                backpackID = 1178,
            });

            套装.Add(new MyClothing
            {
                name = "测试套装2",
                main = "shirt",
                hatID = 0,
                shirtID = 1171,
                pantID = 1172,
                vestID = 0,
                maskID = 0,
                glassesID = 0,
                backpackID = 1170,
            });
        }
    }

    [Serializable]
    public class MyClothing
    {
        public string name;
        public string main;
        public ushort hatID;
        public ushort shirtID;
        public ushort pantID;
        public ushort vestID;
        public ushort maskID;
        public ushort glassesID;
        public ushort backpackID;

        public byte shirtQuality;
        public byte pantsQuality;
        public byte hatQuality;
        public byte backpackQuality;
        public byte vestQuality;
        public byte maskQuality;
        public byte glassesQuality;

        public byte[] shirtState;
        public byte[] pantsState;
        public byte[] hatState;
        public byte[] backpackState;
        public byte[] vestState;
        public byte[] maskState;
        public byte[] glassesState;


        public MyClothing()
        {

        }

        public MyClothing(PlayerClothing clothing)
        {
            hatID = clothing.hat;
            shirtID = clothing.shirt;
            pantID = clothing.pants;
            vestID = clothing.vest;
            maskID = clothing.mask;
            glassesID = clothing.glasses;
            backpackID = clothing.backpack;

            hatQuality = clothing.hatQuality;
            shirtQuality = clothing.shirtQuality;
            pantsQuality = clothing.pantsQuality;
            vestQuality = clothing.vestQuality;
            maskQuality = clothing.maskQuality;
            glassesQuality = clothing.glassesQuality;
            backpackQuality = clothing.backpackQuality;

            hatState = clothing.hatState;
            shirtState = clothing.shirtState;
            pantsState = clothing.pantsState;
            vestState = clothing.vestState;
            maskState = clothing.maskState;
            glassesState = clothing.glassesState;
            backpackState = clothing.backpackState;

        }
    }
}
