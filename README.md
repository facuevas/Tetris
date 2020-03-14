# Tetris
Tetris game made in Unity
All scripts and assets are included in this repository.
---
## Assets
- **Audio**
 Audio is from the gameboy version of tetris.
 This is the only thing not created from scratch.
- **Materials**
All materials were made within Unity.
- **Prefabs**
All prefabs were also made within Unity. Accomplished by combining multiple cube game objects.
- **Scenes**
Contains a Main Menu game scene when the game initially starts and the Tetris game scene when we load the Tetris game.
- **Scripts**
All C# scripts made to run the game.

## Scripts
- **Audio.cs**
Audio.cs controls the game music to turn it on or off and to adjust the volume.
- **GameMaster.cs**
GameMaster.cs controls the state of the game, the scoring, and whether the player has lost or not.
- **ITetrimino.cs**
Interface type that is for each inividual prefab.
- **Tetrimino.cs**
Tetrimino class that is the parent for each individual prefab.
Contains code to check for the edges and converts floating point positions to integer.
- **I.cs J.cs L.cs S.cs T.cs Z.cs**
All of these classes inherit from ITerimino and Tetrimino.
Each tetrimino piece has a different move from each other.
These classes accommodate for that.
- **UIManager.cs**
Controls the game UI for the player to use.
