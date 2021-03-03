using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot.Prefixes;
using GarlicBread.Persistence.Relational;
using Microsoft.Extensions.DependencyInjection;

namespace GarlicBread.Persistence
{
    public class GarlicBreadPrefixProvider : IPrefixProvider
    {
        private readonly IEnumerable<IPrefix> _dmPrefixSet = new[]
            {new StringPrefix(Constants.DEFAULT_GUILD_MESSAGE_PREFIX)};

        private readonly IServiceProvider _services;

        public GarlicBreadPrefixProvider(IServiceProvider services)
        {
            _services = services;
        }

        public async ValueTask<IEnumerable<IPrefix>> GetPrefixesAsync(CachedUserMessage message)
        {
            if (message.Guild == null) return _dmPrefixSet;
            using var scope = _services.CreateScope();
            var record = await scope.ServiceProvider.GetRequiredService<GarlicBreadPersistenceContext>()
                .GetJsonObjectAsync(d => d.GuildConfigurations, message.Guild.Id);
            return record.Prefixes.Select<string, IPrefix>(d => new StringPrefix(d)).Append(MentionPrefix.Instance);
        }
    }
}