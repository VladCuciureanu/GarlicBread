using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GarlicBread.Persistence.Relational
{
    public class CustomizableRole
    {
        public ulong GuildId { get; set; }

        public ulong UserId { get; set; }
        
        public ulong RoleId { get; set; }

        public CustomizableRole(ulong guildId, ulong userId, ulong roleId)
        {
            GuildId = guildId;
            UserId = userId;
            RoleId = roleId;
        }
    }
}