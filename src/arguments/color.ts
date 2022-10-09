import { Argument, ArgumentContext, AsyncArgumentResult } from '@sapphire/framework';
import type { ColorHandler } from '../lib/structures/color';
import { parse } from '../lib/utils/color';

export class ColorArgument extends Argument<ColorHandler> {
	public async run(parameter: string, context: ArgumentContext): AsyncArgumentResult<ColorHandler> {
		const color = parse(parameter);
		return color === null ? this.error({ parameter, identifier: 'Color Argument', context }) : this.ok(color);
	}
}

declare module '@sapphire/framework' {
	interface ArgType {
		color: string;
	}
}
