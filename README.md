Modifying Terraria 1.4.0.5 from decompiled source code made with JetBrains dotPeek
========

### The idea behind this project was to go beyond what I can do by simply modding Terraria. I was also curious about the concept of decompiling code. There was a lot of mangled code and things that I assume are far uglier than the original source code. Luckily its not too far off from the code quality at my last job so I was ready for the task. Visual Studio really tried its best to manage the size of this project. I was usually using 20-22bg of ram while navigating this code base. Luckily my home computer is a champion.

### I am excluding the dll files and Content folder from this Git repo. In order to obtain those files yourself, you will have to purchase Terraria on Steam. This will give you the Content folder and some of the dll files. In order to get the rest of the necessary dll files, you will need to extract them from Terraria.exe yourself using a tool like JetBrains dotPeek. You can NOT use this repository to recreate a functioning Terraria game for free.

### Steps Completed
* Get the game running

### Goals / Todo List
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
* Decrease respawn times for a better dying experience.
