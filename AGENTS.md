# AGENTS.md

Unity 6000.3.7f1 (Unity 6) 3D with URP. Scenes: `Assets/Escenas/Main.unity`, `Menu1.unity`. Custom scripts live in `Assets/SCRIPTS/`.

## Gotchas

- **Class names differ from filenames** — `UnlockingChest.cs` → `Unlocking`, `CoinBehavior.cs` → `Moneda`, `Pause.cs` → `PauseManager`, `NPCinteract.cs` → `NPCInteract`
- **Input** — Scripts use `Keyboard.current` / `Mouse.current` directly; `.inputactions` files are not wired in
- **Score** — `PirateBehaviour.SetPuntos()` is the only public setter; `getPuntos()` for reads. Keys (`"Llave"` tag) auto-increment score by 1 each; 3 needed to open chest
- **`DialogManager` / `AudioManager`** — singletons via public static `Instance` field
- **Dialog survives pause** — `TypewriterEffect` uses `WaitForSecondsRealtime`
- **Pause** — Loads `Menu1` additively and sets `Time.timeScale = 0`; unloads on resume
- **`Ataque.cs`** disables `StarterAssets.ThirdPersonController` during attack; `activarAnimaciones.cs` (`StateMachineBehaviour`) re-enables it on animation state exit
- **`RespawnManager`** also disables `Ataque` during respawn
- **`Unlocking` (chest)** — `consumirLlaves` flag exists but is NOT wired; chest opens when `pirate.getPuntos() >= 3` and awards `puntosBonus`
- **`Moneda` (coin)** — rotates on Z-axis, 0.3s delay before collectible, logs but does NOT award points
- **Save system** — `SaveSystem` uses plain JSON via `JsonUtility` at `Application.persistentDataPath`; no encryption
