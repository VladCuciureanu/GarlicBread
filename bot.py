import os
import discord
import datetime
import global_vars
from discord.ext import commands

from utils.cloud_storage import CloudStorage
from utils.physical_storage import PhysicalStorage


"""
Configurations
"""

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


"""
Driver code
"""

# Declaring bot variable
bot = commands.Bot(command_prefix=get_prefix, description='Fresh out of the oven.')
bot.remove_command("help") # Removes standard help command in order to implement our own.

# Loading Cogs
if __name__ == '__main__':
    for ext in extensions:
        bot.load_extension(ext)

# Setting start time-date
global_vars.start_time_date = datetime.datetime.now()

# Loading Configs
if "GARLIC_TOKEN" in os.environ:
    print("Using cloud storage!")
    global_vars.config_manager = CloudStorage(bot)
else:
    print("Using physical storage!")
    global_vars.config_manager = PhysicalStorage()

# Initializing the bot
try:
    bot.run(global_vars.config_manager.get_token(), bot=True, reconnect=True)
except discord.errors.LoginFailure:
    print("Invalid bot token!")
    os.remove("config.grlk")
    exit()
