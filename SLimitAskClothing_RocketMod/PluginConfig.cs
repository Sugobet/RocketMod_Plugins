using Rocket.API;
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
                hatID = 10000,
                shirtID = 10001,
                pantID = 10002,
            });

            套装.Add(new MyClothing
            {
                name = "测试套装2",
                hatID = 10003,
                shirtID = 10004,
                pantID = 10005,
            });
        }
    }

    [Serializable]
    public class MyClothing
    {
        public string name;
        public ushort hatID;
        public ushort shirtID;
        public ushort pantID;
    }
}
