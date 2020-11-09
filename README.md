<!-- PROJECT LOGO -->
<p align="center">
  <a href="https://github.com/VladCuciureanu/garlic-bread">
    <img src="logo.png" alt="Logo" width="160" height="160">
  </a>

  <h3 align="center">Garlic Bread</h3>

  <p align="center">
    Fresh out of the oven.
    <br />
    <a href="https://github.com/VladCuciureanu/garlic-bread/issues">Report Bug</a>
    ·
    <a href="https://github.com/VladCuciureanu/garlic-bread/issues">Request Feature</a>
    <br />
    <br />
    <a href="https://www.python.org/downloads/release/python-360">
      <img alt="Made with: Python 3.6" src="https://img.shields.io/badge/python-3.6-blue.svg" target="_blank" />
    </a>
    <a href="https://github.com/VladCuciureanu/garlic-bread/blob/main/LICENSE">
      <img alt="License: MIT" src="https://img.shields.io/badge/License-MIT-blue.svg" target="_blank" />
    </a>
  </p>
</p>



<!-- TABLE OF CONTENTS -->
## Table of Contents

* [About the Project](#about-the-project)
  * [Built With](#built-with)
* [Getting Started](#getting-started)
  * [Setup](#setup)
* [Usage](#usage)
* [Contributing](#contributing)
* [License](#license)
* [Contact](#contact)



<!-- ABOUT THE PROJECT -->
## About The Project

#### Click [here](https://discord.com/api/oauth2/authorize?client_id=697481393609113810&permissions=8&scope=bot) to invite Garlic Bread to your server

With my private Discord server slowly rising in members count, I had to face the cold hard truth...\
Something was still missing...
So I made this bot to fill both metaphorical holes in the server and in my heart.

Implemented features:
 * Custom role name and color
 * Hot-swappable modules (cogs)
 * Physical storage (.grlk files)
 * Ephemeral storage (e.g. for Heroku's free tier hosting)
 * Purge messages
 * Uwu

### Built With

* [Python 3.6](https://www.python.org/downloads/release/python-360/)
* [Discord.py](https://github.com/Rapptz/discord.py)



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Setup

1. Clone the repo:
```sh
git clone https://github.com/VladCuciureanu/garlic-bread.git && cd garlic-bread
```
2. Install requirements:
```sh
pip3 install -r requirements.txt
```
3.1 Persistent storage setup:
```
Use this if you have a dedicated VPS or if you're hosting it on your own server or PC.
No pre-setup is needed. Just skip to step 4 and follow the on-screen instructions.
```
3.2 Ephemeral storage setup:
```
I added this for budget bot hosts. For example, Heroku's free tier.
This setup takes a bit more time to setup but not too much.

 1. Setup a dedicated storage text channel. It can be in your discord server
or in an auxiliary discord server. It can even be the DM text channel between you
and Garlic Bread, but that takes a bit more tinkering in order to get the channel ID.

Note: To prevent data-loss, make sure only Garlic Bread can post there and no other
posts are present there.

 2. Copy that channel's ID. 

 3. Add the following environment variables (config vars):
GARLIC_TOKEN: {Your Bot Token}
GARLIC_STORAGE_CHANNEL: {Your dedicated text channel's ID}
```
4. Run bot.py:
```sh
python3 bot.py
```
5. If all went well you should see something like this:
```
Successfully logged in and booted...!
Logged in as ----> {Your bot's username and #}
ID: {Your bot's user ID}
Version: {Discord.py version}
```



<!-- USAGE EXAMPLES -->
## Usage

For commands use the 'help' command on the server (">help") or in the bot's DMs ("?help")



<!-- CONTRIBUTING -->
## Contributing

Contributions are what make the open source community such an amazing place to be learn, inspire, and create. Any contributions you make are **greatly appreciated**.

1. Fork the Project
2. Create your Feature Branch (`git checkout -b feature/AmazingFeature`)
3. Commit your Changes (`git commit -m 'Add some AmazingFeature'`)
4. Push to the Branch (`git push origin feature/AmazingFeature`)
5. Open a Pull Request



<!-- LICENSE -->
## License

Distributed under the MIT License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Vlad CUCIUREANU - [@PollyWantsToDie](https://twitter.com/PollyWantsToDie) - vlad.c.cuciureanu@gmail.com\
Sandevil Sandh (Logo Pic) - [@sandevil](https://unsplash.com/@sandevil)\
Project Link: [https://github.com/VladCuciureanu/garlic-bread](https://github.com/VladCuciureanu/garlic-bread)
