using System.Collections.Generic;

namespace CustomCampaignTools.SDK
{
    internal class SerializedLevelSetup
    {
        public string levelBarcode;
        public string levelName;

#if UNITY_EDITOR
        public static List<SerializedLevelSetup> CopyFrom(LevelSetup[] levelSetups)
        {
            List<SerializedLevelSetup> list = new();
            foreach (var level in levelSetups)
            {
                list.Add(level.Serialize());
            }
            return list;
        }
#endif
    }
}
