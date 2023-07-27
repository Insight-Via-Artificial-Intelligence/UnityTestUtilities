using System.Collections.Generic;

using UnityEditor;
using UnityEngine;

namespace IVAI.UnityTestUtilities.EditorTesting
{
    public static class TestAssetLoader
    {
        public static string baseFolder = "Assets";
        public static string prefabFolder = "Prefabs";
        public static string scriptableFolder = "Scriptable";
        public static string packageName = "";

        public static string GetPackageFolder(string baseFolder, string resourceFolder)
        {
            if (packageName.Length == 0)
            {
                return $"{baseFolder}/{resourceFolder}";
            }

            return $"{baseFolder}/{packageName}/{resourceFolder}";
        }

        public static string GetPath(string startFolder, string name, string folder, string fileExtension)
        {
            string startPath = GetPackageFolder(baseFolder, startFolder);

            return $"{startPath}/{folder}/{name}.{fileExtension}";
        }

        public static GameObject GetPrefab(string name, string folder)
        {
            string path = GetPath(prefabFolder, name, folder, "prefab");

            GameObject prefab = AssetDatabase.LoadAssetAtPath<GameObject>(path);

            if (!prefab)
            {
                Debug.LogWarning($"Could not find {folder}/{name} prefab at {path}");
            }

            return prefab;
        }

        //Creates given prefab from prefabs folder in resourcesFile.
        public static GameObject CreatePrefab(string name, string folder, List<GameObject> testObjects)
        {
            GameObject prefab = GetPrefab(name, folder);

            if (!prefab)
            {
                return null;
            }

            GameObject instance = GameObject.Instantiate(prefab);

            if (testObjects != null)
            {
                testObjects.Add(instance);
            }
            else
            {
                Debug.LogWarning("Test object list was null!");
            }

            return instance;
        }

        public static T CreateInteractable<T>(string name, string folder, List<GameObject> testObjects) where T : Component
        {
            GameObject instance = CreatePrefab(name, folder, testObjects);

            if (!instance)
            {
                return null;
            }

            T result = instance.GetComponentInChildren<T>();

            return result;
        }

        public static T CreateComponentOfType<T>(List<GameObject> testObjects, string name = "Created") where T : Component
        {
            GameObject gameObject = new GameObject();
            gameObject.name = name;
            gameObject.transform.position = Vector3.zero;

            if (testObjects != null) 
            {
                testObjects.Add(gameObject);
            }
            else
            {
                Debug.LogWarning("Test object list was null!");
            }

            return gameObject.AddComponent<T>();
        }

        public static T GetScriptable<T>(string name, string folder) where T : ScriptableObject
        {
            string path = GetPath(scriptableFolder, name, folder, "asset");

            T result = AssetDatabase.LoadAssetAtPath<T>(path);

            if (!result)
            {
                Debug.LogWarning($"Could not find {folder}/{name} scriptable at {path}");
            }

            return result;
        }

        public static void CleanUpObjects(List<GameObject> testObjects)
        {
            int maxRecursion = 10;
            for (int i = testObjects.Count - 1; i >= 0; i--)
            {
                GameObject toDestroy = testObjects[i];

                if (!toDestroy)
                {
                    continue;
                }

                int recursion = 0;
                while (toDestroy.transform.parent != null && recursion < maxRecursion)
                {
                    recursion++;
                    toDestroy = toDestroy.transform.parent.gameObject;
                }

                GameObject.Destroy(toDestroy);
            }

            testObjects.Clear();
        }

        // Gets a different position for testing objects for each index
        public static Vector3 GetPosition(int positionIndex, float finalModifier = 5.0f)
        {
            int type = positionIndex % 6;

            int modifier = type / 6;

            if (modifier < 1)
            {
                modifier = 1;
            }

            Vector3 basePosition = Vector3.zero;

            switch (type)
            {
                case 0:
                    {
                        basePosition = Vector3.up;
                        break;
                    }
                case 1:
                    {
                        basePosition = Vector3.down;
                        break;
                    }
                case 2:
                    {
                        basePosition = Vector3.right;
                        break;
                    }
                case 3:
                    {
                        basePosition = Vector3.left;
                        break;
                    }
                case 4:
                    {
                        basePosition = Vector3.back;
                        break;
                    }
                case 5:
                    {
                        basePosition = Vector3.forward;
                        break;
                    }
            }

            return basePosition * modifier * finalModifier;
        }

    }
}
