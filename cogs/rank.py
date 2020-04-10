import discord
from discord.ext import commands
import os
import pickle


def rgb_to_color(r, g, b):
    """
    Creates a discord.Colour from given rgb values.
    Note: r,g,b are supposed to be between 0-255.
    :param r: int, red value.
    :param g: int, green value.
    :param b: int, blue value.
    :return: discord.Colour, resulting color.
    """

    # Upper clamp
    if r > 255:
        r = 255
    if g > 255:
        g = 255
    if b > 255:
        b = 255

    # Lower clamp
    if r < 0:
        r = 0
    if g < 0:
        g = 0
    if b < 0:
        b = 0

    return discord.Colour.from_rgb(r, g, b)


class RankCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot
        self.ranks = {}
        if os.path.exists("ranks.grlk"):
            self.ranks = pickle.load(open("ranks.grlk", "rb"))
            print(self.ranks)
        else:
            pickle.dump(self.ranks, file=open("ranks.grlk", "wb"))

    @commands.command()
    @commands.guild_only()
    async def role_color(self, ctx, *args):
        """
        Color command. Gives anyone the desired role color.
        :param ctx: Context, command context.
        :param args: array of arguments from command
        :return: -
        """
        guild = await self.bot.fetch_guild(ctx.guild.id)
        author = str(ctx.message.author.id)

        color_obj = None
        if len(args) == 3:
            color_obj = rgb_to_color(int(args[0]), int(args[1]), int(args[2]))
        elif len(args) == 1:
            hex_code = args[0].lstrip('#')
            rgb = tuple(int(hex_code[i:i + 2], 16) for i in (0, 2, 4))
            color_obj = discord.Colour.from_rgb(rgb[0], rgb[1], rgb[2])
        else:
            raise ValueError("Invalid number of arguments.")

        if author not in self.ranks:
            role = await guild.create_role(name=author, color=color_obj, mentionable=False, reason="Color")
            self.ranks[author] = role.id
            pickle.dump(self.ranks, file=open("ranks.grlk", "wb"))
            await ctx.message.author.add_roles(role)
        else:
            role = guild.get_role(self.ranks[author])
            await role.edit(color=color_obj)

    @commands.command()
    @commands.guild_only()
    async def role_name(self, ctx, *args):
        """
        Renames personal role to given name.
        :param ctx: Context, command context.
        :param args: array of words.
        :return: -
        """
        guild = await self.bot.fetch_guild(ctx.guild.id)
        author = str(ctx.message.author.id)

        if len(args) == 0:
            raise ValueError("Must provide a role name.")

        role_name = ""
        for word in args:
            role_name += str(word)
            role_name += " "

        if author not in self.ranks:
            role = await guild.create_role(name=role_name, mentionable=False, reason="Role")
            self.ranks[author] = role.id
            pickle.dump(self.ranks, file=open("ranks.grlk", "wb"))
            await ctx.message.author.add_roles(role)
        else:
            role = guild.get_role(self.ranks[author])
            await role.edit(name=role_name)


def setup(bot):
    bot.add_cog(RankCog(bot))
