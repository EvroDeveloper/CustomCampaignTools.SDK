using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;

namespace CustomCampaignTools.SDK.Editor
{
    [CustomEditor(typeof(SpawnerDespawnSaver))]
    public class SpawnerDespawnSaverEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("b0b44eae2f34c45c9bd822a66ff92756");
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
            VisualElement tree = visualTree.Instantiate();

            Button resetButton = tree.Q<Button>("ResetButton");
            resetButton.clicked += () =>
            {
                (target as SpawnerDespawnSaver).Reset();
            };

            root.Add(tree);

            return root;
        }
    }
}
