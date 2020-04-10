import discord
from discord.ext import commands


class MiscCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='uwu', aliases=['owo'])
    async def uwu(self, ctx):
        """
        Definitely wont explain this.
        :param ctx:
        :return:
        """
        await ctx.send('Owo daddy what\'s that?')


def setup(bot):
    bot.add_cog(MiscCog(bot))
