using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;

namespace CustomCampaignTools.SDK.Editor
{
    [CustomEditor(typeof(InvokeInCampaign))]
    public class InvokeInCampaignEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("affed6f1bc46247269c1758a9b45b17d");
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
            VisualElement tree = visualTree.Instantiate();

            root.Add(tree);

            return root;
        }
    }
}
