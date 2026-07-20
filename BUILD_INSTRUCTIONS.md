# Build & Run Instructions (Prototype)

This file explains how to open and run the prototype locally. It assumes you have Unity installed.

Recommended Unity version
- Unity 2021.3 LTS (recommended) or Unity 2022 LTS. Use the same Editor version when building WebGL and Android to avoid compatibility issues.

Required Unity packages
- Universal Render Pipeline (URP) via Package Manager
- (Optional) Addressables if later used

Opening the project
1. Clone the repo: git clone https://github.com/andrewwolfson12-coder/portal-sonic-doom.git
2. Open Unity Hub and add the repository folder as a project.
3. If Unity complains about packages or ProjectSettings versions, allow it to upgrade packages. Prefer opening with the exact recommended Unity version.

Initial setup (after I commit the full project skeleton)
1. Install URP via Window -> Package Manager -> Universal RP. Install the latest compatible URP for your Unity version.
2. Create a URP Pipeline Asset (Assets -> Create -> Rendering -> Universal Render Pipeline -> Pipeline Asset) and assign it in Edit -> Project Settings -> Graphics and Quality.
3. Import the Assets folder contents (I will provide free assets placed under Assets/FreeAssets) and configure materials to use URP/Lit shader.
4. Open the main scene located at Assets/Scenes/Main.unity and press Play to test the demo.

Desktop controls
- Movement: WASD / Arrow keys
- Aim: Mouse
- Fire: Left mouse button (Fire1)
- Jump: Space
- Place portals: Right mouse button / E (configurable)

Mobile controls (on-screen)
- Left virtual joystick for movement
- Right touch area for aim (drag to aim)
- Tap buttons for Fire and Portal placement

Building for WebGL
1. File -> Build Settings -> WebGL, switch platform
2. Add Scenes: ensure the Main scene is included
3. Build and Run. The output can be deployed to GitHub Pages or itch.io.

Building for Android
1. File -> Build Settings -> Android, switch platform (you will need Android SDK/NDK via Unity Hub)
2. Configure Player Settings (bundle id, orientation)
3. Build an APK and install on device for testing.

Notes
- I will provide an initial WebGL build artifact for easy testing once Phase 1 is complete.
- After the first playable commit, these instructions will be expanded with exact Unity ProjectSettings and pipeline assets.
