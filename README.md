# CombatHelicopter2 :: main branch
![](Images/logo.png)

CombatHelicopter2 decomp. & little RnD

## The preface

On the github on the "channel" of the WPR developer, I accidentally came across such an issue:

https://github.com/8212369/WPR/issues/44

I don't think I have the brains in my WPR fork to provide support for non-standard silverlight games. But in the 21st century, with the help of dotPeek reverse engineering and agent-based development environments like Trae, you can try to port some WP7 games and UWP for Windows10Mobile, Windows 11 or XBox in an hour or two...

## Tech. details

- App type : UWP
- Min. Win. SDK build: 10240 (Hello, Astoria)
- Win. SDK build: 19041
- Scaling through render targeting and letterboxing implemented
- The InputTransform class added to Helicopter.Modela
- InputState (Mouse and touch transformation) updated
- Keyboard controls added to the GameplayScreen
- VS 2022 used as IDE for build/test/debug 

## Screenshot
![](Images/sshot01.png)
![](Images/sshot02.png)

## Status

- Draft. Only "first" AI-reconstruction done (some decomp. errors automatically fixed approx. 1-3 hours of Trae IDE using...)
- Game runs in "Desktop" (Windows OS) successfully, base mouse/kbd support added but gampeplay not tested yet. 

## Input controls (Keyboard mode)
- The gameplay screen has introduced the processing of basic actions:
- W/Up — lifting (pinch = hold, release = stop).
  - Space/LeftCtrl — Fire 1.
  - Shift — Fire 2 (not realized yet... Buy mode unlocking needed!)
  - Enter — start the level from the preparation state.
- See more tech. info in /Docs folder.

## TODO
- Activate additional weapon (type 1, 2, 3)
- Fix "Buy" … =)

## ..
As is. No support. Just for fun!

## References
https://github.com/8212369/WPR/issues/44

[m][e] 2026

![](Images/footer.png)

