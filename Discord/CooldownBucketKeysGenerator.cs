using System;
using Qmmands.Delegates;

namespace GarlicBread.Discord
{
    public static class CooldownBucketKeysGenerator
    {
        public static CooldownBucketKeyGeneratorDelegate Default = (t, ctx) =>
        {
            if (!(t is CooldownType ct))
                throw new InvalidOperationException(
                    $"Cooldown bucket type is incorrect. Expected {typeof(CooldownType)}, received {t.GetType().Name}.");

            var discordContext = (GarlicBreadCommandContext) ctx;

            if (discordContext.User.Id ==
                discordContext.Bot.CurrentApplication.GetAsync().GetAwaiter().GetResult().Owner.Id)
                return null;

            return ct switch
            {
                CooldownType.Server => discordContext.Guild.Id.ToString(),

                CooldownType.Channel => discordContext.Channel.Id.ToString(),

                CooldownType.User => discordContext.User.Id.ToString(),

                CooldownType.Global => "Global",

                _ => throw new ArgumentOutOfRangeException()
            };
        };
    }
}