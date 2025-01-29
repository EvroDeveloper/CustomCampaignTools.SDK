# Custom Campaign Tools SDK
A set of scripts to allow BoneLab modders to integrate Campaign features into their campaign mod.

## Important Notes
When making a campaign that supports CustomCampaignTools, it's important to make sure that most things work without the mod installed. If something relies too heavily on the mod (not recommended) you should include a disclaimer for the player in-game. 

## Ammo Score Display
Usage Info: Used on a TextMeshPro component, call the SetTargetBarcode method from an UltEvent to setup the correct barcode. It will then display the saved ammo high score from that specific level.

## CampaignUnlocking
Usage Info: In cases where a campaign has RestrictDevTools or RestrictAvatars enabled, you can call UnlockDevTools or UnlockAvatars to allow the player to use Dev Tools or Avatars (bodylog) through your campaign. Useful if you want to lock the use of DevTools and Avatars until the campaign has been completed, by invoking the Unlock methods at the end of the last level.

## Save Point
Usage Info: Save Points can be placed around your campaign. In order to save a SavePoint, call the Save method. This will save the level, the current position, as well as the player's inventory into the save to load back to later. See "ContinueCampaign" component for more info on making a continue button.

## Continue Campaign
Usage Info: Used for interfacing with the current SavePoint. Calling the Continue() method will load you to your save point. To have a Continue button that only shows when there is a valid save point, you can invoke the EnableIfValidSave() method, passing in the Button gameobject, just make sure that it is disabled beforehand. Then, you can just have the button call the Continue method when clicked.

## Invoke In Campaign
Usage Info: Requiring an UltEventHolder, Invokes the ulteventholder if the campaign mod is loaded. Useful for executing special functionality when a player has this mod installed.

## Unhide In Campaign
Usage Info: Used in conjunction with HideOnAwake, will unhide the object if the campaign mod is loaded. Useful for having things that only enable when a player has this mod installed.

## Variable Manager
Usage Info: CustomCampaignTools's saving system has a section for saving specific float values that can be set by the modder. Interfacing everything through ultevents, you can call SetValue, IncrementValue, GetValue (to get a return value to use as a parameter), and InvokeIf, which provides functionality for invoking a given UltEventHolder if the value matches the comparison type to the compared value. It is a bit complex, and definately not NEEDED for every campaign, but I wanted an easy way to save and read your own data.