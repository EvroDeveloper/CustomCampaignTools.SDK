using System;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Saving/GameObject Enabled Saver")]
    public class ObjectEnabledSaver : MonoBehaviour
    {
        [Tooltip("A unique ID for this object. Used to identify it in save data. A random ID will be assigned on Reset().")]
        public int uniqueID;

#if UNITY_EDITOR
        void Reset()
        {
            uniqueID = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }
#endif
    }
}
