using UltEvents;
using UnityEngine;
using System;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Version Check")]
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
