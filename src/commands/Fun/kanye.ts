import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import { CommandInteraction, MessageEmbed } from 'discord.js';
import { fetch, FetchResultTypes } from '@sapphire/fetch';

@ApplyOptions<CommandOptions>({
	name: 'kanye',
	description: 'Replies with a random Kanye quote'
})
export class KanyeCommand extends Command {
	public override chatInputRun(interaction: CommandInteraction) {
		fetch<KanyeResponse>('https://api.kanye.rest/?format=json', FetchResultTypes.JSON)
			.then(async (response) => {
				const quote = response.quote;
				const embed = new MessageEmbed()
					.setColor('#F4D190')
					.setAuthor({
						name: 'Kanye West',
						url: 'https://kanye.rest',
						iconURL: 'https://i.imgur.com/SsNoHVh.png'
					})
					.setDescription(quote)
					.setTimestamp()
					.setFooter({
						text: 'Powered by kanye.rest'
					});
				return await interaction.reply({ embeds: [embed] });
			})
			.catch(async (error) => {
				this.container.logger.error(error);
				return await interaction.reply('Something went wrong when fetching a Kanye quote :(');
			});
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description
		});
	}
}

interface KanyeResponse {
	quote: string;
}
