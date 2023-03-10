# **The Zazacrifice of Shaggy**

## _Game Design Document_

---

##### **Copyright notice / author information / boring legal stuff nobody likes**

##
## _Index_

---

1. [Index](#index)
2. [Game Design](#game-design)
    1. [Summary](#summary)
    2. [Gameplay](#gameplay)
    3. [Mindset](#mindset)
3. [Technical](#technical)
    1. [Screens](#screens)
    2. [Controls](#controls)
    3. [Mechanics](#mechanics)
4. [Level Design](#level-design)
    1. [Themes](#themes)
        1. Ambience
        2. Objects
            1. Ambient
            2. Interactive
        3. Challenges
    2. [Game Flow](#game-flow)
5. [Development](#development)
    1. [Abstract Classes](#abstract-classes--components)
    2. [Derived Classes](#derived-classes--component-compositions)
6. [Graphics](#graphics)
    1. [Style Attributes](#style-attributes)
    2. [Graphics Needed](#graphics-needed)
7. [Sounds/Music](#soundsmusic)
    1. [Style Attributes](#style-attributes-1)
    2. [Sounds Needed](#sounds-needed)
    3. [Music Needed](#music-needed)
8. [Schedule](#schedule)

## _Game Design_

---

### **Summary**

Dive into an exciting adventure with The Zazacrifice of Shaggy, a turn-based RPG follows the protagonist, Shaggy, as he battles his way through a mysterious tower. Encounter fierce enemies, navigate tricky puzzles and uncover hidden secrets as you journey through this challenging and immersive RPG. 

### **Gameplay**

Shaggy, as he attempts to rescue his furry companion, Scooby, who was abducted by a mysterious gang. A homeless man who witnessed the abduction gives Shaggy information about the gang’s whereabouts in exchange for the recovery of some heavily sedating potions that the gang stole from him.

Arm yourself with a selection of weapons to prepare yourself for the battles ahead. The homeless man offers Shaggy four types of weapons; these will determine the set of attacks and statistics available throughout the game. Shaggy must be prepared for the enemies he will encounter on his journey. He also begins with a selection of potions with basic uses, including Rage, Health Boost, Endurance Boost, and Special Powers. As he progresses, he may find other potions, spells and abilities to help him along the way.

The gameplay of The Zazacrifice of Shaggy is designed to be an adventurous game with PvP battles against NPCs and level bosses. Solve tricky puzzles and search for hidden secrets as you battle your way through intense boss battles and difficult dungeons. Unlock new abilities, customize your character with a variety of weapons and armor as you make your way to the top of the tower and uncover the truth behind the Zazacrifice. An unforgettable journey awaits in The Zazacrifice of Shaggy

### **Mindset**

The ideal mindset we strive for in this game is the following: We want the user to feel a sense of wonder at the start of the game as he is being told the story of the game. When the user has chosen his battle abilities and has a bit more of context as well as power, we want the player to feel powerful and excited for this new adventure. To provoke the player even more we plan on adding NPC’s whose purpose is to slow the player down and have his statistics decrease so the salvation of his furry companion doesn't come that easy. This will provoke the player to prepare and strategically play his way into the castle. 


## _Technical_

---
### **Screens**

1. Title Screen
    1. Menu
3. Game
    1. Inventory
    2. Map
    3. Gandalf´s store 
4. End Credits

_(example)_

### **Controls**

How will the player interact with the game? Will they be able to choose the controls? What kind of in-game events are they going to be able to trigger, and how? (e.g. pressing buttons, opening doors, etc.)
we will have 2 different gaming options:

Option A (gamer type):
controls:
"W" = forward
"D" = right.
"S" = backwards
"A" = Left.
"Q" = Interact (open doors, confirm purchases, drink potions)
"ESC" = Menu.

Option B (Modern type):
controls:
"Up arrow" = forward
"Right arrow" = right.
"Down arrow" = backwards
"Left Arrow" = Left.
"Q" = Interact (open doors, confirm purchases, drink potions)
"ESC" = Menu.
### **Mechanics**

General
-The player can choose between three classes (light, medium, heavy) that will affect their statistics in combat
-The player will be able to obtain or buy equipment that alters his statistics
-The player will also get consumables to regenerate HP and MP or key items by completing battles or side-quests

Stats
-HP (Life Points)
-MP (Magic points to use special attacks)
-STR (How strong the attacks will be)
-DEF (How much will resist enemy attacks)
-LUC (What is the probability of making a critical attack)
-SPE (The character with the most SPE points will attack first)
-CHR (It will allow you to get discounts in the store and better rewards)

Combat:
-The combat will be in turns and will end when the character or the opponent no longer has HP

-During combat you can choose to use normal attacks, magic attacks (consumes MP), use a consumable or run away from the battle

-If the match is won if it has a slight increase in stats and the player is rewarded with money or items

-There is a chance to receive or deal a critical attack that does 200% more damage

-If enemies notice main character they´ll run forward after him, in case of colappsing (with main character) the battle will begin with enemie´s having the first attack

-If main charcater hits enemie´s back, the battle will begin with main character having the first attack

## _Level Design_

---

_(Note : These sections can safely be skipped if they&#39;re not relevant, or you&#39;d rather go about it another way. For most games, at least one of them should be useful. But I&#39;ll understand if you don&#39;t want to use them. It&#39;ll only hurt my feelings a little bit.)_

### **Themes**

1. Forest
    1. Mood
        1. Dark, calm, foreboding
  2. Objects
        1. _Ambient_
            1. Fireflies
            2. Beams of moonlight
            3. Tall grass
        2. _Interactive_
            1. Wolves
            2. Goblins
            3. Rocks
2. Castle
    1. Mood
        1. Dangerous, tense, active
    2. Objects
        1. _Ambient_
            1. Rodents
            2. Torches
            3. Suits of armor
        2. _Interactive_
            1. Guards
            2. Giant rats
            3. Chests

_(example)_

### **Game Flow**

1. Player starts in forest
2. Pond to the left, must move right
3. To the right is a hill, player jumps to traverse it (&quot;jump&quot; taught)
4. Player encounters castle - door&#39;s shut and locked
5. There&#39;s a window within jump height, and a rock on the ground
6. Player picks up rock and throws at glass (&quot;throw&quot; taught)
7. … etc.

_(example)_

## _Development_

---

### **Abstract Classes / Components**

1. BasePhysics
    1. BasePlayer
    2. BaseEnemy
    3. BaseObject
    4. BaseCombactObject
    5. BaseNPC


2. BaseObstacle
3. Visual
3. BaseInteractable


_(example)_

### **Derived Classes / Component Compositions**

1. BasePlayer
    1. PlayerMain
        1. Light player 
        2. Middle player 
        3. Heavy player 

2. BaseEnemy
    1. EnemyZabush (33% of chances to drop Berries)
    2. EnemyWizzards 
    3. EnemyZaclon
    4. EnemyGiantZanaZana (semi-boss, drop key for final boss)
    5. EnemyLordZA (final boss)

3. BaseObject
    1. ObjectBerrie (heals 3 hp each)
    2. ObjectChest (spits random item)
    3. ObjectGoldCoin (cha-ching!)
    4. ObjectKey (for accesing the final boss (inside the castle))
    5. ObjectRock (pickable, throwable)
    6. ObjectPotion 
        1. HealPotion (may acces to it in Gandalf´s Zastore)
        2. DefensePotion (may acces to it in Gandalf´s Zastore)
        3. AttackPotion (may acces to it in Gandalf´s Zastore)

2. BaseObstacle
    1. ObstacleHouseWindow
    2. ObstacleWall
    3. ObstacleHouseCouch 
    4. ObstacleHouseBed
    5. ObstacleHouseTable 
    6. ObstacleHouseCupboard
    7. ObstacleHouseMailbox
    8. ObstacleBush
    9. OsbactleTree
    10. ObstacleForestRock (not the same rock as ObjectRock nor ObstacleCastleRock)
    10. ObstacleGate (watches to see if certain buttons are pressed)
    11. ObstacleCastleRock (ot the same rock as ObjectRock nor ObstacleForestRock)
    12. ObstacleCastlePillar 

3. BaseNPC
    1. NPCVillager (both average NPC women/men)
    2. NPChobbit 
    3. NPCSethRogan
    

4. Visual
    1. VisualHouseRug
    1. VisualGrass
    2. VisualBird
    4. VisualWind (**NOTA PARA QEUIPO: les gustaria que se vieran como ondas de viento??, en caso de que no quieran borrar el punto 4**)
    5. VisualForestGround
    6. VisualCastleGround 
    


5. BaseInteractable
    1. InteractableButton

_(example)_

## _Graphics_

---

### **Style Attributes**

What kinds of colors will you be using? Do you have a limited palette to work with? A post-processed HSV map/image? Consistency is key for immersion.

What kind of graphic style are you going for? Cartoony? Pixel-y? Cute? How, specifically? Solid, thick outlines with flat hues? Non-black outlines with limited tints/shades? Emphasize smooth curvatures over sharp angles? Describe a set of general rules depicting your style here.

Well-designed feedback, both good (e.g. leveling up) and bad (e.g. being hit), are great for teaching the player how to play through trial and error, instead of scripting a lengthy tutorial. What kind of visual feedback are you going to use to let the player know they&#39;re interacting with something? That they \*can\* interact with something?

### **Graphics Needed**

1. Characters
    1. Human-like
        1. Goblin (idle, walking, throwing)
        2. Guard (idle, walking, stabbing)
        3. Prisoner (walking, running)
    2. Other
        1. Wolf (idle, walking, running)
        2. Giant Rat (idle, scurrying)
2. Blocks
    1. Dirt
    2. Dirt/Grass
    3. Stone Block
    4. Stone Bricks
    5. Tiled Floor
    6. Weathered Stone Block
    7. Weathered Stone Bricks
3. Ambient
    1. Tall Grass
    2. Rodent (idle, scurrying)
    3. Torch
    4. Armored Suit
    5. Chains (matching Weathered Stone Bricks)
    6. Blood stains (matching Weathered Stone Bricks)
4. Other
    1. Chest
    2. Door (matching Stone Bricks)
    3. Gate
    4. Button (matching Weathered Stone Bricks)

_(example)_


## _Sounds/Music_

---

### **Style Attributes**

Again, consistency is key. Define that consistency here. What kind of instruments do you want to use in your music? Any particular tempo, key? Influences, genre? Mood?

Stylistically, what kind of sound effects are you looking for? Do you want to exaggerate actions with lengthy, cartoony sounds (e.g. mario&#39;s jump), or use just enough to let the player know something happened (e.g. mega man&#39;s landing)? Going for realism? You can use the music style as a bit of a reference too.

 Remember, auditory feedback should stand out from the music and other sound effects so the player hears it well. Volume, panning, and frequency/pitch are all important aspects to consider in both music _and_ sounds - so plan accordingly!

### **Sounds Needed**

1. Effects
    1. Soft Footsteps (dirt floor)
    2. Sharper Footsteps (stone floor)
    3. Soft Landing (low vertical velocity)
    4. Hard Landing (high vertical velocity)
    5. Glass Breaking
    6. Chest Opening
    7. Door Opening
2. Feedback
    1. Relieved &quot;Ahhhh!&quot; (health)
    2. Shocked &quot;Ooomph!&quot; (attacked)
    3. Happy chime (extra life)
    4. Sad chime (died)

_(example)_

### **Music Needed**

1. Slow-paced, nerve-racking &quot;forest&quot; track
2. Exciting &quot;castle&quot; track
3. Creepy, slow &quot;dungeon&quot; track
4. Happy ending credits track
5. Rick Astley&#39;s hit #1 single &quot;Never Gonna Give You Up&quot;

_(example)_


## _Schedule_

---

_(define the main activities and the expected dates when they should be finished. This is only a reference, and can change as the project is developed)_

1. develop base classes
    1. base entity
        1. base player
        2. base enemy
        3. base block
  2. base app state
        1. game world
        2. menu world
2. develop player and basic block classes
    1. physics / collisions
3. find some smooth controls/physics
4. develop other derived classes
    1. blocks
        1. moving
        2. falling
        3. breaking
        4. cloud
    2. enemies
        1. soldier
        2. rat
        3. etc.
5. design levels
    1. introduce motion/jumping
    2. introduce throwing
    3. mind the pacing, let the player play between lessons
6. design sounds
7. design music

_(example)_
