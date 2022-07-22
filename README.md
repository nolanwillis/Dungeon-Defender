# Dungeon Defender

## About

### Overview
Dungeon Defender is my first project using Unity and C#. It’s a 3D side scroller/platformer, set in a medieval dungeon. My goal for this project was to become familiar with the Unity editor and to create components I could reuse for future projects. The mechanics of this game are very simple: get the highest score possible by killing enemies, before running out of lives.

### Technologies Used
- Unity (2021.3.2f1)
- Visual Studio 2022
- GitHub

### Features
- Physics-based Movement Controller
- Melee Combat
- Animation
- Persistent Data (JSON)
- Spawning System
- Enemy AI
- Menus/UI
- Post Processing (HDRP)
- Lighting
- Health
- Sound

### Asset Sources:
- Unity Asset Store: terrain, props, and sounds
- Mixamo: character meshes and animations
- Freesound.org: sounds

## Gameplay

### Keyboard Controls
- A: move left
- D: move right
- Left Click: attack
- Right Click: block
- Space: jump

### Known Bugs
- If the player lands perfectly on top of an enemy they can get stuck.
- Enemies can get too close to platform edges, occasionally blocking the player from jumping.
- Player block animation not playing in certain situations.
- Enemy run animation can be delayed sometimes. 

## Code Base Overview

### AudioManager.cs
- Stores and sets up all sound files in the Game scene.
- Enables other scripts to play sounds.
- Allows pitch and volume to be changed in the editor for each sound.

### AnimationStateSound.cs
- State machine behavior that plays a sound when entering an animation state.

### CameraFollow.cs
- Enables the main camera to follow the player.
- Allows the target, smooth speed, offset, and velocity to be changed in the editor.

### DataFileHandler.cs
- Enables GameDataManager.cs to save and load data from a data file in local storage.
- Enables GameDataManager.cs to serialize/deserialize data in JSON.
- Enables GameDataManager.cs to encrypt game data using xor encryption.

### Destroy.cs
- State machine behavior that handles player and enemy death.
- On state enter and animator is attached to the player: disables input.
- On state enter and animator is attached to an enemy: removes capsule collider (so player can walk over enemy while dying) and removes enemy controller (stops following player when dying).
- On state exit and animator is attached to the player: removes lives, if there’s 0 lives, saves the game and switches to the Death Menu scene. Also opens the spawn point used by the player and respawns the player.
- On state exit and animator is attached to an enemy: increases score, opens spawn point used by enemy, spawns a new enemy.

### DetectHit.cs
- State machine behavior that handles enemy hit detection (only used when enemy attack animations are played).
- On state enter: calls the StartDelayDetectHit method in EnemyController.cs.

### EnemyController.cs
- Enables enemies to search, follow, and attack the player.
- Handles all enemy animations.
- Uses coroutines to delay time between attacks and to enable DetectHit.cs to trigger a delayed hit detection.
- Allows the following to be changed in the editor: walk point range, sight range, attack range, hit detection range, and enemy damage.

### GameData.cs
- Contains all data that will be stored in the game data file.
- Enables GameDataManager.cs to instantiate a GameData object, which has a high score parameter. 

### GameDataManager.cs
- Loads the game data when the game starts.
- If there’s no data file stored locally, it creates a new one by instantiating a GameData object.
- Saves game data if the application quits.
- Enables other scripts to save game data.
- Allows encryption to be toggled and the data file name to be changed in the editor.

### ISaveSystem.cs
- Interface implemented by all classes that need to store or load data.
- All classes that implement this interface must have a save and load function. These functions are then called in the GameDataManager.cs to save or load data.

### KillZone.cs
- Enables a 3D volume to kill the player.

### LevelManager.cs
- Sets up the initial game by spawning the player and enemies.
- Enables other scripts to spawn both the player and an enemy at a spawn point (spawn point is found by calling GetSpawnPoint in SpawnPointManager.cs.

### Lives.cs
- Sets the player's initial amount of lives.
- Enables other scripts to change the amount of lives.

### Metadata.cs
- Stores the spawn point assigned to the player or enemy (probably could be refactored and removed).

### PlayerCombat.cs
- Enables other scripts to cause the player to block, attack, attack while running, and detect hits.
Handles player combat animations.
- Allows the enemy layer, attack point transform, and attack range to be changed in the editor.
 
### PlayerHealth.cs
- Name is kind of confusing, because this script handles player and enemy health.
- Sets the initial health for the gameObject this script is attached to.
- Enables other scripts to apply damage to the gameObject this script is attached to.
- Allows the gameObject’s maximum health to be changed in the editor.

### PlayerInput.cs
- Auto-generated script from Unity’s input system.

### PlayerInputManager.cs
- Enables PlayerManager.cs to handle all input from the player by calling methods in PlayerLocomotion.cs and PlayerCombat.cs.
- Allows other scripts to disable certain input (i.e if player is running disable block).

### PlayerLocomotion.cs
- Enables other scripts to make the player move, jump, fall and rotate.
- Allows the following to be changed in the editor: acceleration, backwards acceleration (decceleration), rotation speed, air control, leaping velocity boost, and jump force. 

### PlayerManager.cs
- Calls HandleAllInput in PlayerInputManager.cs in an update function.
- Calls HandleAllMovement in PlayerLocomotion.cs in a fixed update function. 

### ResetBool.cs
- State machine behavior that resets a boolean parameter on state enter.

### ResetInt.cs
- State machine behavior that resets an integer parameter on state enter.

### Sound.cs
- Enables AudioManager.cs to instantiate sound objects.
- Each sound object has a name, audio clip, audio source, volume and pitch parameter.
- Allows the name, audio clip, volume and pitch to be changed in the editor.

### SpawnPointManager.cs
- Keeps track of all spawn point transforms.
- Keeps track of which spawn points are in use.
- Initially sets all spawn points to open.
- Allows other scripts to find an open spawn point index and open a spawn point. 

## Looking back

### Conclusion
This project solidified my basic understanding and confidence level while using Unity. There’s still a lot I didn’t cover but hopefully making this project will make future projects easier to create. Before Unity I was trying to learn Unreal Engine using online courses. After finishing the courses I still lacked fundamental knowledge and didn’t know enough to make my own project. So, I decided to try Unity, but instead of online courses I decided to learn by implementing features. When I got stuck I would read forums, docs and watch youtube videos of how other people overcame the same problems. This method worked far better than the latter, I learned more in 2 months than a year of sporadically completing online courses.

### Some Things I Would Change Going Forward
- Try to follow some of the SOLID principles. Especially the single-responsibility and dependency inversion principles. Several methods and classes perform multiple functions and some of the classes are very tightly coupled.
- Apply some design. The nature of this project was ad hoc, the focus was more on learning unity than creating a well designed code base. For my next project I want to change that by doing planning, analysis, and design then implementing. I think gathering requirements, creating a use case diagram and a class diagram will make development far easier.

 
