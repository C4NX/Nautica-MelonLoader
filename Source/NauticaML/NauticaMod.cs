using HarmonyLib;
using MelonLoader;
using NauticaML.GUI;
using NauticaML.Patchs;
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
        public static NauticaMod Instance { get; private set; }

        public NauticaMod() : base()
        {
            Instance = this;
        }

        public override void OnApplicationStart()
        {
            MelonLogger.Msg($"Loading Nautica...");
            
            base.OnApplicationStart();
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

            throw new Exception("Ahahahahahyuzehfiudhf");
        }
    }
}
