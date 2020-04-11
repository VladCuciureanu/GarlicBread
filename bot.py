import os
import pickle
import discord
from discord.ext import commands


extensions = ['cogs.base',
              'cogs.rank',
             #  'cogs.guild',
              'cogs.owner',
              'cogs.misc']


def get_prefix(bot_obj, message):
    prefixes = ['>', '!']
    if not message.guild:
        return '?'
    return commands.when_mentioned_or(*prefixes)(bot_obj, message)


bot = commands.Bot(command_prefix=get_prefix, description='O w O')
config = {}


if __name__ == '__main__':
    for ext in extensions:
        bot.load_extension(ext)


@bot.event
async def on_command_error(ctx, error):
    if type(error) == discord.ext.commands.errors.CommandNotFound:
        await ctx.send("Owo daddy what's this command? \\*confused noises\\*")


@bot.event
async def on_ready():
    # Check if given guild id is valid
    if config["guild"] not in bot.guilds:
        os.remove("config.grlk")
        raise ValueError("Bot doesn't belong to a guild with given ID.")
        

    print(f'\n\nLogged in as: {bot.user.name} - {bot.user.id}\nVersion: {discord.__version__}\n')
    print(f'Successfully logged in and booted...!')


# Driver Code
if os.path.exists("config.grlk"):
    config = pickle.load(open("config.grlk", "rb"))
else:
    config["token"] = str(input("Bot Token: "))
    config["guild"] = int(input("Guild ID: "))
    pickle.dump(config, file=open("config.grlk", "wb"))

try:
    bot.run(config["token"], bot=True, reconnect=True)
except discord.errors.LoginFailure as e:
    print(e)
    os.remove("config.grlk")
    exit()
except ValueError as e:
    print(e)
    exit()
# TODO: fail-safe bot token and guild id connection (delete config and try again if wrong token)