using System.Collections;
using System.Collections.Generic;

using UnityEditor;
using UnityEditor.UIElements;

using UnityEngine;
using UnityEngine.UIElements;

using IVAI.EditorUtilities.InspectorEditor;
using IVAI.TestableSample.Runtime;

[CustomEditor(typeof(SampleUnit))]
public class SampleUnitEditor : Editor
{
    // We can set the default visual tree as a default asset reference
    // Does not have to be one provided in this sample
    public VisualTreeAsset inspectorXML = null;

    // The object we are editing
    private SampleUnit sampleUnit = null;

    public void OnEnable()
    {
        sampleUnit = (SampleUnit)target;

        // Get a simple tree from a folder in the project
        // Required to get the get the default inspector property
        if (!inspectorXML)
        {
            // Function handles whether or not it is in a package
            inspectorXML = InspectorHelpers.GetDefaultTree();
        }
    }

    public override VisualElement CreateInspectorGUI()
    {
        // Create a new VisualElement to be the root of our inspector UI
        VisualElement mainInspector = new VisualElement();

        // Clone the tree from the template
        inspectorXML.CloneTree(mainInspector);

        // Get the defualt inspector from the tree
        // If using a custom tree ensure you have a default inspector property
        VisualElement defaultInspector = mainInspector.Q("Default_Inspector");

        // Attach a default inspector to the tree
        InspectorElement.FillDefaultInspector(defaultInspector, serializedObject, this);

        // Adds a lable to the inspector
        InspectorHelpers.AddLable(mainInspector, "An example lable");

        // Add a button which calls the function when clicked
        InspectorHelpers.AddButtonWithFunction(mainInspector, "Set a random number in script", SampleUnitEditorFunction);

        // Adds a button below with a different function
        InspectorHelpers.AddButtonWithFunction(mainInspector, "Set a random number in all scripts", SampleUnitAllInSceneEditorFunction);

        return mainInspector;
    }

    // A function is set to be called above
    public void SampleUnitEditorFunction()
    {
        SampleUnitEditorFunction(sampleUnit);
    }

    // You can directly edit the targetted object
    // Has an argument so it can be reused on every object in scene below
    public void SampleUnitEditorFunction(SampleUnit toSet)
    {
        // Random number min inclusive, max exclusive
        int randomNumber = Random.Range(0, 101);

        // Calls a fuction which changes a value
        toSet.SetChangeMe(randomNumber);

        // Sets the script to be dirty so it saves
        EditorUtility.SetDirty(toSet);
    }

    // Call editor function on all objects in the scene
    public void SampleUnitAllInSceneEditorFunction()
    {
        SampleUnit[] unitArray = GameObject.FindObjectsOfType<SampleUnit>(true);

        foreach (SampleUnit highlighter in unitArray)
        {
            SampleUnitEditorFunction(highlighter);
        }
    }
}
