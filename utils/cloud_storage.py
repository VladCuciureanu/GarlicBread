import os
import json
import discord
from discord.ext import commands


class CloudStorage:
    def __init__(self, bot):
        self.bot = bot
        if "GARLIC_STORAGE" not in os.environ:
            raise EnvironmentError("You must set 'GARLIC_STORAGE' envvar. Check docs.")

    def load(self):
        print("Yay")

        print("Yay")

    def get_token(self):
        return str(os.environ["TOKEN"])

