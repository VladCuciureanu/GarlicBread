import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import { CommandInteraction, MessageEmbed } from 'discord.js';

@ApplyOptions<CommandOptions>({
	name: 'userinfo',
	description: `Responds with a user's info`,
	preconditions: ['GuildOnly']
})
export class AvatarCommand extends Command {
	public override async chatInputRun(interaction: CommandInteraction) {
		const user = interaction.options.getUser('user', true);
		const guildUser = interaction.guild?.members.cache.get(user.id);

		const embed = new MessageEmbed()
			.setTitle(user.username)
			.setImage(user.displayAvatarURL({ dynamic: true }))
			.setFields([
				{ name: 'Creation Date', value: dateHumanizer(user.createdAt) },
				{ name: 'Join Date', value: dateHumanizer(guildUser?.joinedAt!) }
			])
			.setColor('#0x00ae86');

		return await interaction.reply({ embeds: [embed] });
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description,
			options: [
				{
					type: 'USER',
					required: true,
					name: 'user',
					description: `Which user's info do you want to look at?`
				}
			]
		});
	}
}

const dateHumanizer = (date: Date) =>
	date.toLocaleDateString('en-gb', {
		year: 'numeric',
		month: 'long',
		day: 'numeric'
	});
