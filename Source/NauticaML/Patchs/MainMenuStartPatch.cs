using HarmonyLib;
using MelonLoader;
using NauticaML.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TMPro;
using UnityEngine;

namespace NauticaML.Patchs
{
    [HarmonyPatch(typeof(uGUI_MainMenu), "Start")]
    internal class MainMenuStartPatch
    {
        [HarmonyPostfix]
        public static void Postfix()
        {
            try
            {
                NauticaMod.Instance?.OnMainMenuStart();
            }
            catch (Exception ex)
            {
                MelonLogger.Error("Error in OnMainMenuStart()", ex);

                var dislaimerObj = GameObject.Find("EarlyAccessDisclaimer");
                dislaimerObj.GetComponentInChildren<TMP_Text>().text = $"<color=#ff0000ff><b>Error in OnMainMenuStart()</b></color>\n\n{ex}";
            }
        }
    }
}
