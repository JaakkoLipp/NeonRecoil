# Futuristic 2D Platformer

A neon-soaked, cyberpunk-inspired 2D platformer built in Unity. Guide your character through multiple levels of rooftop arenas and undercity corridors, blasting patrolling drones and outrunning malfunctioning security bots. Fight with precision-aimed projectiles, manage health and time, and compete for the highest score ever recorded.

NOTE THIS REPO CONTAINS ONLY THE ASSETS FOLDER, i couldnt upload the full repo at this time. contact if needed.

---

## Key Features

* **Dynamic Player Controller**
  Smooth left/right movement, variable jump height, and responsive air control.

* **Enemy AI & Line-of-Sight**
  Patrolling enemies flip to face the player, detect intruders with raycasted vision, and fire accurately only when unblocked by platforms.

* **Precision Shooting**
  Bullets are instantiated toward the player’s position (or in the player’s facing direction) and rotate to match their trajectory.

* **Multi-Level Progression**
  Levels load additively on top of a persistent UI scene. Level Two unlocks automatically when all enemies in Level One are defeated.

* **Persistent UI & Score System**
  A `ScoreManager` singleton tracks score (100 × remaining seconds per kill), timer (30 sec countdown), and health—surviving scene loads until player death.

* **Main Menu & High Score**
  Start, Options, and Quit buttons with pixel-art styling. The main menu displays the all-time high score saved via `PlayerPrefs`.

* **Audio & Visual Polish**
  Looping background music, on-demand gunfire SFX, 9-slice pixel UI, hover/click button animations, and a widescreen 16:9 pixel-art city-night skyline backdrop.


---

## Gameplay & Controls

| Action          | Input             |
| --------------- | ----------------- |
| Move Left/Right | A/D or ← / →      |
| Jump            | Space             |
| Shoot           | Left Mouse Button |
| Pause           | Escape            |

Defeat all enemies in each level before the timer hits zero. Collect extra time by reaching hidden power-ups, but beware: platforms and walls can block enemy fire!

---

## Project Structure

```
Assets/
├─ Scripts/
│  ├─ PlayerMovement.cs
│  ├─ PlayerHealth.cs
│  ├─ Enemy.cs
│  ├─ Bullet.cs / EnemyBullet.cs
│  ├─ ScoreManager.cs
│  ├─ LevelLoader.cs
│  ├─ MainMenuManager.cs
│  └─ EndGame.cs
├─ Scenes/
│  ├─ MainMenu.unity
│  ├─ PersistentUI.unity
│  ├─ LevelOne.unity
│  ├─ LevelTwo.unity
│  └─ GameOver.unity
├─ Art/
│  ├─ PixelBackgrounds/
│  └─ UI_Sprites/
├─ Audio/
│  ├─ Music/
│  └─ SFX/
└─ TMPro/ (TextMeshPro assets)
```

---

## Credits

* **Pixel Art Background**: Custom-generated and edited textures
* **Audio**:

  * Background Music (CC0) from OpenGameArt.org
  * Gunfire SFX (CC0) from OpenGameArt.org
* **UI Framework**: Unity UI & TextMeshPro

Licensed under the MIT License. Feel free to remix, extend, and deploy!
