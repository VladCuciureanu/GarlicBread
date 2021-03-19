using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Disqord;
using Disqord.Events;
using GarlicBread.Discord;
using Humanizer;
using Microsoft.Extensions.Hosting.Internal;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace GarlicBread.Modules
{
    public class OwnerModule : GarlicBreadModuleBase
    {
        public ILogger<CoreModule> Logger { get; set; }

        [Command("uptime", "up")]
        [Description("Displays the bot's uptime.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_Uptime()
        {
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;

            var initialMessage = await Context.Channel.SendMessageAsync("Fetching user info...").ConfigureAwait(false);

            async Task Handler(MessageReceivedEventArgs emsg)
            {
                try
                {
                    var msg = emsg.Message;
                    if (msg.Id != initialMessage.Id) return;

                    var _ = initialMessage.ModifyAsync(m =>
                    {
                        m.Content = null;
                        var sb = new StringBuilder()
                            .AppendLine(
                                $":clock1: **Uptime:** {(DateTime.UtcNow - Process.GetCurrentProcess().StartTime.ToUniversalTime()).Humanize()}");

                        if (Context.Bot.Latency != null)
                            sb.AppendLine(
                                $":handshake: **Gateway:** {(int) Context.Bot.Latency.Value.TotalMilliseconds}ms");

                        m.Embed = new LocalEmbedBuilder()
                            .WithTitle($"**Garlic Bread**'s uptime")
                            .WithTimestamp(DateTime.Now)
                            .WithDescription(sb.ToString())
                            .WithFooter($"Request {Context.RequestId}")
                            .WithColor(Context.Color)
                            .Build();
                    });
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error occurred during user info handler");
                }

                Context.Bot.MessageReceived -= Handler;
            }

            Context.Bot.MessageReceived += Handler;
        }
        
        [Command("purge", "purgemessages")]
        [Description("Displays the bot's uptime.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_PurgeMessages(int limit = 0)
        {
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).ManageMessages) return;
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).ReadMessageHistory) return;

            var messages = await Context.Channel.GetMessagesAsync(limit, RetrievalDirection.Before);
            foreach (var restMessage in messages)
            {
                await restMessage.DeleteAsync();
            }
        }
    }
}