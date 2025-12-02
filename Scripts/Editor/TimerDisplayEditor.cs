using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;

namespace CustomCampaignTools.SDK.Editor
{
    [CustomEditor(typeof(TimerDisplay))]
    public class TimerDisplayEditor : UnityEditor.Editor
    {
        SerializedProperty overallTypeProperty;
        PropertyField overallType;
        PropertyField trialType;
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("71d05bb6071e047ca8adc8d3697ac944");
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
            VisualElement tree = visualTree.Instantiate();

            SerializedObject serObj = new SerializedObject(target);
            overallTypeProperty = serObj.FindProperty("displayType");

            overallType = tree.Q<PropertyField>("displayType");
            trialType = tree.Q<PropertyField>("trialDisplayType");
            overallType.RegisterValueChangeCallback((evt) =>
            {
                UpdateTrialSelector(evt.changedProperty);
            });
            UpdateTrialSelector(overallTypeProperty);

            root.Add(tree);
            return root;
        }
        
        void UpdateTrialSelector(SerializedProperty prop)
        {
            trialType.style.display = prop.enumValueIndex == 2 ? DisplayStyle.Flex : DisplayStyle.None;
        }
    }
}
