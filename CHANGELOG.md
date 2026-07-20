# Changelog

All notable changes to this project will be documented in this file.

## 2026-07-20 — Brightness pass
- The scene read too dark; raised exposure (1.35→1.85), hemisphere/ambient/key/fill light, and added a second warm fill so the whole arena stays legible.
- Thinned the fog and lightened the background + fog colour so distance no longer fades to black.
- Brightened the IBL environment map and boosted the neon point lights (added a third).
- Softened the vignette so screen edges are never crushed, and raised the bloom threshold so only neon/lava blooms at the higher exposure.

## 2026-07-20 — Depth pass: viewmodel, props, beveled geometry, AO
- Added a first-person viewmodel: armored gauntlets + the GRAVITON-TEAR emitter/blade and a glowing left "portal hand", with look-sway, walk-bob and fire recoil.
- Beveled every structural block (RoundedBoxGeometry) so edges catch light instead of reading as hard CG cubes.
- Dressed the sector with props/greebles: pipes + valves, oil/toxic barrels (collidable cover), hazard-stripe panels, support struts, hanging cables and emissive ceiling lamps that also feed reflections.
- Added contact-shadow ambient occlusion under demons/barrels/props for grounding (robust fallback after GTAO/SSAO rendered black on the swiftshader/WebGL path).
- Verified: full three-wave win path intact, zero runtime errors.

## 2026-07-20 — Realism pass (de-"8-bit")
- Added image-based lighting: a procedural PMREM environment map so PBR metals reflect the room/lava instead of looking flat.
- Replaced blocky cell-noise with smooth multi-octave fractal (fbm) noise and raised all textures to 1K, adding fine micro-relief to the normal maps and dedicated roughness maps — kills the pixelated/retro texture look.
- Retuned materials for real reflectivity (higher metalness + envMapIntensity), brighter exposure, a cool front fill light and softer shadows so surfaces and demons read naturally.
- Higher-resolution demon skin with normal-mapped warty relief and reflective sheen.

## 2026-07-20 — Visual overhaul: realistic, scary, high-quality
- Added a cinematic post-processing pipeline (vendored under `vendor/jsm/`): EffectComposer + UnrealBloom + a custom atmosphere pass (film grain, vignette, chromatic aberration, low-HP screen warp) + FXAA + ACES tone-mapping.
- Replaced flat-colour surfaces with procedural PBR materials: canvas-generated worn metal (rivets/rust/seams), pitted concrete and molten magma, each with a derived normal map for real surface relief.
- Redesigned the enemies as genuinely menacing demons — hunched charred bodies with glowing lava-vein skin, horned skulls, fangs, clawed arms and digitigrade legs, plus a stalking gait, head-tracking and an attack lunge with a guttural growl.
- Atmosphere pass: thicker fog, hundreds of rising embers, flickering fire/neon lights, flowing animated lava, and a continuous sub-bass dread drone.
- Re-verified end-to-end in headless Chromium (post-processing shaders compile clean, full 3-wave win path, zero runtime errors).

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
