import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import type { CommandInteraction } from 'discord.js';

@ApplyOptions<CommandOptions>({
	name: 'purge',
	aliases: ['nuke'],
	description: `Delete a given number of messages from current text channel.`,
	preconditions: ['OwnerOnly', 'GuildTextOnly']
})
export class PurgeCommand extends Command {
	public override async chatInputRun(interaction: CommandInteraction) {
		const count = interaction.options.getInteger('count', true);
		interaction.channel?.messages.fetch({ limit: count }).then(async (messages) => {
			interaction.deferReply({ ephemeral: true });
			await Promise.all(messages.map((message) => message.delete())).then(() =>
				interaction.followUp({ content: `💣 Purged ${messages.size} messages.`, ephemeral: true })
			);
		});
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description,
			options: [
				{
					type: 'INTEGER',
					required: true,
					name: 'count',
					description: `How many messages should be deleted?`
				}
			]
		});
	}
}
