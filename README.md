<h1 align = "center">Beverage Belt Patch History</h1>

<p align = "center">Jacob Sauer</p>

<p align = "center">Created 17 May 2021</p>

<p align = "center">Last updated 1 June 2021</p>

___

## Version 0.1.0 (May 17th)

General Changes:
- Precision Mode and Frenzy Mode were added to the build.
  - In both modes, beverage containers with randomly selected properties spawn atop a conveyor belt, and are transported to the player. 
  - There are four different beverage types:
    - *Milk cartons*, which contain no customization.
    - *Beer bottles*, which contain no customization.
    - *Cans*, which contain 18 customizations based on pop tab presence and label (nine different designs).
    - *Plastic bottles*, which contain 16 customizations based on bottle cap presence, plastic colour (clear plastic, which is fully transparent, or coloured plastic, which is further separated into blue, green, and red), and bottle cap colour (clear bottles can possess black, blue, green, red, or white caps, while a coloured bottle's cap must match the colour of its plastic).
  - The player's objective is to sort these beverages into their respective recycling bins, and ensure that two quality control measures are upheld: bottle caps must be removed from plastic bottles, and pop tabs must be removed from cans. There is a 50% chance that a plastic bottle will spawn with a bottle cap, and a 50% chance that a can will spawn with a pop tab.
  - There are three types of errors that the player can commit: sorting errors represent a beverage being placed in an incorrect bin, handling errors occur when a quality control measure is unfulfilled, and lost items encompass beverages that are not removed from the conveyor belt before being destroyed.
  - The player's sorting accuracy is dynamically calculated and outputted as a percentage value. As well, errors of each type are tallied separated and outputted in the same window.
  - In Precision Mode, a new beverage will spawn every () seconds. In Frenzy Mode, a new beverage will spawn every 1.2 seconds, and the conveyor belt is approximately 150% faster than in Precision Mode.
___

## Version 0.2.0 (May 21st)

General Changes:
- Classic Mode and Survival Mode were added to the build.
  - In Classic and Survival Mode, the game state is based on the amount of errors made rather than the amount of beverages handled. In Classic Mode, the game will end after the player's third error, while in Survival Mode, the game will end after the player's first error. 
  - The spawn delay time function in Classic and Survival mode is identical to that of Precision Mode for the first 100 beverages. It then decreases to a constant 1 second for beverages 100-149, and again to 0.5 seconds for the 150th beverage and beyond.
  - The conveyor belt speed in Classic and Survival Mode is identical to that of Precision Mode.
- A menu with laser-pointer interactivity was added to the build. The menu contains buttons corresponding to the four game modes, displays brief information about each mode when its respective button is hovered over, and loads a game mode when its respective button is clicked.
- A red "Menu" button was added to each game mode, which immediately returns the player to the menu if pressed.
The player will automatically be returned to the menu after 5 seconds if a lose condition is encountered in Classic and Survival Mode, or if all 100 items have been handled in Precision and Frenzy Mode.
- Sound effects for successful actions and errors were added to each game mode.

Precision Mode:
- The spawn delay time function was increased to 2.75 - (item count / 50) seconds.

Frenzy Mode:
- The spawn delay time was decreased to 1 second.
___

## Version 0.3.0 (May 23rd)

General Changes:
- The width of the last section of the conveyor belt was reduced by 30% in order to accommodate narrower play areas.
- The dimensions of the conveyor belt's rollers were increased in order to prevent beverages from becoming stuck.
- The height of the conveyor belt's rails was extended significantly in order to prevent beverages from falling off of the conveyor belt.
- The recycling bins were given a black nameplate for their text components. The colour of these components was changed from black to white in order to improve readability.
- The distances between the bins were reduced by 80%.
- The back walls of the milk carton and beer bottle bins were extended significantly in order to minimize accidental overthrows.
- The floor and desk counters now destroy beverages upon collision (unless the beverage is still being held by the player), which prevents players from abusing these spaces for penalty-free temporary storage.

Menu:
- The entire UI was placed on a black backdrop, and with the exception of the text on the Classic and Survival Mode buttons, all text colour was changed from black to white.
- The range of the realtime light sources was increased by 30% in order to improve readability.

Precision Mode:
- A timer component was added to the scoreboard. The timer begins when the player presses the green "Start" button, and stops when the 100th beverage is destroyed.

Classic Mode:
- The conveyor belt's speed now reduces to zero for three seconds after each non-terminating mistake, and permanently after a terminating mistake.

Survival Mode:
- The conveyor belt's speed now reduces to zero after a mistake.

___

## Version 0.3.1 (May 25rd)

Precision Mode:
- The angular velocity of the conveyor belt's rollers was reduced by 20% in order to account for an unintended shadow buff that resulted from the width reduction in Version 0.3.0.
- The spawn delay time function was adjusted to 2.5 - (3 * item count / 200) seconds..

Classic Mode:
- The angular velocity of the conveyor belt's rollers was reduced by 20% in order to account for an unintended shadow buff that resulted from the width reduction in Version 0.3.0.
- The spawn delay time function was adjusted to , which resolved an issue with the previous formula that causes beverages 88-99 to spawn faster than beverages 100-149.

Survival Mode:
- The angular velocity of the conveyor belt's rollers was reduced by 20% in order to account for an unintended shadow buff that resulted from the width reduction in Version 0.3.0.
- The spawn delay time function was adjusted to , which resolved an issue with the previous formula that causes beverages 88-99 to spawn faster than beverages 100-149.

Frenzy Mode:
- The angular velocity of the conveyor belt's rollers was reduced by 10% in order to account for an unintended shadow buff that resulted from the width reduction in Version 0.3.0.
___

## Version 0.4.0 (June 1st)

General Changes:
- Free Play Mode was added to the build.
  - In Free Play Mode, the player can control both the speed of the conveyor belt and the rate at which beverages spawn using sliders on the desk adjacent to the conveyor belt. The timer and error counters from Precision Mode are also present in this mode, although they have no effect on the condition of the game. The only way for the player to leave this mode is to press the red "Menu" button on the countertop.
  - The spawn delay time can vary between 0.5 seconds and 5.5 seconds, while the conveyor belt speed can vary between 0% and 100% (for reference, the belt speed is 40% in Classic, Precision, and Survival Mode, and 66% in Frenzy Mode).
- The widths of the milk carton and beer bottle bins' adjacent walls were narrowed by 50%, which decreases the risk of milk cartons landing atop them.
- Soft shadow effects were added to the realtime light sources in order to improve realism within the environment.

Menu:
- A green button corresponding to Free Play Mode was added to the canvas.
- Each button now contains a clear outline, which highlights in orange if the player's laser pointer hovers over the button.

Precision Mode:
- The floor and desk counters now destroy beverages upon collision (unless the beverage is still being held by the player), which prevents players from abusing these spaces for penalty-free temporary storage. This change was erroneously not applied to Precision Mode in Version 0.3.0.

Classic Mode:
- All beverages now disappear when the player commits their third error, which prevents players from obtaining additional points after they have already lost.

Survival Mode:
- All beverages now disappear when the player commits an error, which prevents players from obtaining additional points after they have already lost. 

Frenzy Mode:
- The overall objective of Frenzy Mode has been altered. Rather than aiming for the highest score out of 100 beverages, the player must simply recycle as many beverages as possible in a span of two minutes.
- The scoreboard no longer contains any metrics pertaining to errors or the player's accuracy as a percentage. It only displays the player's score and remaining time.
- The spawn delay time was decreased to 0.9 seconds.
- The sound effect corresponding to an error was removed, as the sheer frequency at which errors are expected to occur would likely cause the sound effect to decrease enjoyment.
- The angular velocity of the conveyor belt components was increased by 10%.

___

## Version 0.4.0 (June 1st)

General Changes:
- Free Play Mode was renamed to Training Mode.
- The material applied to the user's virtual hands was altered in order to be more easily distinguishable from the metallic colour of the desk.

Classic Mode:
- The scoring system was altered from a constant point-based structure to a varying money-based structure. Instead of receiving one point per successful disposal regardless of the item, the player now earns 5 cents for beer bottles, 10 cents for cans and clear plastic bottles, 20 cents for coloured plastic bottles, and 25 cents for milk cartons. In a future build, a currency system that enables the player to buy powerups based on money earned from this game mode will be implemented.

Survival Mode:
- The scoring system was altered from a constant point-based structure to a varying money-based structure. Instead of receiving one point per successful disposal regardless of the item, the player now earns 5 cents for beer bottles, 10 cents for cans and clear plastic bottles, 20 cents for coloured plastic bottles, and 25 cents for milk cartons. In a future build, a currency system that enables the player to buy powerups based on money earned from this game mode will be implemented.
