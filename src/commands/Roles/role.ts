import { ApplyOptions } from '@sapphire/decorators';
import { ApplicationCommandRegistry, Command, CommandOptions } from '@sapphire/framework';
import type { CommandInteraction, HexColorString, Role } from 'discord.js';
import prisma from '../../lib/prisma';
import { parse as parseColor } from '../../lib/utils/color';

@ApplyOptions<CommandOptions>({
	name: 'role',
	description: `Configure a personalized role`,
	preconditions: ['GuildOnly']
})
export class RoleCommand extends Command {
	public override async chatInputRun(interaction: CommandInteraction) {
		const userId = interaction.user.id;
		const guildId = interaction.guild!.id;
		const newName = interaction.options.getString('role-name', false);
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

		if (newColor !== null && parseColor(newColor) !== null) {
			const color = parseColor(newColor as string)!.hex.toString();
			await new Promise((resolve, _) => {
				this.container.logger.info(`Setting new color for role ${(role as Role).id}...`);
				role?.setColor(color as HexColorString).then(() => resolve(null));
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
					name: 'role-name',
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
