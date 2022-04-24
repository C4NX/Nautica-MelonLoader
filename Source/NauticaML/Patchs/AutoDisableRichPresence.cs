using HarmonyLib;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticaML.Patchs
{
    /// <summary>
    /// This patch disables rich presence for the discord platform service.
    /// This is made to let nautica make its own rich presence even with the service in game.
    /// </summary>
    [HarmonyPatch(typeof(PlatformServicesDiscord), "SetRichPresence")]
    internal class AutoDisableRichPresence
    {
        [HarmonyPrefix]
        public static bool Prefix() => false;
    }
}
