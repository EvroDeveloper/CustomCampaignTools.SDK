using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Achievements/Achievement Display")]
    public class AchievementDisplay : MonoBehaviour
    {
        private Button nextButton;
        private Button backButton;
        private TMP_Text pageText;
        private TMP_Text unlockCount;
        
        public void Activate() { }
    }
}
