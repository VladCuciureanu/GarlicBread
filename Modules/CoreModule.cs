using System;
using System.Diagnostics;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Events;
using GarlicBread.Discord;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace GarlicBread.Modules
{
    [Name("Core")]
    public class CoreModule : GarlicBreadModuleBase
    {
        public ILogger<CoreModule> Logger { get; set; }

        [Command("ping")]
        [Description("Benchmarks the connection to the Discord servers.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        [GuildOnly]
        public async Task Command_PingAsync()
        {
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;

            var sw = Stopwatch.StartNew();
            var initial = await Context.Channel.SendMessageAsync("Pinging...").ConfigureAwait(false);
            var restTime = sw.ElapsedMilliseconds.ToString();

            async Task Handler(MessageReceivedEventArgs emsg)
            {
                try
                {
                    var msg = emsg.Message;
                    if (msg.Id != initial.Id) return;
                    var rtt = sw.ElapsedMilliseconds.ToString();

                    var httpSw = Stopwatch.StartNew();
                    var task = await new HttpClient().GetAsync("https://google.com/");
                    httpSw.Stop();

                    var _ = initial.ModifyAsync(m =>
                    {
                        m.Content = null;
                        var sb = new StringBuilder()
                            .AppendLine($":mailbox_with_mail: **REST:** {restTime}ms")
                            .AppendLine($":roller_coaster: **Round-trip:** {rtt}ms")
                            .AppendLine($":blue_heart: **Internet - Google:** {httpSw.ElapsedMilliseconds}ms");
                        if (Context.Bot.Latency != null)
                            sb.AppendLine(
                                $":handshake: **Gateway:** {(int) Context.Bot.Latency.Value.TotalMilliseconds}ms");
                        m.Embed = new LocalEmbedBuilder()
                            .WithTitle("Pong!")
                            .WithTimestamp(DateTime.Now)
                            .WithDescription(sb.ToString())
                            .WithFooter($"Request {Context.RequestId}")
                            .WithColor(Context.Color)
                            .Build();
                    });
                    sw.Stop();
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error occurred during ping handler");
                }

                Context.Bot.MessageReceived -= Handler;
            }

            Context.Bot.MessageReceived += Handler;
        }
    }
}