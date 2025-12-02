using TMPro;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Display")]
    public class AchievementDisplay : MonoBehaviour
    {
        public Button nextButton;
        public Button backButton;
        public TMP_Text pageText;
        public TMP_Text unlockCount;

        public void Awake()
        {
        }

        public void Activate()
        {
        }

        public void NextPage()
        {
        }
        public void PrevPage()
        {
        }

        public void SetUnlockTextFormat(string format)
        {
        }
    }
}
