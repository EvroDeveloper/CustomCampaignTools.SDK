using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Campaign Unlocking")]
    public class CampaignUnlocking : MonoBehaviour
    {
        public void UnlockDevTools(bool enableInstantly) { }
        public void UnlockAvatars(bool enableInstantly) { }
        public void UnlockLevel(string barcode) { }
    }
}
