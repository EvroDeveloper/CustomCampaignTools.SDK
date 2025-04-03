using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Display")]
    public class AchievementDisplay : MonoBehaviour
    {
        private GameObject nextButton;
        private GameObject backButton;
        private TMP_Text pageText;
        private TMP_Text unlockCount;
        
        public void Activate() { }
    }
}
