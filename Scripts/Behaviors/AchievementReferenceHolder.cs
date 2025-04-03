using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Reference Holder")]
    public class AchievementReferenceHolder : MonoBehaviour
    {
        public Image achievementIcon;
        public TMP_Text titleTMP;
        public TMP_Text descriptionTMP;
    }
}
