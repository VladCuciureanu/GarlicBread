import discord
import json
import random
from pathlib import Path
from discord.ext import commands

class MiscCog(commands.Cog):
    def __init__(self, bot):
        self.bot = bot

    @commands.command(name='uwu', aliases=['owo'])
    async def uwu(self, ctx, *, prompt : str = ""):
        """
        Definitely wont explain this.
        :param ctx: Context, command context.
        :param prompt: prompt shortcuts
        """
        owo_prompts_file = Path("owo_prompts.json")
        owo_prompts = json.loads(owo_prompts_file.read_text())
        text = owo_prompts["?"]
        if prompt in owo_prompts:
            text = owo_prompts[prompt]
        await ctx.send(str(text))

    @commands.command(name='hmm', aliases=['hmmm','hmmmm'])
    async def hmm(self, ctx):
        """
        Toss a coin.
        :param ctx: Context, command context.
        """
        await ctx.send("...fuck.")

    @commands.command(name='roll')
    async def roll(self, ctx, d : int = 6):
        """
        Rolls a dice in given size.
        :param ctx: Context, command context.
        """
        if d >= 1:
            roll = random.randint(1, d)
            await ctx.send("Rolled a D"+str(d)+" and got "+str(roll)+".")
        else:
            await ctx.send("Dice cant have 0 sides or less...")


def setup(bot):
    bot.add_cog(MiscCog(bot))
