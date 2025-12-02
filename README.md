Modifying Terraria 1.4.0.5 from decompiled source code made with JetBrains dotPeek
========

The motivation behind this project was to go beyond what I can do by simply modding Terraria, and to see if I could do it myself the hard way. I was curious about the concept of decompiling code. There was a lot of mangled code and things that I assume are far uglier than the original source code. Luckily its not too far off from the code quality at my last job so I was ready for the task.

I am excluding the /exe files and Content folder from this Git repo. In order to obtain those files yourself, you will have to purchase Terraria on Steam. You will also need to follow below steps to legally download version 1.4.0.5 specifically. You can NOT use this repository to recreate a functioning Terraria game for free.

### How to get it running on Windows
* Clone this repo: ` gh repo clone AdamSavard/Terarria1405`
* Install the latest [Visual Code Community](https://visualstudio.microsoft.com/vs/community/).
  * Under Individual Components, check `.NET Framework 4.6 targeting pack` and `.NET Framework 4.6.1 SDK`. As of now, these are the earliest 4.x versions. I updates Terraria1405.csproj to use .NET 4.6 instead of 4.0 and it worked. In the future, you may be forced to bump up to higher versions, and get it working again.
* Download and install [XNA](https://www.microsoft.com/en-us/download/details.aspx?id=20914).
* Download Terraria 1.4.0.5:
    * Download [Terraria Depot Downloader](https://github.com/RussDev7/TerrariaDepotDownloader/releases/tag/1.8.5.7).
    * Run `TerrariaDepotDownloader.exe`.
    * In Settings tab, enter your steam Account Name and Password.
    * Make sure Terraria is installed in Steam locally.
    * In Downloader tab, choose 1.4.0.5 and Download.
    * Navigate into the TerrariaDepots folder.
    * Copy the Terraria.exe and Content folder into the root of this project.
* Build the project:
  * I did this by running this command in VS Code bash terminal at the root directory: `msbuild Terraria1405.csproj > buildout.txt 2>&1`. I think you could do it with buttons in Visual Studio Community too.
* Copy dll files from TerrariaDepots into Debug
  * `cd /c/Users/.../TerrariaDepotDownloader-1.8.5.7/TerrariaDepots
    cp *.dll /c/.../Terarria1405/bin/Debug/
    cd -`
* Run the Terraria.exe in bin/Debug and enjoy

### Tenative Personal Goals for Modding
* I want to rename some items and change NPC dialogue lines. Some things are just poorly written in my opinion.
* Increase the number of item stacks that can be dropped on the ground from 400 so less items have to disappear (this rarely happens but still). My computer will handle it.
* Items that have been dropped across the world should be saved in the world file so they will still exist after leaving and returning.
* Change the path for world/player files to a seperate directory for this version of the game, since I'm going to mess with the file formats.
* The world file needs to keep track of where the player last was before leaving. This may be a multiplayer breaking change. It mgiht be worth it to refactor away the Steam classes so this game runs purely offline.
* The world file should also store the spawned enemies including bosses. Ideally even projectiles. The result should be a version of Terraria where leaving the game and resuming later is the same as pausing. This way there is no cheesy way to run away from bosses baked into the game.
* Softcore difficulty should drop nothing; dropping money is pointless.
* Mediumcore difficulty should have all of their items destroyed, eliminating the item retrieval run.
* Hardcore difficulty isn't quite satisfying because having your character deleted on death just makes you spin up a new one. This process can be sped up by just having the player respawn with all their held items destroyed and their maximum life and mana reset back down.
* Items that can only be obtained through chests that are generated with the world should also be available through monster drops. Ideally, a world can't (easily) run dry as a result of item destruction.
* Decrease respawn times for a better dying experience. Dying while a boss is spawned should not cause the respawn timer to be longer. If anything, it should despawn the boss. That way all bosses have to be defeated without dying.

Notes
========
**Nov 2025** - Oh God a project I abandoned years ago due to AWS headhunting me, on my deadname account no less, blew up, and I don't know when I can get back to my goals with this, and won't enjoy doing it on this account. Haha. - Alice

**Dec 2025** - I got it running on my new Windows 11 PC that did not even have Visual Studio installed on it. Let's rewrite the README a bit because it was a pain. - Alice
