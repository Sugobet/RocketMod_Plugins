using MySql.Data.MySqlClient;
using Rocket.API;
using Rocket.API.Serialisation;
using Rocket.Core.Logging;
using Rocket.Core.Plugins;
using Rocket.Unturned.Chat;
using Rocket.Unturned.Player;
using SDG.Unturned;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using Logger = Rocket.Core.Logging.Logger;

namespace SScheduleExperience_RocketMod
{
    public class PluginMain : RocketPlugin<PluginConfig>
    {
        public static PluginConfig Config { get; private set; }
        public DataStorage DataStorage { get; private set; }

        protected override void Load()
        {
            Config = Configuration.Instance;

            DataStorage = new DataStorage();

            PlayerSkills.OnExperienceChanged_Global += OnExperienceChanged_Global;

            foreach (string val in Configuration.Instance.expList)
            {
                string tim = val.Split(':')[0];
                InvokeRepeat(this, () =>
                {
                    GiveExperience(short.Parse(tim));
                }, float.Parse(tim), float.Parse(tim));
            }

            Logger.Log("SScheduleExperience v1.0.0 loaded    Author: Sugobet");
        }

        private void OnExperienceChanged_Global(PlayerSkills skills, uint arg2)
        {
            UnturnedPlayer player = UnturnedPlayer.FromPlayer(skills.player);

            // save to mysql
            var r_data = DataStorage.Query(player.CSteamID.ToString());
            if (r_data == null)
            {
                DataStorage.Insert(player.CSteamID.ToString(), player.CharacterName, player.Experience.ToString());
            }
            else
            {
                DataStorage.Update(player.CSteamID.ToString(), player.CharacterName, player.Experience.ToString());
            }
        }

        protected override void Unload()
        {
            DataStorage.Dispose();

            PlayerSkills.OnExperienceChanged_Global -= OnExperienceChanged_Global;
        }

        public static IEnumerator InvokeRepeat(MonoBehaviour monoBehaviour, System.Action action, float timer, float duration)
        {
            var temp = DoRepeat(action, timer, duration);
            monoBehaviour.StartCoroutine(temp);
            return temp;
        }

        private static IEnumerator DoRepeat(System.Action action, float timer, float duration)
        {
            yield return new WaitForSeconds(timer);
            while (true)
            {
                action.Invoke();
                yield return new WaitForSeconds(duration);
            }
        }

        private void GiveExperience(short time)
        {
            foreach (string val in Configuration.Instance.expList)
            {
                string[] cmd = val.Split(':');

                if (cmd[0] != time.ToString()) { return; }

                string permissionName = cmd[1];

                foreach (SteamPlayer client in Provider.clients)
                {
                    if (client == null) continue;

                    UnturnedPlayer player = UnturnedPlayer.FromPlayer(client.player);
                    List<Permission> playerPermission = player.GetPermissions();
                    foreach (Permission permission in playerPermission)
                    {
                        if (permission.Name == permissionName)
                        {
                            uint exp = uint.Parse(cmd[2]);
                            uint newExp = player.Player.skills.experience + exp;
                            // player.Player.skills.ReceiveExperience(newExp);
                            player.Player.skills.ServerSetExperience(newExp);

                            string message = cmd[3];
                            message = message.Replace("player", player.CharacterName);
                            message = message.Replace("exp", exp.ToString());
                            UnturnedChat.Say(player, message, true);
                            break;
                        }
                    }
                }
            }
        }
    }
}
