using System;
using UnityEngine;
using SLZ.Marrow.Warehouse;
using UltEvents;
using System.Reflection;
using UnityEditor;

namespace CustomCampaignTools.SDK
{
    [AddComponentMenu("CustomCampaignTools/Saving/Spawner Despawn Saver")]
    [RequireComponent(typeof(CrateSpawner))]
    public class SpawnerDespawnSaver : MonoBehaviour
    {
        public void Setup(CrateSpawner c, GameObject g) { }

#if UNITY_EDITOR
        void Reset()
        {
            CrateSpawner spawner = GetComponent<CrateSpawner>();
            var call = spawner.onSpawnEvent.AddPersistentCall((Action<CrateSpawner, GameObject>)Setup);

            typeof(PersistentArgument).GetField("_Type", UltEventUtils.AnyAccessBindings).SetValue(call.PersistentArguments[0], PersistentArgumentType.Parameter);
            call.PersistentArguments[0].Int = 0;
            typeof(PersistentArgument).GetField("_Type", UltEventUtils.AnyAccessBindings).SetValue(call.PersistentArguments[1], PersistentArgumentType.Parameter);
            call.PersistentArguments[1].Int = 1;
        }
#endif
    }
}