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
    [CreateAssetMenu(fileName = "New Campaign.asset", menuName = "Custom Campaign Tools/Campaign Settings", order = 1)]
    public class CampaignSettings : ScriptableObject
    {
        public Pallet Pallet;
        public string Name = "New Campaign";
        public LevelCrateReference MainMenu;
        public LevelCrateReference[] MainLevels;
        public LevelCrateReference[] ExtraLevels;
        public LevelCrateReference LoadScene;
        public bool ShowCampaignInMenu = true;
        public bool RestrictDevTools = false;
        public bool RestrictAvatar = false;
        public AvatarCrateReference CampaignAvatar;
        public bool SaveWeaponsBetweenLevels = false;
        public bool SaveAmmoBetweenLevels = true;
        public Achievement[] Achievements;

        [ContextMenu("Add Assets to Addressables")]
        public void AddAssetsToAddressables()
        {
            foreach (Achievement a in Achievements)
            {
                AddTextureToAddressables(a.Icon);
            }
        }

        public static void AddTextureToAddressables(MarrowAssetT<Texture2D> texture, string groupName = "Campaign", string addressName = null)
        {
#if UNITY_EDITOR
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
#endif
        }

#if UNITY_EDITOR
        [ContextMenu("Save Json")]
        public void SaveCampaignJson()
        {
            var data = new CampaignLoadingData()
            {
                Name = Name,
                InitialLevel = MainMenu.Barcode.ID,
                MainLevels = LevelCrateArrayToBarcodes(MainLevels),
                ExtraLevels = LevelCrateArrayToBarcodes(ExtraLevels),
                LoadScene = LoadScene.Barcode.ID,
                ShowInMenu = ShowCampaignInMenu,
                RestrictDevTools = RestrictDevTools,
                RestrictAvatar = RestrictAvatar,
                CampaignAvatar = CampaignAvatar.Barcode.ID,
                SaveLevelWeapons = SaveWeaponsBetweenLevels,
                SaveLevelAmmo = SaveAmmoBetweenLevels,
                Achievements = Achievements.ToData(),
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
#endif

        private List<string> LevelCrateArrayToBarcodes(LevelCrateReference[] levelCrateReferences)
        {
            List<string> barcodes = new List<string>();
            foreach(LevelCrateReference re in levelCrateReferences)
            {
                barcodes.Add(re.Barcode.ID);
            }
            return barcodes;
        }
    }

    internal class CampaignLoadingData
    {
        public string Name { get; set; }
        public string InitialLevel { get; set; }
        public List<string> MainLevels { get; set; }
        public List<string> ExtraLevels { get; set; }
        public string LoadScene { get; set; }
        public bool ShowInMenu { get; set; }
        public bool RestrictDevTools { get; set; }
        public bool RestrictAvatar { get; set; }
        public string CampaignAvatar { get; set; }
        public bool SaveLevelWeapons { get; set; }
        public bool SaveLevelAmmo { get; set; }
        public List<AchievementData> Achievements { get; set; }
    }

    [Serializable]
    public struct Achievement
    {
        public string Key;
        public MarrowAssetT<Texture2D> Icon;
        public string Name;
        public string Description;

        public AchievementData ConvertToData()
        {
            return new AchievementData()
            {
                Key = Key,
                IconGUID = Icon.AssetGUID,
                Name = Name,
                Description = Description,
            };
        }

        
    }

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
        public string IconGUID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}