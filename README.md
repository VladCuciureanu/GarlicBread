<!-- PROJECT LOGO -->
<p align="center">
  <a href="https://github.com/VladCuciureanu/GarlicBread">
    <img src="logo.png" alt="Logo" width="160" height="160">
  </a>

  <h3 align="center">Garlic Bread</h3>

  <p align="center">
    Fresh out of the oven.
    <br />
    <b>
    Notice: I stopped working on it since I disagree with Discord's privacy policy.
    <br />
    </b>
    <a href="https://github.com/VladCuciureanu/GarlicBread/issues">Report Bug</a>
    ·
    <a href="https://github.com/VladCuciureanu/GarlicBread/issues">Request Feature</a>
    <br />
    <br />
    <img src="https://img.shields.io/badge/license-Unlicense-blue.svg" href="http://unlicense.org/">
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
 * Purge messages
 * Uwu

### Built With

* [Net 5.0](https://dotnet.microsoft.com/download/dotnet/5.0)
* [Disqord](https://github.com/Quahu/Disqord)
* [Qmmands](https://github.com/Quahu/Qmmands)



<!-- GETTING STARTED -->
## Getting Started

To get a local copy up and running follow these simple steps.

### Setup

1. Clone the repo:
```sh
git clone https://github.com/VladCuciureanu/GarlicBread.git && cd GarlicBread
```
2. Copy example config and fill in required info:
```sh
cp garlicbread.appsettings.example.json garlicbread.appsettings.json
```
3. Install requirements:
```sh
dotnet restore
```
4. Install migrations:
```sh
dotnet ef database update
```
4. Run bot:
```sh
dotnet run
```



<!-- USAGE EXAMPLES -->
## Usage

For commands use the 'help' command on the server ("gb help") or in the bot's DMs.



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

Distributed under the Unlicense License. See `LICENSE` for more information.



<!-- CONTACT -->
## Contact

Vlad CUCIUREANU - [@VladinskiDev](https://twitter.com/VladinskiDev) - vlad.c.cuciureanu@gmail.com\
Sandevil Sandh (Logo Pic) - [@sandevil](https://unsplash.com/@sandevil)\
Project Link: [https://github.com/VladCuciureanu/GarlicBread](https://github.com/VladCuciureanu/GarlicBread)
