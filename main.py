import discord
import numpy
import json
from discord.ext import commands

bot_token = ''
guild_id = 0

bot = commands.Bot(command_prefix='>')


@bot.event
async def on_ready():
    print("Bot is ready.")
    guild = await bot.fetch_guild(guild_id)


@bot.event
async def on_command_error(ctx, error):
    print(ctx.command.name + " was invoked incorrectly.")
    print(error)


@bot.command()
async def uwu(ctx):
    await ctx.message.delete()
    await ctx.send("Owo daddy what's that?")


@bot.command()
async def ping(ctx):
    """
    Pong.
    :param ctx: Context, command context.
    :return: -
    """
    await ctx.send("Pong!")


@bot.command()
@commands.has_guild_permissions(administrator=True)
async def purge_roles(ctx):
    """
    Removes all roles from guild.
    :param ctx: Context, command context.
    :return: -
    """
    deleted_counter = 0
    guild = await bot.fetch_guild(guild_id)
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
    except discord.errors.Forbidden as e:
        print(e.text)


@bot.command()
async def color(ctx, *args):
    """
    Color command. Gives anyone the desired role color.
    :param ctx:
    :param args:
    :return:
    """
    guild = await bot.fetch_guild(guild_id)
    author = str(ctx.message.author.id)

    if len(args) == 3:
        r = int(args[0])
        g = int(args[1])
        b = int(args[2])
        color_arr = numpy.clip([r, g, b], 0, 255)
        color_obj = discord.Colour.from_rgb(int(color_arr[0]), int(color_arr[1]), int(color_arr[2]))
        role_list = guild.roles
        role = list(filter(lambda x: x.name == author, role_list))
        if len(role) > 0:
            role = role[0]
        else:
            role = None

        if role is None:
            role = await guild.create_role(name=author, color=color_obj)
            await ctx.message.author.add_roles(role)
        else:
            await role.edit(color=color_obj)
    elif len(args) == 1:
        hex_code = args[0].lstrip('#')
        rgb = tuple(int(hex_code[i:i + 2], 16) for i in (0, 2, 4))
        color_obj = discord.Colour.from_rgb(rgb[0], rgb[1], rgb[2])
        role_list = guild.roles
        role = list(filter(lambda x: x.name == author, role_list))
        if len(role) > 0:
            role = role[0]
        else:
            role = None

        if role is None:
            role = await guild.create_role(name=author, color=color_obj)
            await ctx.message.author.add_roles(role)
        else:
            await role.edit(color=color_obj)
    else:
        pass  # throw syntax error


# Driver Code
bot.run(bot_token)
