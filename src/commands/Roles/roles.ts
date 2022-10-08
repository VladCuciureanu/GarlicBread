import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import type { CommandInteraction, Role } from 'discord.js';
import prisma from '../../lib/prisma';

@ApplyOptions<CommandOptions>({
	name: 'role',
	description: `Configure a personalized role`,
	preconditions: ['GuildOnly']
})
export class RoleCommand extends Command {
	public override async chatInputRun(interaction: CommandInteraction) {
		const userId = interaction.user.id;
		const guildId = interaction.guild!.id;
		const newName = interaction.options.getString('name', false);
		const newColor = interaction.options.getString('color', false);
		interaction.deferReply({ ephemeral: true });

		var role: Role | undefined;

		// Try to fetch role
		await prisma.customRole
			.findFirst({
				where: {
					userSnowflake: userId,
					guildSnowflake: guildId
				}
			})
			.then(async (entry) => {
				if (entry !== null) {
					role = await new Promise<Role | undefined>((resolve, _) => {
						interaction.guild!.roles.fetch(entry.roleSnowflake).then((result) => resolve(result !== null ? result : undefined));
					});
				}
			});

		// If no db entry or no role could be found, create one
		if (role === undefined) {
			role = await new Promise<Role>((resolve, _) => {
				interaction.guild!.roles.create({ hoist: true, mentionable: false }).then((role) => {
					interaction
						.guild!.members.fetch(userId)
						.then((user) => user.roles.add(role))
						.then(
							async () =>
								await prisma.customRole.upsert({
									create: {
										userSnowflake: userId,
										guildSnowflake: guildId,
										roleSnowflake: role.id
									},
									update: {
										roleSnowflake: role.id
									},
									where: {
										userSnowflake_guildSnowflake: { userSnowflake: userId, guildSnowflake: guildId }
									}
								})
						)
						.then(() => resolve(role));
				});
			});
		}

		// Set role's properties
		if (newName !== null) {
			await new Promise((resolve, _) => {
				this.container.logger.info(`Setting new name for role ${(role as Role).id}...`);
				role?.setName(newName).then(() => resolve(null));
			});
		}

		if (newColor !== null) {
			await new Promise((resolve, _) => {
				this.container.logger.info(`Setting new color for role ${(role as Role).id}...`);
				role?.setColor('DARK_GOLD').then(() => resolve(null));
			});
		}

		interaction.followUp({ content: '✨ Enjoy your snazzy new role! ✨', ephemeral: true });
	}

	public override registerApplicationCommands(registry: ApplicationCommandRegistry): void {
		registry.registerChatInputCommand({
			name: this.name,
			description: this.description,
			options: [
				{
					type: 'STRING',
					required: false,
					name: 'name',
					description: `What should your role be named?`
				},
				{
					type: 'STRING',
					required: false,
					name: 'color',
					description: 'What color should your role have?'
				}
			]
		});
	}
}
