using System;
using UnityEngine;
using SLZ.Marrow.Warehouse;

namespace CustomCampaignTools.SDK
{
    [RequireComponent(typeof(CrateSpawner))]
    public class SpawnerDespawnSaver : MonoBehaviour
    {
        public void Setup(CrateSpawner c, GameObject g) { }

        // void Reset()
        // {
        //     CrateSpawner spawner = GetComponent<CrateSpawner>();
        //     spawner.OnSpawnEvent.
        // }
    }
}