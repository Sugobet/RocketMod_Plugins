using Rocket.API;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SScheduleExperience_RocketMod
{
    public class PluginConfig : IRocketPluginConfiguration
    {
        /// <summary>
        /// 时间:权限名字:经验:提示
        /// </summary>
        public List<string> expList;

        public string mysql_ip;
        public string mysql_port;
        public string mysql_username;
        public string mysql_password;
        public string mysql_database;
        public string mysql_table;

        public void LoadDefaults()
        {
            expList = new List<string> { "10:default:100:默认组 player 获得经验 exp", "15:vip:200:vip组 player 获得经验 exp" };
        
            mysql_ip = "127.0.0.1";
            mysql_port = "3306";
            mysql_username = "bsl";
            mysql_password = "1234";
            mysql_database = "bsl";
            mysql_table = "playerexperiences";
        }
    }
}
