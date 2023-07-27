![IVAI Banner](repo_header.png "IVAI Banner")
# Unity Test Utilities
This repository contains several utility scripts designed to make testing and the creation of editor scripts easier. The repository can be added through the unity package manager. 

NOTE: You may however want to copy the files into your project if your file structures do match the convention of the package (or you don't want to set the package every test), more details below in Test Asset Loader section.

To instal through package manager (Unity 2021+):
1) Open Package Manager by going from the Window menu.
2) Press the plus icon
3) Select from "Add package from Git Url"
4) Supply the following url: https://github.com/Insight-Via-Artificial-Intelligence/UnityTestUtilities.git?path=/TestUtilities/Assets/com.ivai.testutilities

## Test Asset Loader
The Test Asset Loader is a static script which provides a simple ways of creating assets for test cases to reduce the amount of set-up code and to reduce the need for wrangling strings. This is an editor script and only works during testing. If you require a runtime way of loading assets consider using the resource folder with Resources.Load or using the Addressables package.

### Note on paths:
By default the class Test Asset loader will look for Prefabs in the path: Assets\Prefabs\[Folder]\[AssetName].prefab and will look for scriptable objects in the path: Assets\Scriptable\[Folder]\[AssetName].asset. If the asset is within a subfolder providing the subfolder as the folder argument will work, however if the folder containing scriptable objects \ prefabs is not within the top level of the project or has a different name then the public static strings will need to be modified to get the correct path. This can be done at any time, but it I reccomend either A) copying the file and editing the relevant strings or B) during the SetUp method in testing.

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

## Inspector Helpers
Includes static functions to add simple elements to a UIElements inspector script allowing inspector scripts to be written in a handful of lines.

### public static void AddButtonWithFunction(VisualElement myInspector, string description, Action function)
Adds a button to the VisualLayout, with the description text which invokes the given function when pressed.

### public static void AddLable(VisualElement myInspector, string description)
Adds a label to the VisualLayout with the description.
