import discord
from discord.ext import commands


class MiscCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='uwu', aliases=['owo'])
    async def uwu(self, ctx, prompt: str):
        """
        Definitely wont explain this.
        :param ctx: Context, command context.
        :param prompt: prompt shortcuts
        """
        text = "Owo what's this?"
        if prompt == "goodnight" or prompt == "gn":
            text = "Rawr xD goodnight OwO Hehe nuzzles"
        elif prompt == "goodmorning" or prompt == "gm":
            text = "Owo good mowning xP"
        elif prompt == "sad":
            text = "\*sad uwu noises\*"
        await ctx.send(str(text))

    @commands.command(name='hmm', aliases=['hmmm','hmmmm'])
    async def hmm(self, ctx):
        """
        Toss a coin.
        :param ctx: Context, command context.
        """
        await ctx.send("...fuck.")


def setup(bot):
    bot.add_cog(MiscCog(bot))
