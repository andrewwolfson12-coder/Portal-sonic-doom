# Unity Project Setup Notes (URP)

These are the exact settings and steps I'll apply when creating the Unity URP project skeleton and the Phase 1 demo.

Unity Editor
- Version: Unity 2021.3 LTS (preferred) or 2022 LTS. I will use 2021.3 LTS for compatibility.

Render Pipeline
- Use Universal Render Pipeline (URP) for realistic PBR visuals and WebGL/mobile compatibility.
- Create a URP Asset (Assets/Settings/URPAsset.asset) and assign in Project Settings -> Graphics and Quality.
- Configure URP: set HDR on, enable MSAA (2x or 4x depending on target), and configure shadow resolution to "Medium" for WebGL.

Physics & Time
- Fixed Timestep: 0.01 (100 Hz) for more stable high-speed physics; may change to 0.02 for performance if needed.
- Default solver iterations: 6 (increased to improve stability at high speeds).

Player & Layers
- Add Layer "Player" and Layer "Portal" and Layer "IgnoreRaycast" as needed.
- Add Tag "Player" for the player object so enemy AI can find it.

Scripting & Input
- Input System: I will use the legacy Input API to keep the prototype simple and compatible.
- Controls mapping: Horizontal/Vertical axes, Fire1, Jump.
- Mobile input: simple virtual joystick prefab (free asset) will be used for the demo.

Scene & Camera
- Main scene: Assets/Scenes/Main.unity
- Camera: use a single main camera with Cinemachine optional; for portal camera rendering, a secondary camera will render into a RenderTexture.

Portals
- Portals will be built as planar meshes with a trigger collider in front for teleport detection.
- Camera-through-portal will use a secondary camera mirrored across portal transforms rendering to a RenderTexture used by the portal surface material.
- Teleportation will transform velocity using portal's transform delta and apply a short teleport cooldown.

Project organization
- Assets/
  - Scripts/ (core gameplay scripts)
  - Scenes/ (Main.unity and test scenes)
  - Prefabs/ (Player, Enemy, Portal, Booster)
  - Materials/ (URP materials and portal shader)
  - Art/FreeAssets/ (free PBR assets and HDRIs)
  - Audio/ (SFX from freesound.org)

Asset pipeline & attribution
- I will only use free assets (AmbientCG, PolyHaven, Unity free assets, freesound.org). Full attribution and license links will be recorded in ASSETS_LICENSES.md.

Optimization notes
- Use baked lighting where possible.
- Use LODs and mesh colliders only when necessary.
- Compress textures for WebGL and Android targets.


