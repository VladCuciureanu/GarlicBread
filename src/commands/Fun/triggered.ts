import { ApplyOptions } from '@sapphire/decorators';
import { fetch, FetchResultTypes } from '@sapphire/fetch';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import type { CommandInteraction } from 'discord.js';
import type TenorResponse from '../../interfaces/tenor';

@ApplyOptions<CommandOptions>({
	name: 'triggered',
	description: 'Replies with a random triggered gif!'
})
export class TriggeredCommand extends Command {
	public override chatInputRun(interaction: CommandInteraction) {
		if (!process.env.TENOR_TOKEN) return;
		fetch<TenorResponse>(
			`https://tenor.googleapis.com/v2/search?random=true&key=${process.env.TENOR_TOKEN}&q=triggered&limit=1`,
			FetchResultTypes.JSON
		)
			.then(async (response) => {
				return await interaction.reply({
					content: response.results[0].url
				});
			})
			.catch(async (error) => {
				this.container.logger.error(error);
				return await interaction.reply('Something went wrong when trying to fetch a triggered gif :(');
			});
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description
		});
	}
}
