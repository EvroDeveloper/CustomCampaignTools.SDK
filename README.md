# Custom Campaign Tools

## Ammo Score Display
Usage Info: Used on a TextMeshPro component, call the SetTargetBarcode method from an UltEvent to setup the correct barcode. It will then display the saved ammo high score from that specific level.

## CampaignUnlocking
Usage Info: In cases where a campaign has RestrictDevTools or RestrictAvatars enabled, you can call UnlockDevTools or UnlockAvatars to allow the player to use Dev Tools or Avatars (bodylog) through your campaign. Useful if you want to lock the use of DevTools and Avatars until the campaign has been completed, by invoking the Unlock methods at the end of the last level.

## Save Point
Usage Info: Save Points can be placed around your campaign. In order to save a SavePoint, call the Save method. This will save the level, the current position, as well as the player's inventory into the save to load back to later. See "ContinueCampaign" component for more info on making a continue button.

## Continue Campaign
Usage Info: Used for interfacing with the current SavePoint. Calling the Continue() method will load you to your save point. To have a Continue button that only shows when there is a valid save point, you can invoke the EnableIfValidSave() method, passing in the Button gameobject.