using System;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Disqord;
using GarlicBread.Discord;
using GarlicBread.Extensions;
using GarlicBread.Helpers;
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
    public class UserModule : GarlicBreadModuleBase
    {
        [Command("avatar", "av", "a", "pic", "pfp")]
        [Description("Grabs the avatar for a user.")]
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
    }
}