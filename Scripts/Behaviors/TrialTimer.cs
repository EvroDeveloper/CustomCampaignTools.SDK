using UnityEngine;
using System;
using System.Collections.Generic;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Trial Timer")]
    public class TrialTimer : MonoBehaviour
    {
        public void StartTimer(string trialKey)
        {
        }

        public float EndTimer(string trialKey)
        {
            return 0f;
        }
        
        public bool EndTimer_IsBest(string trialKey)
        {
            return false;
        }

        public void PauseTimer(string trialKey)
        {
        }

        public void ResumeTimer(string trialKey)
        {
        }
    }
}
