using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Disqord;
using Disqord.Bot;
using Disqord.Events;
using GarlicBread.Discord;
using Humanizer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Qmmands;

namespace GarlicBread.Modules
{
    [Name("Misc")]
    public class MiscModule : GarlicBreadModuleBase
    {
        public ILogger<CoreModule> Logger { get; set; }
        public IServiceProvider Services { get; set; }
        
        [Command("uwu")]
        [Description("Definitely wont explain this.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_Uwu(string promptKey = null)
        {
            try
            {
                promptKey = promptKey ?? "?";

                if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;

                var prompt = Services.GetRequiredService<IConfiguration>().GetSection("Uwu")[promptKey];

                if (prompt == null)
                {
                    await Context.Channel
                        .SendMessageAsync("Invalid key. Couldn't find a prompt matching given key.")
                        .ConfigureAwait(false);
                    throw new KeyNotFoundException();
                }

                await Context.Channel.SendMessageAsync(prompt).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error occurred during ... uwu?");
            }
        }
        
        [Command("hmm", "hmmm", "hmmmm")]
        [Description("Toss a coin.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_Hmm()
        {
            try
            {
                if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;

                await Context.Channel.SendMessageAsync("... Frick.").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error occurred during coin toss.");
            }
        }
        
        [Command("roll")]
        [Description("Rolls a dice in given size.")]
        [CommandCooldown(1, 3, CooldownMeasure.Seconds, CooldownType.User)]
        public async Task Command_Roll(int diceSides = 6)
        {
            try
            {
                if (!Context.BotMember.GetPermissionsFor(Context.Channel as CachedTextChannel).SendMessages) return;

                if (diceSides < 1)
                {
                    await Context.Channel
                        .SendMessageAsync("Dice must have at least 1 side...")
                        .ConfigureAwait(false);
                    throw new ArithmeticException();
                }
                
                int rolledNumber = new Random().Next(1, diceSides);

                await Context.Channel.SendMessageAsync("Rolled a D" + diceSides + " and got " + rolledNumber + ".").ConfigureAwait(false);
            }
            catch (Exception e)
            {
                Logger.LogError(e, "Error occurred during coin toss.");
            }
        }
    }
    
    
}