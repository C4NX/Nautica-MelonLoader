using HarmonyLib;
using MelonLoader;
using NauticaML.GUI;
using NauticaML.Patchs;
using NauticaML.Utils;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace NauticaML
{
    public class NauticaMod : MelonMod
    {
        public const long DiscordClientId = 843153449922592768;


        private DiscordCustomPresence discordCustomPresence;

        public static NauticaMod Instance { get; private set; }

        public NauticaMod() : base()
        {
            Instance = this;
        }

        public override void OnApplicationStart()
        {
            MelonLogger.Msg($"Loading Nautica...");
            
            if(PlatformServicesUtils.IsRuntimePluginDllPresent("discord_game_sdk"))
            {
                discordCustomPresence = new DiscordCustomPresence(DiscordClientId);
                discordCustomPresence.SetActivity(new Discord.Activity
                {
                    Details = "Testing this mod !"
                });
            }

            base.OnApplicationStart();
        }

        public override void OnUpdate()
        {
            if (discordCustomPresence != null)
                discordCustomPresence.Update();

            base.OnUpdate();
        }

        public void OnMainMenuStart()
        {
            var modsButton = MainMenuButton.CreateNew("Mods", "Mods", 3, () =>
            {
                uGUI_MainMenu.main.OnButtonOptions();

                var tabsObj = GameObject.Find("TabsHolder");

                var lastChildToggle = tabsObj.transform.GetChild(OptionAddTabsPatch.LastModsTabId)?.GetComponent<Toggle>();
                if(lastChildToggle != null)
                    AccessTools.Method(lastChildToggle.GetType(), "Set").Invoke(lastChildToggle, new object[] { true, true });
            });
        }
    }
}
