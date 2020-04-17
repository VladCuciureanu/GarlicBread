import discord
from discord.ext import commands


class BaseCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command()
    @commands.guild_only()
    async def joined(self, ctx, *, member: discord.Member = None):
        """
        Displays when a given member joined the server.
        :param ctx: Context, command context.
        :param member: discord.Member, given member.
        :return: -
        """
        if not member:
            member = ctx.author
        await ctx.send(f'{member.display_name} joined on {member.joined_at}')

    @commands.command()
    async def ping(self, ctx):
        """
        Simple debug command.
        :param ctx: Context, command context.
        :return: -
        """
        await ctx.send('Pong >w<')

    @commands.command(name='perms', aliases=['permissions'])
    @commands.guild_only()
    async def check_permissions(self, ctx, *, member: discord.Member = None):
        """
        Displays the perms of a given member. If no member given, displays perms of author.
        :param ctx: Context, command context.
        :param member: discord.Member, given member.
        :return: -
        """
        if not member:
            member = ctx.author
        perms = '\n'.join(perm for perm, value in member.guild_permissions if value)
        embed = discord.Embed(title='Permissions for:', description=ctx.guild.name, colour=member.colour)
        embed.set_author(icon_url=member.avatar_url, name=str(member))
        embed.add_field(name='\uFEFF', value=perms)
        await ctx.send(content=None, embed=embed)


def setup(bot):
    bot.add_cog(BaseCog(bot))
