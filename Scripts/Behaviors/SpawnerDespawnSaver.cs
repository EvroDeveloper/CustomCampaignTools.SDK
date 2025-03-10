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
            SerializedObject serializedObject = new SerializedObject(spawner);

            SerializedProperty persistentCallsProp = serializedObject.FindProperty("onSpawnEvent._PersistentCalls");

            if (persistentCallsProp.arraySize > 0)
            {
                SerializedProperty lastCallProp = persistentCallsProp.GetArrayElementAtIndex(persistentCallsProp.arraySize - 1);
                SerializedProperty argumentsProp = lastCallProp.FindPropertyRelative("PersistentArguments");

                if (argumentsProp != null && argumentsProp.arraySize >= 2)
                {
                    // Modify arguments using SerializedProperties
                    SerializedProperty firstArg = argumentsProp.GetArrayElementAtIndex(0);
                    SerializedProperty secondArg = argumentsProp.GetArrayElementAtIndex(1);

                    firstArg.FindPropertyRelative("Type").enumValueIndex = (int)PersistentArgumentType.Parameter; // Parameter mode (likely index 2)
                    firstArg.FindPropertyRelative("ParameterIndex").intValue = 0;

                    secondArg.FindPropertyRelative("Type").enumValueIndex = (int)PersistentArgumentType.Parameter;
                    secondArg.FindPropertyRelative("ParameterIndex").intValue = 1;
                }
            }
        }
#endif
    }
}