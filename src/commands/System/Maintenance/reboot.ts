import { ApplyOptions } from '@sapphire/decorators';
import { Command } from '@sapphire/framework';
import { send } from '@sapphire/plugin-editable-commands';
import type { Message } from 'discord.js';

@ApplyOptions<Command.Options>({
	preconditions: ['OwnerOnly']
})
export class RebootCommand extends Command {
	public async messageRun(message: Message) {
		const content = '🚨 Garlic Bread is rebooting!';
		await send(message, content).catch((error) => this.container.logger.fatal(error));
		process.exit(0);
	}
}
