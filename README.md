![IVAI Banner](repo_header.png "IVAI Banner")
# Unity Test Utilities
This repository contains several utility scripts designed to make testing using Unity's Test Framework package easier and the creation of editor scripts easier using the new ui elements easier.

If you have never used the Unity's Testframe work before I suggest you follow through the getting started guide before installing the package:
https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/index.html

Creating a test assembly:
https://docs.unity3d.com/Packages/com.unity.test-framework@1.1/manual/workflow-create-test-assembly.html

If you have never used the UIToolkit you can follow the getting started guide here:
[https://docs.unity3d.com/Manual/UIElements.html](https://docs.unity3d.com/Manual/UIE-simple-ui-toolkit-workflow.html)

Note: The focus on this package is making PlayMode tests which allow users to test scripts in a temporary scene provided by the test launcher. When opening the test runner (Window->General->TestRunner) it will default to EditMode tests, ensure you switch to the Playmode and create a Playmode assembly not an Editmode assembly.

To use the scripts add through package manager (Unity 2021+):
1) Open Package Manager from the Window menu.
2) Press the plus icon
3) Select from "Add package from Git Url"
4) Supply the following url: https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities.git?path=/TestUtilities/Assets/com.ivai.testutilities

## Test Asset Loader
The Test Asset Loader is a static script which provides a simple ways of creating assets for test cases to reduce the amount of set-up code and to reduce the need for wrangling strings. This is an editor script and only works during testing. If you require a runtime way of loading assets consider using the resource folder with Resources.Load or using the Addressables package.

### Note on paths:
By default the class Test Asset loader will look for Prefabs in the path: Assets\Prefabs\[Folder]\[AssetName].prefab and will look for scriptable objects in the path: Assets\Scriptable\[Folder]\[AssetName].asset. In the case you do not follow this convention you can either:
A) Change public variables in the test asset loader
B) Add a file called "TestAssetLoaderSettings" to a root Editor folder. Within this file specify the paths from the asset folder that your project specific files live. The file format is as below:
Prefab Folder: Prefabs
Scriptable Folder: Scriptable
PackageName: com.yourcompany.yourproject
(The asset loader will set the value after the last space to be the given variable).

In the case of the asset being in a subfolder of already set folder, simple provide the folder/subfolder as the argument to the function.

### public static string PrefabFolder 
Base folder/s for prefab Assets.

### public static string ScriptabLeFolder
Base folder/s for prefab Assets.

### public static string PackageFolder
Will add a folder/s between the Assets and prefab or scriptable folders if set in the format Assets\[PackageFolder]\[TypeFolder]...

### public static string GetPackageFolder(string resourceFolder)
Gets the folder that it is expected to have the prefab and scriptable folders in it.

### public static string GetPath(string startFolder, string name, string folder, string fileExtension)
Gets the full path of the expected folder. Can be used to get a path in case the asset you want is within a different part of the project.

### public static GameObject GetPrefab(string name, string folder)
Gets the prefab of the name within the folder (based on the assumptions of folder structure as listed above). If you intend to instanstiate the prefab I suggest using CreatePrefab.

### public static GameObject CreatePrefab(string name, string folder, List<GameObject> testObjects)
Instanstiates the given prefab at position (0,0,0) with quanterion.identity rotation and adds it to a list of testing objects (so they can be destroyed during teardown).

### public static T CreatePrefab<T>(string name, string folder, List<GameObject> testObjects) where T : Component
Instanstiates the given prefab at position (0,0,0) with quanterion.identity rotation and adds it to a list of testing objects (so they can be destroyed during teardown). Afterwards it will try to get the given component in children on the object and return it.
Works for non-monobehaviours components.

### public static T CreateComponentOfType<T>(List<GameObject> testObjects, string name = "Created") where T : Component
Creates a new GameObject at position (0,0,0) with quanterion.identity rotation and attaches the component of type T to the GameObject.
Works for non-monobehaviours components.

### public static T GetScriptable<T>(string name, string folder) where T : ScriptableObject
Gets the scriptable within the folder (based on the assumptions of folder structure as listed above).

### public static void CleanUpObjects(List<GameObject> testObjects)
Destroys a list of GameObjects, reccomeneded to create a list of all testing objects during testing and destroying them during tear down. To avoid lingering objects if transform structure is updates will check up to 10 levels for the parent object to destroy. Clears list on compeletion.

## Test Samples:
If you clone the project and go to Window -> General -> Test Runner and then select the playmode tab and expand all you will see the sample tests provided. Right click on a test such as HelloWorld test and hit open source code to see the given test.

## Inspector Helpers
Includes static functions to add simple elements to a UIElements inspector script allowing inspector scripts to be written in a handful of lines.

### public static VisualTreeAsset GetDefaultTree()
Gets a default visual tree asset from the project. The tree is here: TestUtilities/Assets/com.ivai.testutilities/EditorLayout/BasicEditor.uxml
Use it as a template to create more complex trees (i.e. if you need more than buttons and labels).

### public static void AddButtonWithFunction(VisualElement myInspector, string description, Action function)
Adds a button to the VisualLayout, with the description text which invokes the given function when pressed.

### public static void AddLable(VisualElement myInspector, string description)
Adds a label to the VisualLayout with the description.

### public static void GetAssetsAt<T>(List<T> toFill, string path) where T : UnityEngine.Object
Fills the given list with assets of the type from the asset data base. Useful if you want to assign a lot of assets to scriptable objects. E.g. a scriptable object which holds a list of audio tracks.

### public static void GetAssetsAt<T>(List<T> toFill, string[] paths) where T: UnityEngine.Object
As above except that it accepts multiple asset paths.
