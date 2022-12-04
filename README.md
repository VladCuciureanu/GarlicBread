# Garlic Bread

[![Twitter Handle][]][twitter badge]

<img align="right" src="assets/logo.png" height="150px" alt="TBA">

**Garlic Bread** is a _fun_, _simple_ and _useful_ bot written
using **Discord.JS**. It has some features that I needed for my guild. This bot is designed for casual, simple guilds.

### Features

- Customizable roles
- Dice rolling
- Random Kanye quotes
- Moderation tools
- Remote maintenance

... and more useful tools like 'userinfo' and 'avatar'.

### Getting Started

1. Clone the repo:

```sh
git clone https://github.com/VladCuciureanu/GarlicBread.git
```

2. Duplicate the .env template:

```sh
cp .env .env.local
```

3. Set the appropriate value for the environment variables inside:

```js
DISCORD_TOKEN=CENSORED
TENOR_TOKEN=CENSORED
OWNERS=405688243292733440
DATABASE_URL="file:./dev.db"
```

4. Build the source code:

```sh
pnpm build
```

5. Run the following command and enjoy!

```sh
pnpm start
```

### Contributing

We appreciate your help!

To contribute, please read our
[contributing instructions](https://github.com/VladCuciureanu/GarlicBread/blob/main/CONTRIBUTING.md).

[twitter badge]: https://twitter.com/intent/follow?screen_name=VladCuciureanu_
[twitter handle]: https://img.shields.io/twitter/follow/VladCuciureanu_.svg?style=social&label=Follow
