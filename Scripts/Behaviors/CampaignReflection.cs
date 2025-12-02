using UnityEngine;
using System;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Campaign Reflection")]
    public class CampaignReflection : MonoBehaviour
    {
        public string GetName()
        {
            return "";
        }

        public int GetAchievementsUnlocked()
        {
            return 0;
        }

        public int GetAchievementsTotal()
        {
            return 0;
        }

        public int GetAmmoFromLevel(string barcode)
        {
            return 0;
        }

        public bool GetSavePointValid()
        {
            return false;
        }
        public string GetSavePointLevelBarcode()
        {
            return "";
        }
        public string GetSavePointLevelName()
        {
            return "";
        }


    }
}
