using HarmonyLib;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace NauticaML.Patchs
{
    [HarmonyPatch(typeof(uGUI_OptionsPanel), "AddTabs")]
    internal class OptionAddTabsPatch
    {
        internal static int LastModsTabId = 0;

        [HarmonyPostfix]
        static void Postfix(uGUI_OptionsPanel __instance)
        {
            AddNauticaOptionTab(__instance);
            AddModsOptionTab(__instance);
        }

        public static void AddNauticaOptionTab(uGUI_OptionsPanel optionsPanel)
        {
            var tabId = optionsPanel.AddTab("Nautica");
            optionsPanel.AddHeading(tabId, $"<align=\"center\">Nautica<sup>{NauticaMod.Instance.Info.Version}</sup></align>");
            optionsPanel.AddHeading(tabId, "Generals");
            optionsPanel.AddToggleOption(tabId, "Enable Discord Rich Presence", false, (value) =>
            {
                //TODO: Add discord rich presence for nautica !

                optionsPanel.dialog.Show("WIP", (v) => {}, "OK");

                MelonLogger.Msg("WIP: Discord rich presence !");
            }, "Enable rich presence from nautica to discord");
        }

        public static void AddModsOptionTab(uGUI_OptionsPanel optionsPanel)
        {
            var tabId = optionsPanel.AddTab("Mods");
            LastModsTabId = tabId;

#pragma warning disable CS0618 // Le type ou le membre est obsolète
            foreach (var item in MelonLoader.Main.Mods)
#pragma warning restore CS0618 // Le type ou le membre est obsolète
            {

                bool isSelf = item.Assembly.Equals(Assembly.GetExecutingAssembly());

                StringBuilder sb = new StringBuilder();

                if (isSelf)
                    sb.AppendLine($"<align=\"center\"><color=#ff0000ff>{item.Info.Name}</color></align>");
                else
                    sb.AppendLine($"<align=\"center\"><b>{item.Info.Name}</b></align>");
                sb.AppendLine($"<i>{item.Info.Author ?? "Unknown"}</i>");

                optionsPanel.AddHeading(tabId, sb.ToString());
                if (item.Info.DownloadLink != null)
                {
                    Uri createdUri = null;
                    if (Uri.TryCreate(item.Info.DownloadLink, UriKind.Absolute, out createdUri) && (createdUri.Scheme == Uri.UriSchemeHttp || createdUri.Scheme == Uri.UriSchemeHttps))
                    {
                        optionsPanel.AddButton(tabId, "Open link", () => {
                            optionsPanel.dialog.Show($"Do you want open this link {item.Info.DownloadLink}", (v) =>
                            {
                                if (v == 0)
                                    Process.Start(item.Info.DownloadLink);
                            }, "Yes", "No");
                        });
                    }
                    else MelonLogger.Warning($"Mod {item.ID}, non HTTP/HTTPS download uri scheme, pass.");
                }
            }

        }
    }
}
