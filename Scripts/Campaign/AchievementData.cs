#if MELONLOADER
using Il2CppSLZ.Marrow.Warehouse;
using CustomCampaignTools.Debug;
using CustomCampaignTools.Utilities;
#else
using SLZ.Marrow.Warehouse;
using Newtonsoft.Json;
#endif
using UnityEngine;
using System;
#if UNITY_EDITOR
using UnityEditor;
using System.IO;
using System.Runtime.Serialization;
#endif

namespace CustomCampaignTools
{
    [Serializable]
    public class AchievementData
    {
        private enum IconType
        {
            Bytes,
            Addressable
        }
        private const IconType ICON_TYPE = IconType.Bytes; // force icon type to be bytes for now, idk how to add icons to addressables yet
        public string Key;
        public string Name;
        public string Description;
        public bool Hidden;
#if UNITY_EDITOR
        [JsonIgnore]
        public MarrowAssetT<Texture2D> Icon;
#endif
        public byte[] IconBytes { get; set; }
        public string IconGUID { get; set; }

#if MELONLOADER
        private MarrowAssetT<Texture2D> _iconAsset;
        
        public MarrowAssetT<Texture2D> IconAsset
        {
            get
            {
                if(_iconAsset == null)
                {
                    _iconAsset = new MarrowAssetT<Texture2D>(IconGUID);
                }
                return _iconAsset;
            }
        }
#endif

#if UNITY_EDITOR
        [OnSerializing]
        public void WriteIconData()
        {
            if(ICON_TYPE == IconType.Addressable)
            {
                IconGUID = Icon.AssetGUID;
            }
            else
            {
                string path = AssetDatabase.GUIDToAssetPath(Icon.AssetGUID);
                if (File.Exists(path))
                    IconBytes = File.ReadAllBytes(path);
            }
        }
#endif

#if MELONLOADER
        public Texture2D cachedTexture;
        public Sprite cachedSprite;

        public void LoadIcon(Action<Texture2D> callback)
        {
            Action<Texture2D> realCallback = (T) =>
            {
                T = T.ProperResize(336, 336);
                callback(T);
            };

            var loadingTexture = new Texture2D(2, 2);

            if (IconBytes.Length != 0)
            {
                if (!loadingTexture.LoadImage(IconBytes, false))
                {
                    CampaignLogger.Error("Failed to load texture from embedded resource.");
                    return;
                }
                realCallback(loadingTexture);
            }
            else if (IconGUID != "")
            {
                IconAsset.LoadAsset(realCallback);
            }
            else
            {
                CampaignLogger.Error("No valid icon data found for achievement: " + Key);
                return;
            }
        }

        public void CacheTexture(Action<Texture2D> callback = null)
        {
            if(cachedTexture == null)
            {
                LoadIcon((loadedTexture) =>
                {
                    cachedTexture = loadedTexture;
                    cachedTexture.hideFlags = HideFlags.DontUnloadUnusedAsset;
                });
            }
            callback?.Invoke(cachedTexture);
        }

        public void CacheSprite(Action<Sprite> callback = null)
        {
            if(cachedSprite == null)
            {
                LoadIcon((loadedTexture) =>
                {
                    cachedTexture = loadedTexture;
                    cachedTexture.hideFlags = HideFlags.DontUnloadUnusedAsset;

                    cachedSprite = Sprite.Create(cachedTexture, new Rect(0, 0, cachedTexture.width, cachedTexture.height), new Vector2(0.5f, 0.5f));
                    cachedSprite.hideFlags = HideFlags.DontUnloadUnusedAsset;
                });
            }
            callback?.Invoke(cachedSprite);
        }

        public void CacheResources(Action<Texture2D, Sprite> callback)
        {
            CacheTexture();
            CacheSprite();
            callback.Invoke(cachedTexture, cachedSprite);
        }
#endif
    }
}
