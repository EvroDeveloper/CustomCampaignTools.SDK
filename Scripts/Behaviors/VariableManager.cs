using UltEvents;
using System;
using System.Collections;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/UltEvent Utilities/Variable Manager")]
    public class VariableManager : MonoBehaviour
    {

        public void SetValue(string key, float value)
        {
        }
        public void IncrementValue(string key, float value)
        {
        }
        public float GetValue(string key)
        {
            return 0f;
        }
        public void InvokeIf(string key, ComparisonType comparison, float compareValue, UltEventHolder ultEvent)
        {
        }
    }

    public enum ComparisonType
    {
        Equal,
        NotEqual,
        GreaterThan,
        GreaterThanOrEqual,
        LessThan,
        LessThanOrEqual,
    }
}
