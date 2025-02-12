using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Campaign Reflection")]
    public class CampaignReflection : MonoBehaviour
    {
        public string GetName() { }

        public int GetAchievementsUnlocked() { }
        public int GetAchievementsTotal() { }

        public int GetAmmoFromLevel(string barcode) { }

        public bool GetSavePointValid() { }
        public string GetSavePointLevelBarcode() { }
        public string GetSavePointLevelName () { }


    }
}