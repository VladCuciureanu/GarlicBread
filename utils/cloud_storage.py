import os
import discord
import json
from discord.ext import commands


class CloudStorage:
    def __init__(self, bot):
        self.bot = bot
        self.storage_channel_id = int(os.environ["GARLIC_STORAGE_CHANNEL"])
        self.roles = None
        self.guilds = None
        self.token = self.get_token()

    async def load(self):
        # Get all storage messages
        storage_channel = self.bot.get_channel(self.storage_channel_id)
        raw_data = ""
        async for message in storage_channel.history(limit=None, oldest_first=True):
            raw_data += message.content

        json_data = None

        if raw_data == "":
            self.roles = {}
            self.guilds = {}
            await self.save()
            json_data = {"roles": self.roles, "guilds": self.guilds}
        else:
            json_data = json.loads(raw_data)

        # Roles
        print("Loading roles from cloud storage...")
        self.roles = json_data["roles"]
        print("Loaded roles!")
        # Guilds
        print("Loading guilds from cloud storage...")
        self.guilds = json_data["guilds"]
        print("Loaded guilds!")

    async def save(self, roles=True, guilds=True):
        storage_channel = self.bot.get_channel(self.storage_channel_id)
        # Sanity check
        if self.roles is None or self.guilds is None or self.token is None:
            raise RuntimeError("Configs not properly initialized! Please load() before modifying.")
        message = {"roles": self.roles, "guilds": self.guilds}
        json_message = json.dumps(message)

        async for message in storage_channel.history(limit=None):
            await message.delete()

        while len(json_message) > 0:
            fragment = json_message[:2000]
            json_message = json_message[2000:]
            await storage_channel.send(content=fragment)

    def get_token(self):
        self.token = str(os.environ["GARLIC_TOKEN"])
        return self.token

    """
    Getters
    """

    def get_roles(self):
        if self.roles is None:
            self.load()
        return self.roles

    def get_guilds(self):
        if self.guilds is None:
            self.load()
        return self.guilds

    """
    Roles
    """

    async def add_role(self, guild_id, user_id, role_id):
        guild_id = str(guild_id)
        user_id = str(user_id)

        if guild_id not in self.roles:
            self.roles[guild_id] = {}

        self.roles[guild_id][user_id] = role_id
        await self.save(roles=True, guilds=False)

    async def del_role(self, role_id):
        role_id = str(role_id)
        trigger = False
        for guild in self.roles:
            for user in self.roles[guild]:
                if self.roles[guild][user] == role_id:
                    del self.roles[guild][user]
                    trigger = True
        if not trigger:
            raise KeyError("Couldn't find given role.")
        else:
            await self.save(roles=True, guilds=False)

    """
    Guilds
    """

    async def add_guild(self, guild_id, guild_role_id):
        guild_id = str(guild_id)
        guild_role_id = str(guild_role_id)
        if guild_id not in self.guilds:
            self.guilds[guild_id] = {}
        if guild_role_id not in self.guilds[guild_id]:
            self.guilds[guild_id][guild_role_id] = []
        else:
            raise ValueError("Guild already exists!")
        await self.save(roles=False, guilds=True)

    async def del_guild(self, guild_role_id):
        guild_role_id = str(guild_role_id)
        trigger = False
        for guild in self.guilds:
            if guild_role_id in self.guilds[guild]:
                del self.guilds[guild][guild_role_id]
                trigger = True
        if not trigger:
            raise KeyError("Couldn't find given guild.")
        else:
            await self.save(roles=False, guilds=True)

