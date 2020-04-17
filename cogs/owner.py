from discord.ext import commands
import os
import pickle


class OwnerCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='load', hidden=True)
    @commands.is_owner()
    async def module_load(self, ctx, *, cog: str):
        """
        Unloads a module.
        :param ctx: Context, command context.
        :param cog: str, module name.
        :return:
        """
        try:
            self.bot.load_extension(cog)
        except Exception as e:
            await ctx.send(f'**`ERROR:`** {type(e).__name__} - {e}')
        else:
            await ctx.send('**`SUCCESS`**')

    @commands.command(name='unload', hidden=True)
    @commands.is_owner()
    async def module_unload(self, ctx, *, cog: str):
        """
        Unloads a module.
        :param ctx: Context, command context.
        :param cog: str, module name.
        :return:
        """
        try:
            self.bot.unload_extension(cog)
        except Exception as e:
            await ctx.send(f'**`ERROR:`** {type(e).__name__} - {e}')
        else:
            await ctx.send('**`SUCCESS`**')

    @commands.command(name='reload', hidden=True)
    @commands.is_owner()
    async def module_reload(self, ctx, *, cog: str):
        """
        Reloads a module.
        :param ctx: Context, command context.
        :param cog: str, module name.
        :return:
        """
        try:
            self.bot.unload_extension(cog)
            self.bot.load_extension(cog)
        except Exception as e:
            await ctx.send(f'**`ERROR:`** {type(e).__name__} - {e}')
        else:
            await ctx.send('**`SUCCESS`**')

    @commands.command(hidden=True)
    @commands.is_owner()
    @commands.guild_only()
    async def purge_roles(self, ctx):
        """
        Resets roles. Shouldn't be used. Deletes ALL roles.
        :param ctx: Context, command context.
        :return: -
        """
        deleted_counter = 0
        guild = await self.bot.fetch_guild(ctx.guild.id)
        role_list = guild.roles
        try:
            for role in role_list:
                if role.name != "@everyone" and role.name != "Garlic Bread":
                    print("Deleting role " + str(role.name) + "...")
                    await role.delete()
                    print("Deleted!")
                    deleted_counter += 1
            if deleted_counter > 0:
                await ctx.send("Deleted " + str(deleted_counter) + "roles successfully!")
            else:
                await ctx.send("There's no roles to delete!")
        except Exception as e:
            print(e)

    @commands.command(hidden=True)
    @commands.is_owner()
    @commands.guild_only()
    async def purge_messages(self, ctx, *, count: int):
        """
        Deletes a given number of messages in chronological order.
        :param ctx: Context, command context.
        :param count: int, number of messages to delete.
        :return: -
        """
        channel = ctx.message.channel
        async for message in channel.history(limit=count):
            await message.delete()
    
    @commands.command(hidden=True)
    @commands.is_owner()
    async def host_type(self, ctx):
        """
        Displays whether or not the bot is hosted locally.
        :param ctx: Context, command context.
        :return: -
        """
        if "TOKEN" in os.environ:
            await ctx.send("Currently being hosted on Heroku!")
        else:
            await ctx.send("I'm being hosted locally.")
    
    @commands.command(hidden=True)
    @commands.is_owner()
    @commands.guild_only()
    async def save_roles(self, ctx):
        roles = {}
        if os.path.exists("roles.grlk"):
            roles = pickle.load(open("roles.grlk", "rb"))
        else:
            pickle.dump(roles, file=open("roles.grlk", "wb"))
            
        guild = ctx.guild
        
        if str(ctx.guild.id) not in roles:
            roles[str(ctx.guild.id)] = {}
        
        async for member in guild.fetch_members():
            if member.id != self.bot.user.id:
                member_roles = member.roles
                if len(member_roles) > 1:
                    roles[str(ctx.guild.id)][str(member.id)] = member_roles[1].id
        
        pickle.dump(roles, file=open("roles.grlk", "wb"))
        await ctx.send("Saved roles successfully!")


def setup(bot):
    bot.add_cog(OwnerCog(bot))
