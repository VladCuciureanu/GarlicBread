using System;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Bot.Prefixes;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace GarlicBread.Discord
{
    public class GarlicBreadDiscordBot : DiscordBot
    {
        private readonly ILogger<GarlicBreadDiscordBot> _logger;
        private readonly IServiceProvider _services;

        public GarlicBreadDiscordBot(IServiceProvider services)
            : base(
                TokenType.Bot,
                services.GetRequiredService<IConfiguration>().GetSection("Secrets").GetSection("Discord")["Token"],
                services.GetRequiredService<IPrefixProvider>(),
                services.GetRequiredService<DiscordBotConfiguration>())
        {
            _services = services;
            _logger = services.GetRequiredService<ILogger<GarlicBreadDiscordBot>>();
        }

        protected override ValueTask<DiscordCommandContext> GetCommandContextAsync(CachedUserMessage message,
            IPrefix prefix)
        {
            return new(CreateContext(message, prefix, RuntimeFlags.None));
        }

        public DiscordCommandContext CreateContext(CachedUserMessage message, IPrefix prefix, RuntimeFlags flags)
        {
            return new GarlicBreadCommandContext(this, prefix, message, _services.CreateScope(), flags);
        }

        public override string ToString()
        {
            return CurrentUser.ToString();
        }

        protected override async ValueTask AfterExecutedAsync(IResult result, DiscordCommandContext ctx0)
        {
            var context = (GarlicBreadCommandContext) ctx0;
            if (!result.IsSuccessful && !(result is CommandNotFoundResult))
                await context.Channel.SendMessageAsync(result.ToString());
        }
    }
}