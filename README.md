# Custom Campaign Tools SDK
A set of scripts to allow BoneLab modders to integrate Campaign features into their campaign mod.

## Important Notes
All script methods MUST be invoked from an UltEvent. If you call methods on custom scripts from a default Unity Event, your game WILL CRASH. If you need to invoke something from a unity event, have it go through an UltEventHolder invoked by the Unity Event.

When making a campaign that supports CustomCampaignTools, it's important to make sure that most things work without the mod installed. If something relies too heavily on the mod (not recommended) you should include a disclaimer for the player in-game. 

## Campaign Options
**Name:** What name to show when selecting a campaign

**Pallet:** The pallet that it should save the json to.

**Intro Level:** A smaller intro level that, if set, will be the initial level loaded in the campaign. Campaign Unlocking can then allow the player to skip the intro, and then entering the campaign will take you straight to the Main Menu.

**Main Menu:** The initial level that will be loaded when selecting a campaign. Includes an optional name override for a context-specific name. For example: Boneworks01_Breakroom (level) with a name override of "Breakroom" This is used for specific cases with BrowsingPlus by Swipez.

**Main Levels:** Which levels are the "Main" campaign levels. These levels will have their ammo saved

**Extra Levels:** Any additional levels that are part of your campaign. These levels will not have their ammo saved.

**Load Scene:** If you have a custom loading screen, this will be used when loading any level from the campaign, or loading from a ContinueCampaign component.

**Load Scene Music:** If set to a specific MonoDisc, it will override the music in the Loading Screen. Custom LoadScene is not required.

**Unlockable Levels:** If enabled, all levels, except the MainMenu, will be locked and cannot be entered. Levels can be "Unlocked" by either the player entering themself (usually through a level transition) or by unlocking by barcode in the CampaignUnlocking script

**Restrict Dev Tools:** Used to restrict dev tools from the player, until unlocked by CampaignUnlocking.

**Avatar Restriction:** Used to restrict avatar switching from the player, until unlocked by CampaignUnlocking. Two main modes are Restrict and Whitelist, with an extra option to Disable Bodylog. While you can techically enable both flags Restrict and Whitelist, Whitelist will take priority over Restrict, so it's best to only choose the one you want. Restrict mode will lock the player to a single avatar, defined by CampaignAvatar, and will block your avatar menu and any avatar changes. Whitelist mode will keep your Avatars menu on, but will only let you switch into avatars that are included in WhitelistedAvatars.

**Campaign Avatar:** The default avatar of the campaign, used if AvatarRestriction is set to Restrict. This mod CAN be from an external mod if needed, but be sure to fill out BaseGameFallbackAvatar for people who don't have the specific external mod installed.

**Base Game Fallback Avatar:** In the case that the player does not have CampaignAvatar installed, this avatar will be used instead. Must make this a base game avatar, or else Avatar Restriction will not work if neither avatars are found.

**Whitelisted Avatars:** Defines a whitelist of avatars that will be allowed if AvatarRestriction is set to Whitelist. 

**Achievement Unlock Sound:** Specifies a custom sound to play when Achievements are unlocked. Can be left blank to use the default notification sound.

**Achievements** Gives you the ability to add your own custom achievements to your campaign. Achievements have the following properties:
 - Key: A unique string that represents the achievement
 - Hidden: Whether or not the description of the Achievement is shown before unlocking.
 - Name: The name of the Achievement
 - Icon: A custom icon for the achievement. If left blank, a default icon will be used.
 - Description: A short description about the Achievement

**Show Campaign In Menu:** If enabled, will show the campaign in the new Campaign menu in G114 and BoneMenu

**Prioritize In Level Panel:** If enabled, the campaign will show itself at the front of the level panel while you are currently in the campaign. Enabled by default, but useful to disable if you want campaign features without messing up the levels panel.

**Save Ammo Between Levels:** If enabled, your high-score of ammo will be saved for each level in MainLevels. Ammo is NOT saved in ExtraLevels or MainMenu. Functions similar to Boneworks Ammo Saving

**Save Level Inventory:** If enabled, your inventory will transfer between Main levels

**Inventory Save Filter:** Select only certain items to save in your inventory between levels. If emtpy, all items will transfer.

**Lock Player In Campaign:** If enabled, entering the campaign through G114 or Bonemenu will "Lock" the player in the campaign. Locked in campaign means that the player's level list will be limited to only the campaign levels, and the only way to exit the campaign is by pressing "Exit Campaign" in the player's option menu. This button will leave the current campaign and take the player back to G114. This will not lock the player if they enter a level through the levels menu, only if officially "entered".

**Campaign Unlockables:** A list of crates that are marked as Unlockable in the pallet. Not all have to be included, but when a player resets their save, all of the listed unlockable crates will be re-locked.

**Hide Crates from Gachapon:** Specifies which campaign spawnables should not be unlockable through the Gachapon Machine in G114. Helps keep the fun of unlocking things in the campaign without having to redact them.

## Scripts Usage Info

### Ammo Score Display
Usage Info: Used on a TextMeshPro component, call the SetTargetBarcode method from an UltEvent to setup the correct barcode. It will then display the saved ammo high score from that specific level.

### CampaignUnlocking
Usage Info: In cases where a campaign has RestrictDevTools or RestrictAvatars enabled, you can call UnlockDevTools or UnlockAvatars to allow the player to use Dev Tools or Avatars through your campaign. Useful if you want to lock the use of DevTools and Avatars until the campaign has been completed, by invoking the Unlock methods at the end of the last level. The methods also have a bool to enableInstantly, which will give the player immediate control back, unhiding their bodylog and returning the dev tools respectively. If enableInstantly is set to false, access to dev tools or avatars will be granted once the next level is loaded. It can also be used to unlock levels manually by barcode if Unlockable Levels is enabled, useful for unlocking a level that the player would not be able to get to normally.

### Save Point
Usage Info: Save Points can be placed around your campaign. In order to save a SavePoint, call the Save method. This will save the level, the current position, as well as the player's inventory into the save to load back to later. See "ContinueCampaign" component for more info on making a continue button.
If applicable, Save Points are also able to save items other than just your inventory. Based on the functionality in Boneworks, by putting a Box Collider set to Trigger on your save point (moved around independently by editing collider center), any spawnable items that intersect the trigger at the time of saving are saved as well. When loading from a save point, the player's inventory and ammo is restored and all items will be spawned at the box trigger's center position.

### Continue Campaign
Usage Info: Used for interfacing with the current SavePoint. Calling the Continue() method will load you to your save point. To have a Continue button that only shows when there is a valid save point, you can invoke the EnableIfValidSave() method, passing in the Button gameobject, just make sure that it is disabled beforehand. Then, you can just have the button call the Continue method when clicked.

### Invoke In Campaign
Usage Info: Requiring an UltEventHolder, Invokes the ulteventholder if the campaign mod is loaded. Useful for executing special functionality when a player has this mod installed.

### Unhide In Campaign
Usage Info: Used in conjunction with HideOnAwake, will unhide the object if the campaign mod is loaded. Useful for having things that only enable when a player has this mod installed.

### Variable Manager
Usage Info: CustomCampaignTools's saving system has a section for saving specific float values that can be set by the modder. Interfacing everything through ultevents, you can call SetValue, IncrementValue, GetValue (to get a return value to use as a parameter), and InvokeIf, which provides functionality for invoking a given UltEventHolder if the value matches the comparison type to the compared value. It is a bit complex, and definately not NEEDED for every campaign, but I wanted an easy way to save and read your own data.

### Campaign Achievement Manager
Usage Info: Used to unlock achievements by key. When an achievement is unlocked, it will send a BoneMenu notification. Call UnlockAchievement and put in the unique key of the achievement to unlock.

### Achievement Display
Usage Info: Used to display all achievements from the current campaign in your level. References can be set up like normal unity scripts. Then call Activate when the player enters a bounding area near the display.

### Achievement Reference Holder
Usage Info: Used with AchievementDisplay in order to specify which objects to show achievement name, description, and icon on. You can have multiple reference holders per page.

### Spawner Despawn Saver
Usage Info: When Setup with a Crate Spawner, SavePoints will keep track of if the spawnable was Despawned, as well as if the AIBrain was killed. When the save point is saved, all despawned spawnables or killed NPCs with this script will not be spawned in when continuing. Best Practices: Put on all Ammo Boxes and Ammo Dest Boxes to prevent the player from picking up the same ammo twice, and put on all non-encounter NPCs to not respawn them if they have been killed. (Likely to not be saved if used in cratespawners in chunks, make sure its always in the persistent scene.)

### GameObject Enabled Saver
Usage Info: When on any GameObject, this object's active state will be saved when a SavePoint is activated, and will be set appropriately when continuing from that save point.

### Trial Timer
Usage Info: Used to Start, Pause, and End TrialTimers. Best to use in combination with the BoneTimer script from the Extended SDK if you want to display it in realtime, as TrialTimers work in the background and do not constantly update their time for performance reasons. Every ended TrialTimer is saved to calculate Best, Latest, and Average times for each Trial Key. You can start multiple timers with the same script, each one is based on the Trial Key that you use in the UltEvent.

### Timer Display
Usage Info: A big multi-use script for displaying different time values from CustomCampaignTools. Has the ability to display Total Time in Campaign, Total Time per Level, as well as saved Trial Times. Trial Times can display the Best, Latest, or Average time. All Timer Displays have customizable time display, and you can add Prefix and Postfix text with ultevents to add text before or after the time text for formatting reasons.

### Version Check
Usage Info: Allows you to invoke events if the CustomCampaignTools version is equal to or above a specific version. Useful for disabling an "Update" message if the user has the required update already installed. It is best to have messages like these to be defaulted enabled, so that people without latest campaign tools (who might not have the Version Check script) will see the Update message without any missing script running. Then, just use Version Check to disable it as long as they have a good version.