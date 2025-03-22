using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;
using Unity.Plastic.Antlr3.Runtime.Tree;
using WebSocketSharp;

[CustomEditor(typeof(CampaignSettings))]
public class CampaignSettingsEditor : Editor
{
    VisualElement Restrict;
    VisualElement Whitelist;
    Toggle BodylogToggle;
    Button ExportButton;

    new CampaignSettings target;

    public override VisualElement CreateInspectorGUI()
    {
        target = base.target as CampaignSettings;

        // Create a root VisualElement for the inspector
        VisualElement root = new VisualElement();

        string VISUALTREE_PATH = AssetDatabase.GUIDToAssetPath("69462c375a6f01540bd1b90de68d5e2e");
        VisualTreeAsset visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(VISUALTREE_PATH);
        VisualElement tree = visualTree.Instantiate();

        DropdownField AvatarRestriction = tree.Q<DropdownField>("AvatarRestriction");
        Restrict = tree.Q<VisualElement>("RestrictionType-Restrict");
        Whitelist = tree.Q<VisualElement>("RestrictionType-Whitelist");
        BodylogToggle = tree.Q<Toggle>("DisableBodyLog");

        AvatarRestriction.RegisterValueChangedCallback(choice => OnRestrictionChoiceChanged(choice.newValue));

        if (target.AvatarRestriction.HasFlag(AvatarRestrictionType.DisableBodyLog))
        {
            BodylogToggle.value = true;
        }

        if (target.AvatarRestriction == AvatarRestrictionType.None)
        {
            AvatarRestriction.index = 0;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[0]);
        }
        else if (target.AvatarRestriction.HasFlag(AvatarRestrictionType.EnforceWhitelist))
        {
            AvatarRestriction.index = 2;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[2]);
        }
        else if (target.AvatarRestriction.HasFlag(AvatarRestrictionType.RestrictAvatar))
        {
            AvatarRestriction.index = 1;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[1]);
        }
        else
        {
            AvatarRestriction.index = 0;
            target.AvatarRestriction = AvatarRestrictionType.None;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[0]);
        }

        BodylogToggle.RegisterCallback<ChangeEvent<bool>>((evt) =>
        {
            if (evt.newValue)
            {
                target.AvatarRestriction |= AvatarRestrictionType.DisableBodyLog;
            }
            else
            {
                target.AvatarRestriction &= ~AvatarRestrictionType.DisableBodyLog;
            }
        });

        ExportButton = tree.Q<Button>("ExportButton");
        ExportButton.RegisterCallback<MouseUpEvent>((evt) =>
        {
            target.SaveCampaignJson();
        });

        root.Add(tree);

        return root;
    }

    public void OnRestrictionChoiceChanged(string choice)
    {
        switch(choice)
        {
            case("None"):
                target.AvatarRestriction = AvatarRestrictionType.None;
                Restrict.style.display = DisplayStyle.None;
                Whitelist.style.display = DisplayStyle.None;
                BodylogToggle.style.display = DisplayStyle.None;
                break;
            case ("Restrict Avatar"):
                target.AvatarRestriction = AvatarRestrictionType.RestrictAvatar;
                Restrict.style.display = DisplayStyle.Flex;
                Whitelist.style.display = DisplayStyle.None;
                BodylogToggle.style.display = DisplayStyle.Flex;
                break;
            case ("Avatar Whitelist"):
                target.AvatarRestriction = AvatarRestrictionType.EnforceWhitelist;
                Restrict.style.display = DisplayStyle.None;
                Whitelist.style.display = DisplayStyle.Flex;
                BodylogToggle.style.display = DisplayStyle.Flex;
                break;
        }


        if (BodylogToggle.value)
        {
            target.AvatarRestriction |= AvatarRestrictionType.DisableBodyLog;
        }
        else
        {
            target.AvatarRestriction &= ~AvatarRestrictionType.DisableBodyLog;
        }
    }
}