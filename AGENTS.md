# AGENTS.md

## Project
Unity 6000.3.7f1 (Unity 6) 3D with URP. Uses Starter Assets Third Person Controller and new Input System.

## Key Packages
`com.unity.render-pipelines.universal`, `com.unity.inputsystem`, `com.unity.cinemachine`, `com.unity.ai.navigation`, `com.unity.shadergraph`, `com.unity.ugui`, TextMesh Pro.

## Scenes
- `Assets/Escenas/Main.unity` — main game scene
- `Assets/Escenas/Menu1.unity` — pause menu (loaded additively)

## Scripts (`Assets/SCRIPTS/`)
- **Player**: `PirateBehaviour.cs` (collects keys, tracks `puntos` counter), `Ataque.cs` (left-click attack animation, disables controller during attack)
- **Collectibles**: `UnlockingChest.cs` (opens when player with 3 keys enters trigger, reveals coin), `CoinBehavior.cs` (rotating coin, 0.3s delay before collectible)
- **NPCs**: `NPCinteract.cs` (E key proximity interaction via `DialogManager`), `NPCcontroller.cs` (trigger-based "hablar" animation), `NavegationPatrol.cs` (NavMeshAgent waypoint patrol)
- **UI**: `DialogManager.cs` (singleton, typewriter effect via coroutine), `Pause.cs` (Escape key, loads/unloads Menu1 additively, sets `Time.timeScale`)
- **Environment**: `Giro.cs` (rotating object with trigger sound), `activarAnimaciones.cs` (root-level)

## Input
New Input System (`com.unity.inputsystem`). Actions defined in `Assets/InputSystem_Actions.inputactions`. Keyboard access via `Keyboard.current` (Escape, E). Mouse via `Mouse.current.leftButton`.

## Architecture Notes
- `DialogManager` is a singleton accessed via `DialogManager.Instance`
- Attack (`Ataque.cs`) disables `StarterAssets.ThirdPersonController` during animation to prevent movement
- Pause uses `SceneManager.LoadScene` additive + `Time.timeScale = 0`
- `DialogManager.TypewriterEffect` uses `WaitForSecondsRealtime` so dialog continues during paused time

## Asset Structure
Third-party assets: AllSkyFree (skies), Bandit (enemy model), FreeTestCharacterAsuna, NordicFantasyCharacter_Einheri, Polytope Studio, StarterAssets, ChestFree, Rust Key, DavePixel. Terrain data in root Assets.
