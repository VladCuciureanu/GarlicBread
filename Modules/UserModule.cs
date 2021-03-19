using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Events;
using GarlicBread.Discord;
using GarlicBread.Extensions;
using GarlicBread.Helpers;
using Humanizer;
using Microsoft.Extensions.Logging;
using Qmmands;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats;
using SixLabors.ImageSharp.Formats.Gif;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.Processing.Processors.Transforms;
using Color = Disqord.Color;

namespace GarlicBread.Modules
{
    [Name("User")]
    public class UserModule : GarlicBreadModuleBase
    {
        public ILogger<CoreModule> Logger { get; set; }
        
        [Command("avatar", "av", "a", "pic", "pfp")]
        [Description("Grabs the avatar for a user.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task GetAvatarAsync(
            [Name("User")]
            [Description("The user who you wish to get the avatar for.")]
            CachedMember target = null)
        {
            target ??= Context.Member;
            await ReplyAsync(embed: new LocalEmbedBuilder()
                .WithAuthor(target)
                .WithColor(Context.Color)
                .WithImageUrl(target.GetAvatarUrl())
                .WithDescription($"{UrlHelper.CreateMarkdownUrl("128", target.GetAvatarUrl(size: 128))} | " +
                                 $"{UrlHelper.CreateMarkdownUrl("256", target.GetAvatarUrl(size: 256))} | " +
                                 $"{UrlHelper.CreateMarkdownUrl("1024", target.GetAvatarUrl(size: 1024))} | " +
                                 $"{UrlHelper.CreateMarkdownUrl("2048", target.GetAvatarUrl(size: 2048))}")
                .Build());
        }
        
        [GuildOnly]
        [Command("userinfo", "info", "ui", "joined", "age")]
        [Description("Displays basic user info about the mentioned user.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_UserInfo(CachedMember target = null)
        {
            target ??= Context.Member;

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
                            .AppendLine($":wave: **Join date:** {target.JoinedAt.Humanize()}");

                        if (Context.Bot.Latency != null)
                            sb.AppendLine(
                                $":handshake: **Gateway:** {(int) Context.Bot.Latency.Value.TotalMilliseconds}ms");

                        m.Embed = new LocalEmbedBuilder()
                            .WithTitle($"**{target.Name}**'s info")
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
    }
}