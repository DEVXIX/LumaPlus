<p align="center">
 <h2 align="center">LumaPlus</h2>
 <p align="center">
 <img width="800px" src="https://imgur.com/tIjc6UU.png" align="center" alt="Logo Background" />
 <p align="center">🧑‍🌾🥕 This mod allows you to play with more than 4 players 🥕🧑‍🌾
</p>

<br>
  <p align="center">
    <a href="https://github.com/DEVXIX/LumaPlus/graphs/contributors">
      <img alt="GitHub Contributors" src="https://img.shields.io/github/contributors/DEVXIX/LumaPlus" />
    </a>
    <a href="https://github.com/DEVXIX/LumaPlus/issues">
      <img alt="Issues" src="https://img.shields.io/github/issues/DEVXIX/LumaPlus?color=0088ff" />
    </a>
    <a href="https://github.com/DEVXIX/LumaPlus/pulls">
      <img alt="GitHub pull requests" src="https://img.shields.io/github/issues-pr/DEVXIX/LumaPlus?color=0088ff" />
    </a>
    <br/>
    <br/>
    <a href="https://github.com/BepInEx/BepInEx">
      <img src="https://img.shields.io/badge/Supports-BepInEx-gray.svg?colorA=orange&colorB=FB542B&style=for-the-badge"/>
    </a>
  </p>

  <p align="center">
    <a href="https://github.com/DEVXIX/LumaPlus/issues/new/choose">Report Issues</a>
    ·
    <a href="https://github.com/DEVXIX/LumaPlus/issues/new/choose">Request Feature</a>
  </p>

<p align="center">Love the project? Please consider <a href="https://github.com/DEVXIX/LumaPlus/Contribution.md">contributing</a> to help it improve.</p>

___
### Mod Installation and Usage
Installation procedure (for beginners):

Prerequisites-

Download the mod loader v5 release: [BepInEx_Patcher_5.4.23.2.zip](https://github.com/BepInEx/BepInEx/releases/tag/v5.4.23.2)

Download the latest mod release: [Download](https://github.com/DEVXIX/LumaPlus/releases/)

1. Unzip the archive, it will contain **BepInEx.Patcher.exe**
2. Open Steam, go to your library and find Luma Island
3. Right click Luma Island and select properties, on the left side select Installed Files.
4. Select "Browse" on the right side, to browse through the game files.
5. **Drag Luma Island.exe to BepInExPatcher.exe** and run the game once.
6. In the Luma Island folder that is opened, go to BepInEx -> plugins
7. Place the "LumaPlus.dll" in the folder and the mod will be installed!


How to use the mod (for beginners): 
1) Host the lobby, make it "All Friends"
2) Invite your friends or let them join from the "Join Game Menu"
Your join game menu should look like the image below, and you're set to go. 
<img width="800px" src="https://i.imgur.com/mqvvusT.png" align="center" alt="join game menu" />


____
### Building this project

To build LumaPlus, follow these steps (This is for visual studio):

1. **Download the Project**: Start by cloning the repository from GitHub to your local machine. You can do this using the following command in your terminal or command prompt:           
`git clone https://github.com/DEVXIX/LumaPlus.git`


2. **Open the Solution in Visual Studio**: Navigate to the folder where you cloned the repository and open the `.sln` (solution) file in Visual Studio. 

3. **Reference Required DLLs**: Copy all necessary DLLs to /GameRefs folder and Bepinex dlls to /GameRefs/Bepinex

4. **Build the Project**: You can build the project by selecting **Build** from the top menu and then clicking on **Build Solution** (or simply press `Ctrl + Shift + B`). This will compile the project and generate the DLL file.

5. **Compiled DLL Folder**: You can find the compiled mod `LumaPlus.dll` in the `bin\Debug` or `bin\Release` folder.

6. **Install the DLL**: To use the mod built from source follow the same steps to install any mod / plugin, place the `LumaPlus.dll` file into the `BepInEx/plugins` folder of your game installation.


____
### Contributing
You would need to follow the "Building this project", and you can create a pull request with the necessary changes.

All help is appreciated!


___
### Credits
[DjShinter](https://github.com/DjShinter) - Wrote the mod for Luma Island, general reverse engineering for the game.

[DEVXIX](https://github.com/DEVXIX/) - Idea for the mod, and readme points, important reverse engineering points for the game.
