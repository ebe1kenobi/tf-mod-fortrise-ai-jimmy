# AIJimmy

An AI for the **LoaderAI** loader. Not machine learning, just a set of rules, but
noticeably nicer to play against than the simple or Python AIs, with fairly
human-like behaviour.

Built for versus. It can play co-op but does a poor job of it.

Known limits: it sometimes gets stuck on a ledge, loops on certain movements, or
freezes at the edge of a block hanging over a gap. One human against two AIs makes
for a balanced game.

WiderSetMod supported.

A mod for **FortRise 5** (>= 5.3.3). The FortRise 4 version (`tf-mod-fortrise-ai-jimmy`) is no longer maintained: fixes and new features only land in this repository.

## Installation

1. Install FortRise 5 and start the game through `FortRise.exe`.
2. Install the mods this one depends on first: **LoaderAI**.
3. Copy `release/aijimmy` (or the shipped folder) into `<TowerFall>/FortRise/Mods/`.

Settings are under **Options > Mods > AIJimmy**.
Data and log files live in `<TowerFall>/FortRise/Saves/AIJimmy/` and `<TowerFall>/FortRise/Logs/`.

## Usage

Install **LoaderAI** first, then this mod. The "Jimmy" agent then shows up in the
list of available AIs on the archer select screen.

### AI keyboard layout

An AI is picked on the archer select screen: up and down arrows appear around the
player name as soon as at least one agent is available.

| Action | P1 | P2 | P3 | P4 | P5 | P6 | P7 | P8 |
|--------|----|----|----|----|----|----|----|----|
| Down | A | Z | D | F | G | H | J | K |
| Up | Q | S | E | R | T | Y | U | I |
| Left | O | P | W | C | B | F9 | F11 | Page Up |
| Right | L | M | X | V | N | F10 | F12 | Page Down |
| Jump / **pick the AI** | NumPad1 | NumPad2 | NumPad3 | NumPad4 | NumPad5 | NumPad6 | NumPad7 | NumPad8 |
| Shoot / **drop the AI** | F1 | F2 | F3 | F4 | F5 | F6 | F7 | F8 |
| Dodge | F13 | F14 | F15 | F16 | F17 | F18 | F19 | F20 |

In short: **NumPad 1-8** assigns an agent to the matching player, **F1-F8** removes
it.

## What Jimmy knows about the mode it is in

The AI used to play the same game whatever the mode: chase the nearest player, shoot
at it. That is deathmatch, and it is wrong everywhere else.

### Play tag

When the **PlayTag** mod is installed and a tag match is running, Jimmy targets the
player who is **it** and **runs from them** instead of closing in. When Jimmy is the
one carrying the tag, nothing changes - it chases, which is exactly its usual
behaviour.

Fleeing takes priority over fetching arrows: being touched costs the round, an empty
quiver costs nothing. The goal it walks to is the cell furthest from the threat within
a radius of twelve, weighted by the distance it would have to travel - a cell twice as
far from the chaser but across the map is not a shelter, it is a journey.

### Soccer

When the **Soccer** mod is running, Jimmy goes for the ball, then for the opposite
goal once it carries it. Running, jumping and dodging are the same work as always -
only the destination changes.

### Wide levels, stitched levels

The pathfinding grid used to be **guessed** between two known formats: 32x24, or 42x24
when WiderSet announced wide mode. That covered the two cases it was written for and
no other - a mode that stitches several levels together (Scroll) makes one 64x48 or
larger, and the AI only saw its top-left corner.

It is now **read** off `level.Tiles.Grid`: the level's own collision grid is made of
ten-pixel cells, exactly like the pathfinding one, so there is nothing to convert and
nothing to update the day another format appears. The array is also reallocated when
the format changes, and not only the first time - going from a normal round to a wide
one used to keep a 32-wide grid while the code walked it over 42.

That does not make Jimmy a good Scroll player: it still does not know that the screen
scrolls and that it has to keep up. But it is no longer blind.

### Optional, both ways

PlayTag and Soccer are optional dependencies resolved **on first use** rather than in
the constructor. A mod loaded before us is reachable from there - WiderSet loads 4th,
Jimmy 28th - but that depends on load order, which changes the moment a mod is added
or removed. Without those mods everything answers "no" and Jimmy behaves as before.

## Build / deployment

| Script | Purpose |
|--------|---------|
| `script/release.bat` | build, then assemble into `release/` |
| `script/deploy.bat` | copy `release/` into the TowerFall `Mods` folder |
| `script/release_deploy.bat` | both, one after the other |

Paths (game folder, module name) are set in `script/config.bat`.
