# AGENTS.md

Unity 6000.3.7f1 (Unity 6) 3D + URP. Scenes: `Assets/Escenas/Main.unity` (index 1, entry), `Assets/Escenas/Menu1.unity` (index 2, pause). Custom scripts in `Assets/SCRIPTS/`. StarterAssets is an imported third-party asset, not from UPM.

## Setup quirks

- `.csproj`, `.sln`, `.slnx` are gitignored — Unity regenerates them. Do not manually edit.
- `.inputactions` assets exist but are NOT wired. Input reads `Keyboard.current` / `Mouse.current` directly.

## Tags & Layers

- **Tags**: `Player`, `Llave`, `Coin`, `Enemigos`, `CinemachineTarget`
- **Layers**: `pp` (6), `Enemy` (7); rendering layers 0–7.

## Gotchas

- **Class name ≠ filename** — `UnlockingChest.cs` → `Unlocking`, `CoinBehavior.cs` → `Moneda`, `Pause.cs` → `PauseManager`
- **`Ataque.cs` bug**: `atacar()` never disables `_fps.enabled` before the attack. `ReenableController` (Invoke 1s) only re-enables. Controller is NEVER locked during attack. A `StateMachineBehaviour` in `activarAnimaciones.cs` also tries to re-enable `ThirdPersonController.enabled` on animation exit — both are partial workarounds.
- **Score**: `PirateBehaviour.SetPuntos()` / `getPuntos()`. `"Llave"` tag auto-adds 1 in `OnTriggerEnter`. 3 keys needed for chest.
- **Chest** (`Unlocking`): `consumirLlaves` bool declared but **never read** — keys are not consumed. Awards `puntosBonus` (default 5) when `getPuntos() >= 3`.
- **Coin** (`Moneda`): rotates on Z, 0.3s delay before collectible. Logs only — **does NOT award points**.
- **Pause**: `PauseManager` loads `Menu1` additively + `Time.timeScale = 0`. `PauseMenuController.Reiniciar()` reloads active scene then unloads `Menu1` — fragile if Menu1 not loaded.
- **`LedgeDetector`**: calls `Invoke("PickNewDestination", 1f)` every frame while hitting a ledge — can stack invocations.
- **Unity 6 API**: `Rigidbody.linearVelocity` replaces `.velocity` (see `Bullet.cs:11`).
- **Save system**: plain `JsonUtility` at `Application.persistentDataPath`/savegame.json, no encryption. `SaveData` tracks only `llaves` + `puntos`.
- **`AudioManager`**: singleton via public static `Instance` field; NOT `DontDestroyOnLoad` — resets on scene change.
