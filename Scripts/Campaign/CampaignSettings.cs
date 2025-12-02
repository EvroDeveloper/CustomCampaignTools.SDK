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
                levelBarcode = this.levelCrate.Barcode.ID,
                levelName = this.levelName,
            };
        }
#endif
    }

    [CreateAssetMenu(fileName = "New Campaign.asset", menuName = "Custom Campaign Tools/Campaign Settings", order = 1)]
    public class CampaignSettings : ScriptableObject
    {
        public Pallet Pallet;
        public string Name = "New Campaign";

        [Header("Levels")]
        [Space(10)]
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
        [Tooltip("The default avatar to use in the campaign. Can be from an optional mod")]
        public AvatarCrateReference CampaignAvatar;
        [Tooltip("A fallback base-game avatar if the player does not have Campaign Avatar installed (in the case of a modded avatar)")]
        public AvatarCrateReference BaseGameFallbackAvatar;
        [Tooltip("If Avatar Restriction is set to Whitelist, these will be allowed")]
        public AvatarCrateReference[] WhitelistedAvatars;

        [Header("Achievements")]
        public DataCardReference<MonoDisc> AchievementUnlockSound;
        public SerializableAchievement[] Achievements;

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
        [Tooltip("When the player enters the campaign from Bonemenu or the Main Menu, they will not be able to leave until they press the Exit Campaign button in their menu")]
        public bool LockPlayerInCampaign;
        [Tooltip("When the player resets their CampaignTools save, these crates will get re-locked as well")]
        public SpawnableCrateReference[] CampaignUnlockCrates;
        [Tooltip("These spawnables will NOT be unlockable through the Gachapon machine in G114")]
        public SpawnableCrateReference[] HideCratesFromGachapon;
        [Tooltip("If this is a development build, enabling this will allow you to access dev menu in bonemenu for testing")]
        public bool DevBuild = false;
        

#if UNITY_EDITOR
        //[ContextMenu("Add Assets to Addressables")]
        public void AddAssetsToAddressables()
        {
            foreach (SerializableAchievement a in Achievements)
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

            // Convert the asset path to a GUID
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
            string outputPath = Path.Combine(AddressablesManager.GetBuiltModFolder(Pallet), "campaign.json.bundle");
            File.WriteAllText(outputPath, json);
        }


        public static List<string> CrateArrayToBarcodes(ScannableReference[] levelCrateReferences)
        {
            List<string> barcodes = new List<string>();
            foreach(ScannableReference re in levelCrateReferences)
            {
                barcodes.Add(re.Barcode.ID);
            }
            return barcodes;
        }
#endif
    }

    [Flags]
    public enum AvatarRestrictionType
    {
        None = 0,
        DisableBodyLog = 1,
        RestrictAvatar = 2,
        EnforceWhitelist = 4
    }

    [Serializable]
    public struct SerializableAchievement
    {
        public string Key;
        public bool Hidden;
        public MarrowAssetT<Texture2D> Icon;
        public string Name;
        public string Description;

#if UNITY_EDITOR
        public AchievementData ConvertToData()
        {
            byte[] IconBytes = new byte[0];

            string path = AssetDatabase.GUIDToAssetPath(Icon.AssetGUID);
            if (File.Exists(path))
                IconBytes = File.ReadAllBytes(path);

            return new AchievementData()
            {
                Key = Key,
                Hidden = Hidden,
                IconBytes = IconBytes,
                Name = Name,
                Description = Description,
            };
        }
        
        public static List<AchievementData> ConvertToData(SerializableAchievement[] achievements)
        {
            var list = new List<AchievementData>();
            foreach (var achievement in achievements)
            {
                list.Add(achievement.ConvertToData());
            }
            return list;
        }
#endif
    }

    public struct AchievementData
    {
        public string Key { get; set; }
        public bool Hidden { get; set; }
        public byte[] IconBytes { get; set; }
        public string IconGUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
