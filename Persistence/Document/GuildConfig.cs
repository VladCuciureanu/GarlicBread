using System.Collections.Generic;

namespace GarlicBread.Persistence.Document
{
    public class GuildConfig : JsonRootObject<GuildConfig>
    {
        public List<string> Prefixes { get; set; } = new() {Constants.DEFAULT_GUILD_MESSAGE_PREFIX};
    }
}