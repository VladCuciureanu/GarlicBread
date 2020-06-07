import discord
import json
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


def setup(bot):
    bot.add_cog(MiscCog(bot))
