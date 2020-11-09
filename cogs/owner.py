import os
import discord
import datetime
from utils.timedelta_format import timedelta_format
from discord.ext import commands
import global_vars


class OwnerCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='load', hidden=True)
    @commands.is_owner()
    async def module_load(self, ctx, *, cog: str):
        """
        Loads a module.
        :param ctx: Context, command context.
        :param cog: str, module name.
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
    async def purge_messages(self, ctx, *, count: int):
        """
        Deletes a given number of messages in chronological order.
        :param ctx: Context, command context.
        :param count: int, number of messages to delete.
        """
        channel = ctx.message.channel
        async for message in channel.history(limit=count):
            await message.delete()
    
    @commands.command(hidden=True)
    @commands.is_owner()
    async def host_type(self, ctx):
        """
        Displays whether or not the bot is hosted locally.
        """
        if "TOKEN" in os.environ:
            await ctx.send("Currently being hosted on Heroku!")
        else:
            await ctx.send("I'm being hosted locally.")

    @commands.command(hidden=True)
    @commands.is_owner()
    @commands.guild_only()
    async def save_roles(self, ctx):
        """
        Debugging tool. Please DO NOT USE.
        :param ctx: Context, command context
        """
        guild = ctx.guild
        roles = global_vars.config_manager.get_roles()
        async for member in guild.fetch_members():
            if member.id != self.bot.user.id:
                member_roles = member.roles
                if len(member_roles) > 1:
                    if str(ctx.guild.id) not in roles or str(member.id) not in roles[str(ctx.guild.id)]:
                        await global_vars.config_manager.add_role(str(ctx.guild.id), str(member.id), str(member_roles[1].id))
        await ctx.send("Saved roles successfully!")

    @commands.command(name='perms', aliases=['permissions'])
    @commands.guild_only()
    @commands.is_owner()
    async def check_permissions(self, ctx, *, member: discord.Member = None):
        """
        Displays the perms of a given member. If no member given, displays perms of author.
        :param ctx: Context, command context.
        :param member: discord.Member, given member.
        """
        if not member:
            member = ctx.author
        perms = '\n'.join(perm for perm, value in member.guild_permissions if value)
        embed = discord.Embed(title='Permissions for:', description=ctx.guild.name, colour=member.colour)
        embed.set_author(icon_url=member.avatar_url, name=str(member))
        embed.add_field(name='\uFEFF', value=perms)
        await ctx.send(content=None, embed=embed)

    @commands.command(name='uptime')
    @commands.is_owner()
    async def get_uptime(self, ctx):
        """
        Displays the bot's uptime.
        """
        uptime = datetime.datetime.now() - global_vars.start_time_date
        await ctx.send(timedelta_format(uptime))


def setup(bot):
    bot.add_cog(OwnerCog(bot))
