using Discord;
using MelonLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NauticaML.Utils
{
    public class DiscordCustomPresence
    {
        private Discord.Discord discordClient;
        private ActivityManager activityManager;

        public DiscordCustomPresence(long clientId)
        {
            try
            {
                discordClient = new Discord.Discord(clientId, (ulong)Discord.CreateFlags.NoRequireDiscord);
                Initialize();
            }
            catch (Exception ex)
            {
                MelonLogger.Warning($"An error occurred while changing or enabling rich presence, {ex.GetType().Name}: {ex.Message}");
                discordClient = null;
            }
        }

        public void Update()
        {
            if (discordClient != null)
                discordClient.RunCallbacks();
        }

        private void Initialize()
        {
            discordClient.SetLogHook(LogLevel.Debug, delegate (LogLevel level, string message)
            {
                MelonLogger.Msg($"Discord: {message}");
            });

            activityManager = discordClient.GetActivityManager();
        }

        /// <summary>
        /// Set the current <see cref="Activity"/> of Discord
        /// </summary>
        /// <param name="presence">The new <see cref="Activity"/></param>
        public void SetActivity(Activity presence) => activityManager.UpdateActivity(presence, (e) => MelonLogger.Msg($"Discord Result: {e}"));
    }
}
