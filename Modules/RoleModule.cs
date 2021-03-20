using System;
using System.Text;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Events;
using GarlicBread.Discord;
using GarlicBread.Persistence.Relational;
using GarlicBread.Services;
using Humanizer;
using Microsoft.Extensions.Logging;
using Qmmands;

namespace GarlicBread.Modules
{
    [Name("Role")]
    public class RoleModule : GarlicBreadModuleBase
    {
        public ILogger<CoreModule> Logger { get; set; }
        public RoleService RoleService { get; set; }

        [GuildOnly]
        [Command("rolecolor", "setrolecolor")]
        [Description("Gives anyone the desired role color.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_SetRoleColor(Color color)
        {
            var target = Context.Member;
            
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).ManageRoles) return;

            var initialMessage = await Context.Channel.SendMessageAsync("Changing role color...").ConfigureAwait(false);

            async Task Handler(MessageReceivedEventArgs emsg)
            {
                try
                {
                    var msg = emsg.Message;
                    if (msg.Id != initialMessage.Id) return;

                    await RoleService.SetRoleColor(Context, color);
                    
                    await initialMessage.ModifyAsync(m =>
                    {
                        m.Embed = new LocalEmbedBuilder()
                            .WithTitle($"Changed **{target.Name}**'s role color")
                            .WithTimestamp(DateTime.Now)
                            .WithFooter($"Request {Context.RequestId}")
                            .WithColor(color)
                            .Build();
                    });
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error occurred during changing a role's color");
                }

                Context.Bot.MessageReceived -= Handler;
            }

            Context.Bot.MessageReceived += Handler;
        }
        
        [GuildOnly]
        [Command("rolename", "setrolename")]
        [Description("Gives anyone the desired role name.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_SetRoleName(string name)
        {
            var target = Context.Member;
            
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;
            if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).ManageRoles) return;

            var initialMessage = await Context.Channel.SendMessageAsync("Changing role name...").ConfigureAwait(false);

            async Task Handler(MessageReceivedEventArgs emsg)
            {
                try
                {
                    var msg = emsg.Message;
                    if (msg.Id != initialMessage.Id) return;

                    await RoleService.SetRoleName(Context, name);
                    
                    await initialMessage.ModifyAsync(m =>
                    {
                        m.Embed = new LocalEmbedBuilder()
                            .WithTitle($"Changed **{target.Name}**'s role name to **{name}**.")
                            .WithTimestamp(DateTime.Now)
                            .WithFooter($"Request {Context.RequestId}")
                            .WithColor(Context.Color)
                            .Build();
                    });
                }
                catch (Exception e)
                {
                    Logger.LogError(e, "Error occurred during changing a role's name");
                }

                Context.Bot.MessageReceived -= Handler;
            }

            Context.Bot.MessageReceived += Handler;
        }
    }
}