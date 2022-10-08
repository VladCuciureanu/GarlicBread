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

		var roleEntry = await prisma.customRole.findFirst({
			where: {
				userSnowflake: userId,
				guildSnowflake: guildId
			}
		});

		if (roleEntry === null) {
			const newRole = await new Promise<Role>((resolve, _) => {
				interaction.guild!.roles.create({ hoist: true, mentionable: false }).then((newRole) => resolve(newRole));
			});
			await new Promise((resolve, _) => {
				interaction
					.guild!.members.fetch(userId)
					.then((user) => user.roles.add(newRole))
					.then(() => resolve(null));
			});
			roleEntry = await prisma.customRole.create({
				data: {
					userSnowflake: userId,
					guildSnowflake: guildId,
					roleSnowflake: newRole.id
				}
			});
		}

		var role = await new Promise<Role | null>((resolve, _) => {
			interaction.guild!.roles.fetch(roleEntry!.roleSnowflake).then((foundRole) => resolve(foundRole));
		});

		if (role === null) {
			role = await new Promise<Role>((resolve, _) => {
				interaction.guild!.roles.create({ hoist: true, mentionable: false }).then((newRole) => resolve(newRole));
			});
			await new Promise((resolve, _) => {
				interaction
					.guild!.members.fetch(userId)
					.then((user) => user.roles.add(role as Role))
					.then(() => resolve(null));
			});
			await prisma.customRole.update({
				where: { userSnowflake_guildSnowflake: { userSnowflake: userId, guildSnowflake: guildId } },
				data: {
					roleSnowflake: role.id
				}
			});
		}

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
