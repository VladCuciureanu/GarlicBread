import discord
from discord.ext import commands


class MiscCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='uwu', aliases=['owo'])
    async def uwu(self, ctx, prompt: str):
        """
        Definitely wont explain this.
        :param ctx:
        :param prompt: different texts
        :return:
        """
        text = None
        if prompt == "goodnight" or prompt == "gn":
            text = "Rawr xD goodnight OwO Hehe nuzzles"
        elif prompt == "goodmorning" or prompt == "gm":
            text = "Owo good mowning xP"
        elif prompt == "sad":
            text = "\*sad uwu noises\*"
        else:
            text = "Owo what's this?"
        await ctx.send(str(text))


def setup(bot):
    bot.add_cog(MiscCog(bot))
