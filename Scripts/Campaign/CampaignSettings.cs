using UnityEngine;
using SLZ.Marrow.Warehouse;
using System.Collections.Generic;
using Newtonsoft.Json;
using System;
using System.Linq;
using SLZ.MarrowEditor;
using SLZ.MLAgents;
using System.IO;
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

        internal SerializedLevelSetup Serialize()
        {
            return new SerializedLevelSetup()
            {
                levelBarcode = this.levelCrate.Barcode.ID,
                levelName = this.levelName,
            };
        }
    }

    [CreateAssetMenu(fileName = "New Campaign.asset", menuName = "Custom Campaign Tools/Campaign Settings", order = 1)]
    public class CampaignSettings : ScriptableObject
    {
        public Pallet Pallet;
        public string Name = "New Campaign";

        [Header("Levels")]
        [Space(10)]
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
        public Achievement[] Achievements;

        [Header("Extra Options")]
        [Space(10)]
        [Tooltip("Specifies whether or not this Campaign will show up in the Campaigns section of Void G114")]
        public bool ShowCampaignInMenu = true;
        public bool SaveAmmoBetweenLevels = true;
        //public bool SaveWeaponsBetweenLevels = false;
        [Tooltip("When the player enters the campaign from Bonemenu or the Main Menu, they will not be able to leave until they press the Exit Campaign button in their menu")]
        public bool LockPlayerInCampaign;
        [Tooltip("When the player resets their CampaignTools save, these crates will get re-locked as well")]
        public SpawnableCrateReference[] CampaignUnlockCrates;
        

#if UNITY_EDITOR
        //[ContextMenu("Add Assets to Addressables")]
        public void AddAssetsToAddressables()
        {
            foreach (Achievement a in Achievements)
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
            var data = new CampaignLoadingData()
            {
                Name = Name,
                InitialLevel = MainMenu.Serialize(),
                MainLevels = SerializeLevelArray(MainLevels),
                ExtraLevels = SerializeLevelArray(ExtraLevels),
                LoadScene = LoadScene.Barcode.ID,
                LoadSceneMusic = LoadSceneMusic.Barcode.ID,
                UnlockableLevels = UnlockableLevels,
                ShowInMenu = ShowCampaignInMenu,
                RestrictDevTools = RestrictDevTools,
                AvatarRestrictionType = AvatarRestriction,
                WhitelistedAvatars = CrateArrayToBarcodes(WhitelistedAvatars),
                CampaignAvatar = CampaignAvatar.Barcode.ID,
                BaseGameFallbackAvatar = BaseGameFallbackAvatar.Barcode.ID,
                SaveLevelWeapons = false,
                //SaveWeaponsBetweenLevels,
                SaveLevelAmmo = SaveAmmoBetweenLevels,
                Achievements = Achievements.ToData(),
                LockInCampaign = LockPlayerInCampaign,
                CampaignUnlockCrates = CrateArrayToBarcodes(CampaignUnlockCrates),
            };
            var settings = new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Serialize,
                PreserveReferencesHandling = PreserveReferencesHandling.Objects
            };
            string json = JsonConvert.SerializeObject(data, settings);
            string outputPath = Path.Combine(AddressablesManager.GetBuiltModFolder(Pallet), "campaign.json.bundle");
            File.WriteAllText(outputPath, json);
        }


        private List<string> CrateArrayToBarcodes(ScannableReference[] levelCrateReferences)
        {
            List<string> barcodes = new List<string>();
            foreach(ScannableReference re in levelCrateReferences)
            {
                barcodes.Add(re.Barcode.ID);
            }
            return barcodes;
        }

        private List<SerializedLevelSetup> SerializeLevelArray(LevelSetup[] input)
        {
            List<SerializedLevelSetup> putput = new List<SerializedLevelSetup>();
            foreach(LevelSetup level in input)
            {
                putput.Add(level.Serialize());
            }
            return putput;
        }
#endif
    }
#if UNITY_EDITOR
    internal class CampaignLoadingData
    {
        public string Name { get; set; }
        public SerializedLevelSetup InitialLevel { get; set; }
        public List<SerializedLevelSetup> MainLevels { get; set; }
        public List<SerializedLevelSetup> ExtraLevels { get; set; }
        public string LoadScene { get; set; }
        public string LoadSceneMusic { get; set; }
        public bool UnlockableLevels { get; set; }
        public bool ShowInMenu { get; set; }
        public bool RestrictDevTools { get; set; }
        public AvatarRestrictionType AvatarRestrictionType { get; set; }
        public string CampaignAvatar { get; set; }
        public string BaseGameFallbackAvatar { get; set; }
        public List<string> WhitelistedAvatars { get; set; }
        public bool SaveLevelWeapons { get; set; }
        public bool SaveLevelAmmo { get; set; }
        public List<AchievementData> Achievements { get; set; }
        public bool LockInCampaign { get; set; }
        public List<string> CampaignUnlockCrates { get; set; }
    }


    internal class SerializedLevelSetup
    {
        public string levelBarcode;
        public string levelName;
    }
#endif

    [Flags]
    public enum AvatarRestrictionType
    {
        None = 0,
        DisableBodyLog = 1,
        RestrictAvatar = 2,
        EnforceWhitelist = 4
    }

    [Serializable]
    public struct Achievement
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
            if(File.Exists(path))
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
#endif
    }

#if UNITY_EDITOR
    public static class AchievementArrayExtensions
    {
        public static List<AchievementData> ToData(this Achievement[] achievements)
        {
            var list = new List<AchievementData>();
            foreach (var achievement in achievements)
            {
                list.Add(achievement.ConvertToData());
            }
            return list;
        }
    }

    public struct AchievementData
    {
        public string Key { get; set; }
        public bool Hidden { get; set; }
        public byte[] IconBytes { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
#endif
}