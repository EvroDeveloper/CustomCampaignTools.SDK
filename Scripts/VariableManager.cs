using UnityEngine;

namespace CustomCampaignTools.SDK
{
    public class VariableManager : MonoBehaviour
    {
        public void SetValue(string key, float value) { }
        public void IncrementValue(string key, float value) { }
        public float GetValue(string key) { }
        public void InvokeIf(string key, ComparisonType comparison, float compareValue, UltEventHolder ultEvent) { }
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