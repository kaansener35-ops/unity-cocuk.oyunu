# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Unity 6 (6000.1.12f1) third-person 3D collectible platformer. The player controls a robot character, collects mushroom-themed items while avoiding an enemy robot that chases using NavMesh. All user-facing text is in Turkish.

- **Rendering**: Universal Render Pipeline (URP) 17.1.0
- **Version Control**: Plastic SCM (`ignore.conf` controls ignores)

## Development Workflow

This is a Unity project — all development happens inside the Unity Editor. There are no CLI build/test commands. To work on this project:

1. Open in Unity Hub with Unity 6000.1.12f1
2. Main scene: `Assets/Tutorialdan Gelen Her Sey/Scenes/GetStarted_Scene.unity`
3. Play via the Unity Editor Play button

## Architecture

### Game Loop

1. **Collect**: Player picks up all collectibles tagged `"Collectible"`, `"PickUp"`, or `"Pickup"`
2. **Win**: `UpdateCollectibleCount.cs` detects count = 0 → shows "TEBRİKLER" message → `Time.timeScale = 0`
3. **Lose**: `EnemyCollision.cs` detects player contact → shows loss message → scene restarts after 1.5s delay via `SceneManager.LoadScene`
4. **Fall recovery**: `RespawnPlayer.cs` monitors Y < -5 → teleports player back to spawn, resets Cinemachine orientation

The static `UpdateCollectibleCount.gameEnded` flag is shared across scripts to prevent loss triggering after win.

### Key Scripts

**Custom scripts** live in two locations:
- `Assets/Tutorialdan Gelen Her Sey/SourceFiles/Scripts/` — core gameplay
- `Assets/Scripts/` — enemy scripts (`EnemyMovement.cs`, `EnemyCollusion.cs`)

| Script | Purpose |
|--------|---------|
| `ThirdPersonController.cs` | CharacterController-based movement, Cinemachine camera, animation, audio |
| `StarterAssetsInputs.cs` | Input System wrapper (move, look, jump, sprint); feeds mobile joystick too |
| `UpdateCollectibleCount.cs` | Counts remaining collectibles each frame, drives win condition |
| `Pickup.cs` | Collectible behaviour: rotation/bob animation, particle + audio on collect |
| `EnemyMovement.cs` | NavMeshAgent follows player transform each frame |
| `EnemyCollision.cs` | Collision → lose condition (checks `gameEnded` first) |
| `RespawnPlayer.cs` | Fall detection + respawn; resets velocity via reflection on CharacterController |
| `MobileJoystick.cs` | Touch joystick UI; writes directly into `StarterAssetsInputs` |
| `MotionAudioController.cs` | Fades audio in/out based on whether object is moving |

### Core Packages

- `com.unity.inputsystem` 1.14.2 — new Input System (not legacy)
- `com.unity.cinemachine` 3.1.2 — camera management
- `com.unity.ai.navigation` 2.0.9 — NavMesh for enemy AI
- `com.unity.render-pipelines.universal` 17.1.0 — URP

### Scene Tags

Defined tags: `CinemachineTarget`, `Hidden_Area_1/2/3`, `Cube_Spawn_Point_1/2`, `Enemy`, `Collectible`

Pickup detection uses tag comparison (`"Collectible"`, `"PickUp"`, `"Pickup"`) — add new collectibles with one of these tags.

## Asset Pack Layout

Third-party assets are kept separate from custom code:
- `Assets/Tutorialdan Gelen Her Sey/` — tutorial-sourced assets + main game scripts/scenes/prefabs
- `Assets/LowPoly Nature Pack/` — environment art
- `Assets/SimpleLowPolyNature/` — simpler nature variant (mobile-optimised shaders)
- `Assets/Puppet Kid/` — character model
- `Assets/Food Pack-Demo/` — food prop assets
- `Assets/Scripts/` — custom enemy scripts only

## Roadmap

Current focus: Level transition system
- Level 1 is complete (collectibles + win/lose condition)
- Next: Convert win condition into a level transition instead of game-end screen
- The "İyi ki doğdun" end screen will be replaced or repurposed for level select/next level flow
- Future levels will each have their own scene

## Code Standards

- Variable names and comments: English only
- Inspector-visible fields: `[SerializeField] private` — never `public` unless required by Unity internals
- Every method must have a brief summary comment above it (XML `/// <summary>` style)
- No magic numbers — use named constants or `[SerializeField]` fields
- Follow standard C# conventions (PascalCase for methods/classes, camelCase for variables)

## Asset Hygiene

- `Assets/Tutorialdan Gelen Her Sey/` — may be modified, unused assets may be deleted if they cause performance issues
- Do not touch third-party asset packs (`LowPoly Nature Pack`, `SimpleLowPolyNature`, `Puppet Kid`, `Food Pack-Demo`)
- Custom scripts only in `Assets/Scripts/` or `Assets/Tutorialdan Gelen Her Sey/SourceFiles/Scripts/`

## Known Issues

None at this time. Update this section as issues are discovered.