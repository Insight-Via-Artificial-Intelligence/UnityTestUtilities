// Copyright (c) 2023 Insight Via Artificial Intelligence
// This file is licensed under the MIT License.
// License text available at https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities/blob/main/LICENSE

using System;
using System.Collections.Generic;

using UnityEditor;

using UnityEngine;
using UnityEngine.UIElements;

namespace IVAI.EditorUtilities.InspectorEditor
{

    public static class InspectorHelpers
    {
        public static VisualTreeAsset GetDefaultTree()
        {
            VisualTreeAsset visualTreeAsset = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Packages/com.ivai.testutilities/EditorLayout/BasicEditor.uxml");

            if (visualTreeAsset)
            {
                return visualTreeAsset;
            }

            return AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/com.ivai.testutilities/EditorLayout/BasicEditor.uxml");
        }

        public static void AddButtonWithFunction(VisualElement myInspector, string description, Action function)
        {
            Button button = new Button();
            button.name = description;
            button.text = description;
            myInspector.Add(button);

            button.clicked += function;
        }

        public static void AddLable(VisualElement myInspector, string description)
        {
            Label label = new Label();
            label.text = description;
            label.name = description;
            myInspector.Add(label);
        }

        public static void GetAssetsAt<T>(List<T> toFill, string path) where T : UnityEngine.Object
        {
            string[] paths = new string[1];
            paths[0] = path;
            GetAssetsAt(toFill, paths);
        }

        public static void GetAssetsAt<T>(List<T> toFill, string[] paths) where T: UnityEngine.Object
        {
            if (toFill == null)
            {
                Debug.LogWarning("To fill was null!");
                return;
            }

            string[] allFiles = UnityEditor.AssetDatabase.FindAssets("t:audioclip", paths);

            Debug.Log(allFiles.Length);

            foreach (string guid in allFiles)
            {
                string path = UnityEditor.AssetDatabase.GUIDToAssetPath(guid);

                T clip = (T)UnityEditor.AssetDatabase.LoadAssetAtPath(path, typeof(AudioClip));

                toFill.Add(clip);
            }
        }
    }
}