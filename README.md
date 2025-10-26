# AR Ghost Catching Game

### [HackNotts '25 Submission](https://devpost.com/software/ghost-catch-idck9r)

https://github.com/user-attachments/assets/9f103d6f-acd0-4abf-9d0e-3c4ebcfe3491

## Inspiration

Our core inspiration sources were the Wii game **Spooky Search** and the Steel Crate Games’ co-op puzzle game **Keep Talking and Nobody Explodes**. We wanted to incorporate the ghost catching element of the former, and the element of verbal communication of privileged knowledge in the latter, and use mobile AR technology.

## What We Set Out To Do

Our initial idea was to have a multiplayer ghost finding game, where there is one “Seer” and one “Catcher”. 

* The Seer *can* see the ghosts with their phone, and the Catcher *can’t*
* The Seer tells the Catcher where to aim

## How It Went

Our first issue was getting Unity to work on everyone’s laptop and phone. We couldn’t get Unity setup on Isaac’s Ubuntu box due to a dependency error, so he made the ghost model in Blender and drew the reticle.

Using [Unity’s mobile AR development guide](​​https://learn.unity.com/pathway/mobile-ar-development), I got a solo version of the game working on iOS, where one player can both see *and* catch the ghosts, using those assets.

Then, we ran into real trouble with the *Cloud Anchors* that synchronize the AR environment between phones (so that both devices agree on where in space the ghosts are). 

At some point yesterday, after reaching an impasse vis-à-vis co-op implementation, the project scope became single-player.

## What We Achieved

Despite the twists and turns, we shipped a playable AR ghost-catching prototype for iOS. A player can scan their space, reveal a ghost, and fling a line trace at it. The collision detection and randomization of ghost location works. We learned a ton about AR mobile development, and the pros and cons of different game engines and frameworks that we tried along the way.

## What's Next?
* [ ] Adaptive original music
* [ ] Multiplayer support
* [ ] Haptic feedback
