using TMPro;

using UnityEngine;
using UnityEngine.UI;
using System;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Reference Holder")]
    public class AchievementReferenceHolder : MonoBehaviour
    {
        [Tooltip("Image to show the achievement's Icon.")]
        public Image achievementIcon;
        
        [Tooltip("Text to show the achievement's title.")]
        public TMP_Text titleTMP;

        [Tooltip("Text to show the achievement's description.")]
        public TMP_Text descriptionTMP;

        [Tooltip("Optional icon to show when the achievement is locked.")]
        public GameObject lockIcon;
    }
}
