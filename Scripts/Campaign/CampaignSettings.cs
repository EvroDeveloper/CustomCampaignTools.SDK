namespace CustomCampaignTools.SDK
{
    public class CampaignSettings : ScriptableObject
    {
        public string Name = "New Campaign";
        public LevelCrateReference MainMenu;
        public LevelCrateReference[] MainLevels;
        public LevelCrateReference[] ExtraLevels;
        public LevelCrateReference LoadScene;
        public bool RestrictDevTools = false;
        public bool RestrictAvatar = false;
        public string CampaignAvatar;

        
    }
}