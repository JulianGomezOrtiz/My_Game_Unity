# AGENTS.md

Unity 6000.3.7f1 (Unity 6) 3D + URP. Scenes: `Assets/Escenas/Main.unity` (build index 1, entry), `Assets/Escenas/Menu1.unity` (build index 2, pause). `Assets/Scenes/SampleScene.unity` exists but is disabled in build settings. Custom scripts in `Assets/SCRIPTS/`. StarterAssets is an imported third-party asset, not from UPM.

## Setup quirks

- `.csproj`, `.sln`, `.slnx` are gitignored — Unity regenerates them. Do not manually edit.
- `.inputactions` asset exists but is NOT wired. Input reads `Keyboard.current` / `Mouse.current` directly.

## Tags & Layers

- **Tags**: `Player`, `Llave`, `Coin`, `Enemigos`, `CinemachineTarget`
- **Layers**: `pp` (6), `Enemy` (7); rendering layers 0–7.

## Gotchas

- **Class name ≠ filename** — `UnlockingChest.cs` → `Unlocking`, `CoinBehavior.cs` → `Moneda`, `Pause.cs` → `PauseManager`
- **Animator param casing**: `WarrokEnemy` / `NavegationPatrol` use `"IsWalking"` / `"IsDead"` (capital I). If your Animator Controller uses different casing or names, scripts will silently fail to trigger animations.
- **`Ataque.cs` bug**: `atacar()` is private with no internal caller (animation-event-driven). It never disables `_fps.enabled` before the attack. `ReenableController` (Invoke 1s) only re-enables. Controller is NEVER locked during attack. `activarAnimaciones.cs` (at `Assets/activarAnimaciones.cs`, not in SCRIPTS/) is a `StateMachineBehaviour` that also tries to re-enable `ThirdPersonController.enabled` on animation exit — both are partial workarounds. `Health.Die()` also disables the controller on death. Additionally, `activarAnimaciones.cs:21` lacks a null check on the controller — will crash if the animator lives on an object without `ThirdPersonController`.
- **Score**: `PirateBehaviour.SetPuntos()` / `getPuntos()`. `"Llave"` tag auto-adds 1 in `OnTriggerEnter`. 3 keys needed for chest.
- **Chest** (`Unlocking`): `consumirLlaves` bool declared but **never read** — keys are not consumed. Awards `puntosBonus` (default 5) when `getPuntos() >= 3`.
- **Coin** (`Moneda`): rotates on Z, 0.3s delay before collectible. Logs only — **does NOT award points**.
- **Pause**: `PauseManager` loads `Menu1` additively + `Time.timeScale = 0`. `PauseMenuController.Reiniciar()` reloads active scene then unloads `Menu1` — fragile if Menu1 not loaded. `DeathScreenUI` also loads Menu1 additively on death (same fragile pattern).
- **`LedgeDetector`**: calls `Invoke("PickNewDestination", 1f)` every frame while hitting a ledge — can stack invocations.
- **`RespawnManager`**: disables `Ataque` component during respawn, re-enables after teleport. Uses fall threshold at `y < -10`.
- **Unity 6 API**: `Rigidbody.linearVelocity` replaces `.velocity` (see `Bullet.cs:19`).
- **Save system**: plain `JsonUtility` at `Application.persistentDataPath`/savegame.json, no encryption. `SaveData` tracks only `llaves` + `puntos`.
- **`AudioManager`**: singleton via public static `Instance` field; NOT `DontDestroyOnLoad` — resets on scene change.
