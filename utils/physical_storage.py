import json
from pathlib import Path


class PhysicalStorage:
    def __init__(self):
        self.roles_file = Path("roles.grlk")
        self.guilds_file = Path("guilds.grlk")
        self.roles = None
        self.guilds = None
        self.token = None

    async def load(self):
        # Roles
        if self.roles_file.is_file():
            print("Loading roles file...")
            self.roles = json.loads(self.roles_file.read_text())
            print("Loaded roles file!")
        else:
            print("Generating roles file")
            self.roles = {}
            self.roles_file.write_text(json.dumps(self.roles))
            print("Generated roles file!")
        # Guilds
        if self.guilds_file.is_file():
            print("Loading guilds file...")
            self.guilds = json.loads(self.guilds_file.read_text())
            print("Loaded guilds file!")
        else:
            print("Generating guilds file...")
            self.guilds = {}
            self.guilds_file.write_text(json.dumps(self.guilds))
            print("Generated guilds file!")

    async def save(self, roles=True, guilds=True):
        # Sanity check
        if self.roles is None or self.guilds is None or self.token is None:
            raise RuntimeError("Configs not properly initialized! Please load() before modifying.")
        # Roles
        if self.roles_file.is_file() and roles:
            print("Saving roles file...")
            self.roles_file.write_text(json.dumps(self.roles))
            print("Saved roles file!")
        # Guilds
        if self.guilds_file.is_file() and guilds:
            print("Saving guilds file...")
            self.guilds_file.write_text(json.dumps(self.guilds))
            print("Saved guilds file!")

    def get_token(self):
        token_file = Path("token.grlk")
        if token_file.is_file():
            print("Loading token file...")
            self.token = token_file.read_text()
            print("Loaded token file!")
            return self.token
        else:
            print("Generating token file...")
            self.token = str(input("Bot Token: "))
            token_file.write_text(self.token)
            print("Generated token file!")
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
