// Copyright (c) 2023 Insight Via Artificial Intelligence
// This file is licensed under the MIT License.
// License text available at https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities/blob/main/LICENSE

using System;

using UnityEngine.UIElements;

namespace IVAI.EditorUtilities.InspectorEditor
{

    public static class InspectorHelpers
    {
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
    }
}