# Changelog

All notable changes to this project will be documented in this file.

## 2026-07-20 — Playable vertical slice: "GRAVITON"
- Shipped a complete, self-contained WebGL game in `index.html` (Three.js, vendored under `vendor/`) — no build step, no CDN, runs offline and on GitHub Pages.
- Player: first-person controller with swept-AABB collision, Quake-style acceleration, sprint, double-jump.
- Sonic layer: boost pads that fling the player across lava gaps, a real km/h speed HUD gauge, grind rails.
- Portal layer: aim-to-place blue/orange portal pair with momentum-preserving teleport (velocity rotated to the exit normal).
- Doom layer: GRAVITON-TEAR projectile weapon (regenerating ammo), demon enemies that chase and fire, three escalating waves, extraction-ring win condition, lava death.
- Neon HUD matching the concept art: HEALTH/SHIELD bars, speed gauge, weapon readout, portal status, damage vignette, wave toasts.
- Procedural Web-Audio SFX; animated lava; particle death bursts.
- Touch controls (virtual stick + on-screen buttons) for mobile.
- Verified end-to-end in headless Chromium (movement, combat, portals, all 3 waves → win) with zero runtime errors.
- Added `.nojekyll` and gameplay screenshot (`Assets/screenshot.png`); refreshed README with play/controls/mechanics.

### Retained
- Original Unity/URP reference scripts under `Assets/Scripts/` (PlayerController, PortalTeleporter, Teleportable, SimpleEnemy, Weapon, Health, Booster) and setup notes (`UNITY_SETUP.md`, `BUILD_INSTRUCTIONS.md`).
