# AR Ghost Catching Game

https://github.com/user-attachments/assets/9f103d6f-acd0-4abf-9d0e-3c4ebcfe3491

## Inspiration

Our core inspiration sources were the Wii game **Spooky Search** and the Steel Crate Games’ co-op puzzle game **Keep Talking and Nobody Explodes**. We wanted to incorporate the ghost catching element of the former, and the element of verbal communication of privileged knowledge in the latter, and use mobile AR technology.

## What We Set Out To Do

Our initial idea was to have a multiplayer ghost finding game, where there is one “Seer” and one “Catcher”. The Seer can see the ghosts with their phone, and the Catcher can’t. The Seer has to tell the Catcher where to aim. 

## How It Went

Our first issue was getting Unity to work on everyone’s laptop and phone. We couldn’t get Unity setup on Isaac’s Ubuntu box due to a dependency error, so he made the ghost model in Blender and drew the reticle.

Using [Unity’s mobile AR development guide](​​https://learn.unity.com/pathway/mobile-ar-development), I got a solo version of the game working on iOS, where one player can both see *and* catch the ghosts, using those assets.

Then, we ran into real trouble with the *Cloud Anchors* that synchronize the AR environment between phones (so that both devices agree on where in space the ghosts are). 

At some point yesterday, after reaching an impasse vis-à-vis co-op implementation, the project scope became single-player.

