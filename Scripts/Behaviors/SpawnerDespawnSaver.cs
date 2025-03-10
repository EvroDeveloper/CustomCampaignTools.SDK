using System;
using UnityEngine;
using SLZ.Marrow.Warehouse;
using UltEvents;
using System.Reflection;

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
            SetArgumentToParameter(call.PersistentArguments[0], 0);
            SetArgumentToParameter(call.PersistentArguments[1], 1);
        }

        private void SetArgumentToParameter(PersistentArgument argument, int parameterIndex)
        {
            if (argument == null) return;

            // Find and set the "Mode" field to "Parameter"
            FieldInfo modeField = typeof(PersistentArgument).GetField(nameof(PersistentArgument.Type), BindingFlags.Instance | BindingFlags.NonPublic);
            if (modeField != null)
            {
                modeField.SetValue(argument, PersistentArgumentType.Parameter);
            }

            // Set the ParameterIndex field
            FieldInfo indexField = typeof(PersistentArgument).GetField(nameof(PersistentArgument.ParameterIndex), BindingFlags.Instance | BindingFlags.Public);
            if (indexField != null)
            {
                indexField.SetValue(argument, parameterIndex);
            }
        }
#endif
    }
}