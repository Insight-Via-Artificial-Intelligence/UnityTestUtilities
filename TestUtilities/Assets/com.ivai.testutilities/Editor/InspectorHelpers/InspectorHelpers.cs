using System;

using UnityEngine.UIElements;

namespace IVAI.InspectorHelpers
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