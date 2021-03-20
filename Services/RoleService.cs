using System.Linq;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Logging;
using GarlicBread.Discord;
using GarlicBread.Extensions;
using GarlicBread.Persistence.Relational;
using Microsoft.EntityFrameworkCore;
using Qmmands;

namespace GarlicBread.Services
{
    public class RoleService
    {
        public static GarlicBreadPersistenceContext DatabaseContext { get; set; }

        public RoleService(GarlicBreadPersistenceContext context)
        {
            DatabaseContext = context;
        }
        
        public static async Task<CustomizableRole> CreateRole(DiscordCommandContext commandContext)
        {
            var user = commandContext.User;
            var guild = commandContext.Guild;
            var role = await guild.CreateRoleAsync();
            DatabaseContext.CustomizableRoles.Add(new CustomizableRole(guild.Id.RawValue, user.Id.RawValue,
                role.Id.RawValue));
            DatabaseContext.SaveChanges();
            await commandContext.Guild.GrantRoleAsync(user.Id, role.Id);
            await role.ModifyAsync(r => r.Name = $"{user.Name}'s Role");
            return new CustomizableRole(guild.Id.RawValue, user.Id.RawValue, role.Id.RawValue);
        }

        public static async Task<CustomizableRole> RepairRole(DiscordCommandContext commandContext)
        {
            var user = commandContext.User;
            var guild = commandContext.Guild;
            DatabaseContext.CustomizableRoles.RemoveRange(
                DatabaseContext.CustomizableRoles.Where(r =>
                    r.UserId == user.Id.RawValue && r.GuildId == guild.Id.RawValue));
            await DatabaseContext.SaveChangesAsync();
            return await CreateRole(commandContext);
        }

        public static async Task SetRoleColor(DiscordCommandContext commandContext, Color color)
        {
            var userId = commandContext.Member.Id.RawValue;
            var guildId = commandContext.Guild.Id.RawValue;
            var roleDto = DatabaseContext.CustomizableRoles.Where(r => r.GuildId == guildId && r.UserId == userId)
                .FirstOrDefault() ?? CreateRole(commandContext).Result;
            if (!commandContext.Guild.Roles.ContainsKey(roleDto.RoleId))
                roleDto = await RepairRole(commandContext);
            await commandContext.Guild.Roles[roleDto.RoleId].ModifyAsync(r => r.Color = color);
        }

        public static async Task SetRoleName(DiscordCommandContext commandContext, string name)
        {
            var userId = commandContext.Member.Id.RawValue;
            var guildId = commandContext.Guild.Id.RawValue;
            var roleDto = DatabaseContext.CustomizableRoles.Where(r => r.GuildId == guildId && r.UserId == userId)
                .FirstOrDefault() ?? CreateRole(commandContext).Result;
            if (!commandContext.Guild.Roles.ContainsKey(roleDto.RoleId))
                roleDto = await RepairRole(commandContext);
            await commandContext.Guild.Roles[roleDto.RoleId].ModifyAsync(r => r.Name = name);
        }
    }
}