using UnityEngine;
using SLZ.Marrow.Warehouse;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using SLZ.MarrowEditor;
using SLZ.MLAgents;
using System.IO;
using System.ComponentModel;
using CustomCampaignTools;
using SimpleSerializables.Types;
#if UNITY_EDITOR
using UnityEditor.AddressableAssets.Settings;
using UnityEditor.AddressableAssets;
using UnityEditor;
#endif

namespace CustomCampaignTools.SDK
{
    [Serializable]
    public struct LevelSetup
    {
        public LevelCrateReference levelCrate;
        public string levelName;
#if UNITY_EDITOR
        internal SerializedLevelSetup Serialize()
        {
            return new SerializedLevelSetup()
            {
                levelBarcode = new(this.levelCrate),
                levelName = this.levelName,
            };
        }
#endif
        public override string ToString()
        {
            if(!string.IsNullOrEmpty(levelName)) return levelName;
            if(levelCrate.IsValid()) return levelCrate.Crate.Title;
            return base.ToString();
        }
    }

    [CreateAssetMenu(fileName = "New Campaign.asset", menuName = "Custom Campaign Tools/Campaign Settings", order = 1)]
    public class CampaignSettings : ScriptableObject
    {
        public Pallet Pallet;
        public string Name = "New Campaign";

        [Header("Levels")]
        [Space(10)]
        [Tooltip("Not Required. Serves as a skippable Intro Level, and functions like Scene Intro Start from Boneworks")]
        public LevelSetup IntroLevel;
        public LevelSetup MainMenu;
        public LevelSetup[] MainLevels;
        public LevelSetup[] ExtraLevels;
        public LevelCrateReference LoadScene;
        public MonoDiscReference LoadSceneMusic;
        [Tooltip("If enabled, levels will not show up in the Levels menu until they have been entered, or unlocked via CampaignUnlocking")]
        public bool UnlockableLevels;

        [Header("Cheat Restriction (Unlock with CampaignUnlocking)")]
        [Space(10)]
        public bool RestrictDevTools = false;
        [Tooltip("Support for restricting the avatar during the campaign. \nRestriction type will enforce a single avatar and disable the avatar menu. \nWhitelist will override Restriction and will let the player choose from a whitelist of avatars. \nAvatar Restriction can be bypassed when CampaignUnlocking.UnlockAvatar is called.")]
        public AvatarRestrictionType AvatarRestriction = AvatarRestrictionType.None;
        [Tooltip("Disable the bodylog while the avatar is restricted. Requires a type of AvatarRestriction")]
        public bool DisableBodyLog;
        [Tooltip("The default avatar to use in the campaign. Can be from an optional mod")]
        public AvatarCrateReference CampaignAvatar;
        [Tooltip("A fallback base-game avatar if the player does not have Campaign Avatar installed (in the case of a modded avatar)")]
        public AvatarCrateReference BaseGameFallbackAvatar;
        [Tooltip("If Avatar Restriction is set to Whitelist, these will be allowed")]
        public AvatarCrateReference[] WhitelistedAvatars;

        [Tooltip("Valid range of Avatar Heights to be allowed")]
        public Vector2 AvatarHeightRange;
        [Tooltip("Valid range of Avatar Mass to be allowed")]
        public Vector2 AvatarMassRange;
        [Tooltip("Valid range of Avatar Arm Length to be allowed")]
        public Vector2 AvatarArmRange;

        [Header("Achievements")]
        public MonoDiscReference AchievementUnlockSound;
        public AchievementData[] Achievements;

        [Header("Extra Options")]
        [Space(10)]
        [Tooltip("Specifies whether or not this Campaign will show up in the Campaigns section of Void G114")]
        public bool ShowCampaignInMenu = true;
        [Tooltip("If true, this campaign will be prioritized and shown at the top of the player's level panel when in the campaign")]
        public bool PrioritizeInLevelPanel = true;
        public bool SaveAmmoBetweenLevels = true;
        public bool SaveInventoryBetweenLevels = false;
        [Tooltip("If Save Inventory Between Levels is enabled, only these items will be saved. If empty, all items will be saved.")]
        public SpawnableCrateReference[] SaveInventoryFilter;
        [Tooltip("If Enabled, entering any Main level will create a new save at that level, overwriting the previous one")]
        public bool UpdateSaveOnLevelEnter = true;
        [Tooltip("When the player enters the campaign from Bonemenu or the Main Menu, they will not be able to leave until they press the Exit Campaign button in their menu")]
        public bool LockPlayerInCampaign;
        [Tooltip("When the player resets their CampaignTools save, these crates will get re-locked as well")]
        public SpawnableCrateReference[] CampaignUnlockCrates;
        [Tooltip("These spawnables will NOT be unlockable through the Gachapon machine in G114")]
        public SpawnableCrateReference[] HideCratesFromGachapon;
        [Tooltip("Override Spawnable for the Rig Manager. DO NOT USE unless you know what you are doing")]
        public SpawnableCrateReference RigManagerOverride;
        [Tooltip("Override Spawnable for the Gameplay Rig. DO NOT USE unless you know what you are doing")]
        public SpawnableCrateReference GameplayRigOverride;
        [Tooltip("Custom Code Mod assembly that Custom Campaign Tools can load in. GENUINELY DO NOT USE THIS UNLESS YOU HAVE ELITE BALL KNOWLEDGE")]
        public TextAsset CampaignSupportAssembly;
        [Tooltip("If this is a development build, enabling this will allow you to access dev menu in bonemenu for testing")]
        public bool DevBuild = false;

        [SerializeField]
        private bool foldoutLevels = true;
        [SerializeField]
        private bool foldoutCheats = true;
        [SerializeField]
        private bool foldoutExtras = true;
        [SerializeField]
        private bool foldoutExperimental = false;
        

#if UNITY_EDITOR
        //[ContextMenu("Add Assets to Addressables")]
        public void AddAssetsToAddressables()
        {
            foreach (var a in Achievements)
            {
                AddTextureToAddressables(a.Icon);
            }
        }

        public static void AddTextureToAddressables(MarrowAssetT<Texture2D> texture, string groupName = "Campaign", string addressName = null)
        {
            AddressableAssetSettings settings = AddressableAssetSettingsDefaultObject.GetSettings(true);
            if (settings == null)
            {
                Debug.LogError("Could not find default AddressableAssetSettings. Make sure Addressables is set up properly.");
                return;
            }
            AddressableAssetGroup group = settings.DefaultGroup;

            string guid = texture.AssetGUID;

            // Create or move the entry to the specified group
            AddressableAssetEntry entry = settings.CreateOrMoveEntry(guid, group);
            if (entry == null)
            {
                Debug.LogError($"Failed to create or move entry for asset at path: {texture}");
                return;
            }

            if (string.IsNullOrEmpty(addressName))
            {
                addressName = texture.EditorAsset.name;
            }
            entry.SetAddress(addressName);

            EditorUtility.SetDirty(settings);

            Debug.Log($"Texture '{texture.EditorAsset.name}' successfully added to Addressable group with address '{addressName}'.");
        }

        [ContextMenu("Save Json")]
        public void SaveCampaignJson()
        {
            if (Pallet == null)
            {
                Debug.LogError("No pallet assigned to CampaignSettings. Please assign a pallet before saving the campaign JSON.");
                return;
            }

            var data = CampaignLoadingData.CopyFrom(this);

            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(data, settings);
            string outputPath = Path.Combine(AddressablesManager.GetBuiltModFolder(Pallet), CampaignConstants.CampaignJsonFileName);
            File.WriteAllText(outputPath, json);
        }


        public static List<ScannableRefSer<T>> CrateArrayToBarcodes<T>(T[] levelCrateReferences) where T : ScannableReference
        {
            List<ScannableRefSer<T>> barcodes = new List<ScannableRefSer<T>>();
            foreach(T re in levelCrateReferences)
            {
                barcodes.Add(new(re));
            }
            return barcodes;
        }
#endif
    }
}
