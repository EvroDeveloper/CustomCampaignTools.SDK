using TMPro;
using System;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Timer Display")]
    public class TimerDisplay : MonoBehaviour
    {

        public TMPro.TMP_Text textDisplay;
        public TimerDisplayType displayType;
        public int serializedTimerDisplayType;
        public TrialTimeDisplayType trialDisplayType;
        public int serializedTrialDisplayType;

        [Tooltip("Always update the Timer Display. May be laggy perhaps, i wouldnt recommend. but its an option ig")]
        public bool alwaysUpdate = false;

        [Tooltip("Displays Hours (if greater than zero)")]
        public bool displayHours = true;

        [Tooltip("Displays Minutes")]
        public bool displayMinutes = true;

        [Tooltip("Displays Seconds")]
        public bool displaySeconds = true;

        [Tooltip("Displays milliseconds (if applicable)")]
        public bool displayMilliseconds = true;

        [Tooltip("Number of decimal places to show for Milliseconds")]
        public int millisecondsDepth = 2;

        public void OnValidate()
        {
            serializedTimerDisplayType = (int)displayType;
            serializedTrialDisplayType = (int)trialDisplayType;
        }

        public void UpdateText()
        {
        }
        
        public void SetTargetLevel(string barcode)
        {
        }
        
        public void SetTargetTrial(string trialKey)
        {
        }

        public void SetTextPrefix(string prefix)
        {
        }

        public void SetTextPostfix(string postfix)
        {
        }

    }

    public enum TimerDisplayType
    {
        TotalCampaignTime,
        LevelTime,
        TrialTime,
    }

    public enum TrialTimeDisplayType
    {
        Best,
        Latest,
        Average
    }
}
