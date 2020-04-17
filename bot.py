import os
import pickle
import discord
from discord.ext import commands

extensions = ['cogs.base',
              'cogs.roles',
              #  'cogs.guild',
              'cogs.owner',
              'cogs.misc']


def get_prefix(bot_obj, message):
    prefixes = ['>']
    if not message.guild:
        return '?'
    return commands.when_mentioned_or(*prefixes)(bot_obj, message)


bot = commands.Bot(command_prefix=get_prefix, description='Fresh out of the oven.')
config = {}

if __name__ == '__main__':
    for ext in extensions:
        bot.load_extension(ext)


@bot.event
async def on_command_error(ctx, error):
    if type(error) == discord.ext.commands.errors.CommandNotFound:
        await ctx.send("Owo what's this command? \\*confused noises\\*")
    elif type(error) == discord.ext.commands.errors.CheckFailure:
        await ctx.send("TwT sowwy but u can't wun this command xP")
    else:
        print(error)


@bot.event
async def on_ready():
    print(f'\n\nLogged in as: {bot.user.name} - {bot.user.id}\nVersion: {discord.__version__}\n')
    print(f'Successfully logged in and booted...!')


# Driver Code #

# Checking if a config already exists
if os.path.exists("config.grlk"):
    # Loading existing config
    config = pickle.load(open("config.grlk", "rb"))
else:
    # Need this check for Heroku
    if "TOKEN" in os.environ:
        # Get bot token from env var
        config["token"] = str(os.environ["TOKEN"])
    else:
        # Get bot token from keyboard input
        config["token"] = str(input("Bot Token: "))
    # Dump config to file
    pickle.dump(config, file=open("config.grlk", "wb"))

try:
    bot.run(config["token"], bot=True, reconnect=True)
except discord.errors.LoginFailure:
    print("Invalid bot token!")
    os.remove("config.grlk")
    exit()
