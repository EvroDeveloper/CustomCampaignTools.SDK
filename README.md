# Custom Campaign Tools SDK
A set of scripts to allow BoneLab modders to integrate Campaign features into their campaign mod.

## Important Notes
All script methods MUST be invoked from an UltEvent. If you call methods on custom scripts from a default Unity Event, your game WILL CRASH. If you need to invoke something from a unity event, have it go through an UltEventHolder invoked by the Unity Event.

When making a campaign that supports CustomCampaignTools, it's important to make sure that most things work without the mod installed. If something relies too heavily on the mod (not recommended) you should include a disclaimer for the player in-game. 

## Campaign Options
**Name:** What name to show when selecting a campaign

**MainLevels:** Which levels are the "Main" campaign levels. These levels will have their ammo saved

**ExtraLevels:** Any additional levels that are part of your campaign. These levels will not have their ammo saved.

**MainMenu:** The initial level that will be loaded when selecting a campaign.

**LoadScene:** If you have a custom loading screen, this will be used when loading the campaign, or loading from a ContinueCampaign component.

**ShowCampaignInMenu** If enabled, will show the campaign in the new Campaign menu in G114 and BoneMenu

**RestrictDevTools:** Used to restrict dev tools from the player, until unlocked by CampaignUnlocking.

**RestrictAvatars:** Used to restrict avatar switching from the player, until unlocked by CampaignUnlocking.

**CampaignAvatar:** When RestrictAvatars is enabled, this avatar will be used by default. If set to none, you will be restricted to use whatever avatar you were last wearing. Kinda broken if you leave this blank.

**SaveAmmoBetweenLevels** If enabled, your high-score of ammo will be saved for each level in MainLevels. Ammo is NOT saved in ExtraLevels or MainMenu. Functions similar to Boneworks Ammo Saving

**Achievements** Gives you the ability to add your own custom achievements to your campaign. Achievements have the following properties:
 - Key: A unique string that represents the achievement
 - Hidden: Whether or not the description of the Achievement is shown before unlocking.
 - Name: The name of the Achievement
 - Icon: A custom icon for the achievement. If left blank, a default icon will be used.
 - Description: A short description about the Achievement


## Scripts Usage Info

### Ammo Score Display
Usage Info: Used on a TextMeshPro component, call the SetTargetBarcode method from an UltEvent to setup the correct barcode. It will then display the saved ammo high score from that specific level.

### CampaignUnlocking
Usage Info: In cases where a campaign has RestrictDevTools or RestrictAvatars enabled, you can call UnlockDevTools or UnlockAvatars to allow the player to use Dev Tools or Avatars (bodylog) through your campaign. Useful if you want to lock the use of DevTools and Avatars until the campaign has been completed, by invoking the Unlock methods at the end of the last level. The methods also have a bool to enableInstantly, which will give the player immediate control back, unhiding their bodylog and returning the dev tools respectively. If enableInstantly is set to false, access to dev tools or avatars will be granted once the next level is loaded.

### Save Point
Usage Info: Save Points can be placed around your campaign. In order to save a SavePoint, call the Save method. This will save the level, the current position, as well as the player's inventory into the save to load back to later. See "ContinueCampaign" component for more info on making a continue button.
If applicable, Save Points are also able to save items other than just your inventory. Based on the functionality in Boneworks, by putting a Box Collider set to Trigger on your save point (moved around independently by editing collider center), any spawnable items that intersect the trigger at the time of saving are saved as well. When loading from a save point, all items will be spawned at the box trigger's center position.

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
Usage Info: Used to display all achievements from the current campaign in your level. Call SetupReferences on Start to specify navigation buttons and TextMeshPro components. Then call Activate when the player enters a bounding area near the display.

### Achievement Reference Holder
Usage Info: Used with AchievementDisplay in order to specify which objects to show achievement name, description, and icon on. Call SetupReferences on Awake

