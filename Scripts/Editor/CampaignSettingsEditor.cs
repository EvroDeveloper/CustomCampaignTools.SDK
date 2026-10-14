using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using CustomCampaignTools.SDK;
using CustomCampaignTools;

[CustomEditor(typeof(CampaignSettings))]
public class CampaignSettingsEditor : Editor
{
    VisualElement Restrict;
    VisualElement Whitelist;
    VisualElement StatRange;
    Toggle BodylogToggle;
    Button ExportButton;
    Label ExportedMessage;

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
        StatRange = tree.Q<VisualElement>("RestrictionType-Stats");
        BodylogToggle = tree.Q<Toggle>("DisableBodyLog");

        AvatarRestriction.RegisterValueChangedCallback(choice => OnRestrictionChoiceChanged(choice.newValue));

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
        else if (target.AvatarRestriction.HasFlag(AvatarRestrictionType.EnforceStatRange))
        {
            AvatarRestriction.index = 3;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[3]);
        }
        else
        {
            AvatarRestriction.index = 0;
            target.AvatarRestriction = AvatarRestrictionType.None;
            OnRestrictionChoiceChanged(AvatarRestriction.choices[0]);
        }

        ExportButton = tree.Q<Button>("ExportButton");
        ExportedMessage = tree.Q<Label>("ExportComplete");
        ExportButton.RegisterCallback<MouseUpEvent>((evt) =>
        {
            target.SaveCampaignJson();
            ExportedMessage.style.display = DisplayStyle.Flex;
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
                StatRange.style.display = DisplayStyle.None;
                BodylogToggle.style.display = DisplayStyle.None;
                break;
            case ("Restrict Avatar"):
                target.AvatarRestriction = AvatarRestrictionType.RestrictAvatar;
                Restrict.style.display = DisplayStyle.Flex;
                Whitelist.style.display = DisplayStyle.None;
                StatRange.style.display = DisplayStyle.None;
                BodylogToggle.style.display = DisplayStyle.Flex;
                break;
            case ("Avatar Whitelist"):
                target.AvatarRestriction = AvatarRestrictionType.EnforceWhitelist;
                Restrict.style.display = DisplayStyle.None;
                Whitelist.style.display = DisplayStyle.Flex;
                StatRange.style.display = DisplayStyle.None;
                BodylogToggle.style.display = DisplayStyle.Flex;
                break;
            case ("Avatar Stat Range"):
                target.AvatarRestriction = AvatarRestrictionType.EnforceStatRange;
                Restrict.style.display = DisplayStyle.None;
                Whitelist.style.display = DisplayStyle.None;
                StatRange.style.display = DisplayStyle.Flex;
                BodylogToggle.style.display = DisplayStyle.Flex;
                break;
        }
    }
}
