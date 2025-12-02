using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;

namespace CustomCampaignTools.SDK.Editor
{
    [CustomEditor(typeof(SavePoint))]
    public class SavePointEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("05f4d4f55d43d4afc8b506d9f8f0162f");
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
            VisualElement tree = visualTree.Instantiate();

            root.Add(tree);

            return root;
        }
    }
}
