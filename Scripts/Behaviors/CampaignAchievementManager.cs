using System;
using System.Collections;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Manager")]
    public class CampaignAchievementManager : MonoBehaviour
    {

        public bool UnlockAchievement(string Key)
        {
            return false;
        }

        public void RelockAchievement(string Key)
        {
        }

        public bool IsAchievementUnlocked(string Key)
        {
            return false;
        }

        public int GetAchievementsTotal()
        {
            return 0;
        }

        public int GetAchievementsCompleted()
        {
            return 0;
        }

        public float GetAchievementsCompletedPercentage()
        {
            return 0f;
        }
    }
}
