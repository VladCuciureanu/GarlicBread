import os
import pickle
import discord
from discord.ext import commands

# Cogs list
extensions = ['cogs.base',
              'cogs.roles',
              #  'cogs.guild',
              'cogs.owner',
              'cogs.events',
              'cogs.misc']

# Command prefix configuration
def get_prefix(bot_obj, message):
    prefixes = ['>'] # Prefixes for guilds
    if not message.guild:
        return '?' # Prefix used for DM interaction
    return commands.when_mentioned_or(*prefixes)(bot_obj, message)

bot = commands.Bot(command_prefix=get_prefix, description='Fresh out of the oven.') # Main bot variable
bot.remove_command("help") # Removes standard help command in order to implement our own.
config = {}

# Cogs loading
if __name__ == '__main__':
    for ext in extensions:
        bot.load_extension(ext)

# Driver Code

# TODO(Vlad): Refactor configuration
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

# Initializing the bot
try:
    bot.run(config["token"], bot=True, reconnect=True)
except discord.errors.LoginFailure:
    print("Invalid bot token!")
    os.remove("config.grlk")
    exit()
