using UnityEngine;
using UltEvents;

namespace CustomCampaignTools.SDK
{
    public class VersionCheck : MonoBehaviour
    {

        public void SetActiveIfGreaterOrEqual(GameObject target, string minVersion, bool active)
        {
        }

        public void InvokeIfGreaterOrEqual(UltEventHolder target, string minVersion)
        {
        }

        public bool IsCurrentVersionGreaterOrEqual(string targetVersion)
        {
            return false;
        }
    }
}
