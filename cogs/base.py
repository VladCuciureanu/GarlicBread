import json
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

    @commands.command()
    async def help(self, ctx):
        """
        Displays Garlic Bread's help doc.
        :param ctx: Context, command context.
        :return: -
        """
        with open("help_embed.json") as json_file:
            embed_json = json.load(json_file)
            for embed in embed_json["embeds"]:
                embed_msg = discord.Embed.from_dict(embed)
                await ctx.message.author.send(embed=embed_msg)
        await ctx.send('Sent help in DMs •w<')


def setup(bot):
    bot.add_cog(BaseCog(bot))
