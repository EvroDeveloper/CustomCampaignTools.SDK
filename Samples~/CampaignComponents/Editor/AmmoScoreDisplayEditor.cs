using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;

namespace CustomCampaignTools.SDK.Editor
{
    [CustomEditor(typeof(AmmoScoreDisplay))]
    public class AmmoScoreDisplayEditor : UnityEditor.Editor
    {
        public override VisualElement CreateInspectorGUI()
        {
            VisualElement root = new VisualElement();

            string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("546ac36735cd24395b56f9e69ed99e6e");
            VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
            VisualElement tree = visualTree.Instantiate();

            root.Add(tree);

            return root;
        }
    }
}
