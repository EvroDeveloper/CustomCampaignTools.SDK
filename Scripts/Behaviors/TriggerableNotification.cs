using System;
using System.Collections;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Triggerable Notification")]
    public class TriggerableNotification : MonoBehaviour
    {

        public void SendNotification(string Title, string Message, Texture2D CustomIcon, NotifType Type, float Length)
        {
        }
    }

    public enum NotifType
    {
        Information = 0,
        Warning = 1,
        Error = 2,
        Success = 3,
        CustomIcon = 4
    }
}
