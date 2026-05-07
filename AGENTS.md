# AGENTS.md

Unity 6000.3.7f1 (Unity 6) 3D with URP. Scenes: `Assets/Escenas/Main.unity` (index 1, entry), `Assets/Escenas/Menu1.unity` (index 2, pause menu). Custom scripts live in `Assets/SCRIPTS/`.

## Dependencies

- **StarterAssets** — `StarterAssets.ThirdPersonController` referenced by `Ataque.cs`
- **Cinemachine**, **TextMeshPro**, **NavMesh** (`com.unity.ai.navigation`), **Unity Input System** (`com.unity.inputsystem`)
- Input scripts read `Keyboard.current` / `Mouse.current` directly; `.inputactions` assets are NOT wired

## Tags & Layers

- **Tags**: `Player`, `Llave`, `Coin`, `Enemigos`, `CinemachineTarget`
- **Custom layers**: `Enemy` (7), `pp` (6)
- **Light layers**: 0–7 defined for URP

## Gotchas

- **Class names differ from filenames** — `UnlockingChest.cs` → `Unlocking`, `CoinBehavior.cs` → `Moneda`, `Pause.cs` → `PauseManager`
- **`Ataque.cs` bug** — `atacar()` never disables `_fps` before the attack; `ReenableController` only re-enables it. Controller is NOT actually locked during attack animation
- **Score** — `PirateBehaviour.SetPuntos()` is the only public setter; `getPuntos()` for reads. Keys (`"Llave"` tag) auto-increment score by 1 each; 3 needed to open chest
- **`Unlocking` (chest)** — `consumirLlaves` flag exists but is NOT wired; chest opens when `pirate.getPuntos() >= 3` and awards `puntosBonus` (default 5)
- **`Moneda` (coin)** — rotates on Z-axis, 0.3s delay before collectible, logs but does NOT award points
- **`Pause`** — Loads `Menu1` additively and sets `Time.timeScale = 0`; unloads on resume. `PauseMenuController` on Menu1 handles resume/restart/quit buttons
- **`RespawnManager`** — disables `Ataque` during respawn coroutine, re-enables after teleport
- **`AudioManager`** — singleton via public static `Instance` field; not `DontDestroyOnLoad`
- **Save system** — `SaveSystem` uses plain JSON via `JsonUtility` at `Application.persistentDataPath`; no encryption. `SaveData` tracks `llaves` and `puntos` only
- **`BanditDialog`** — uses E key to toggle typewriter dialog; closes panel if player walks away
- **`ArmaDisparo`** — fires on mouse left click; sets `Bullet.damage` after instantiate

## Architecture

- **Health** — generic component; `Destroy(gameObject)` at 0 HP, no death animation hook
- **NavegationPatrol** — cycles through public `puntos[]` waypoints via NavMeshAgent
- **LedgeDetector** — stops NavMeshAgent when no ground ahead; unpause after 1s
- **Giro** — rotates Y-axis + plays AudioSource on trigger (one-shot, no loop)
- **NPCcontroller** — triggers `"hablar"` animator bool on player proximity
