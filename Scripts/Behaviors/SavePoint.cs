using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Saving/Save Point")]
    public class SavePoint : MonoBehaviour
    {
        [Tooltip("If set, the player will spawn at this transform when loading from this save point. If not set, the player will spawn at this object's transform.")]
        public Transform playerSpawnPoint;

        [Tooltip("If set, all entities within this box collider will be saved and restored on load.")]
        public BoxCollider entitySaveZone;
        public void Save()
        {
        }

        public void ClearSave()
        {
        }
    }
}
