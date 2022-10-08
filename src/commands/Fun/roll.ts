import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import { randomInt } from 'crypto';
import { CommandInteraction, MessageEmbed } from 'discord.js';

@ApplyOptions<CommandOptions>({
	name: 'roll',
	aliases: ['dice'],
	description: `Roll a dice`
})
export class RollCommand extends Command {
	public override async chatInputRun(interaction: CommandInteraction) {
		const sides = interaction.options.getInteger('sides', false);
		const result = randomInt(1, sides !== null ? sides : 6);

		const embed = new MessageEmbed().setTitle(`🎲 You rolled a(n) ${result}.`).setColor('#0x00ae86');

		return await interaction.reply({ embeds: [embed] });
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description,
			options: [
				{
					type: 'INTEGER',
					required: true,
					name: 'sides',
					description: `How many sides should the dice have?`
				}
			]
		});
	}
}
