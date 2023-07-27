![IVAI Banner](repo_header.png "IVAI Banner")
# Unity Test Utilities
This repository contains several utility scripts designed to make testing and the creation of editor scripts easier. The repository can be added through the unity package manager with the url: 
You may however want to copy the files into your project if your files are not do match the convention of the package, more details below in Test Asset Loader section.

## Test Asset Loader
The Test Asset Loader is a static script which provides a simple ways of creating assets for test cases to reduce the amount of set-up code and to reduce the need for wrangling strings. This is an editor script and only works during testing. If you require a runtime way of loading assets consider using the resource folder with Resources.Load or using the Addressables package.

### Note on paths:
By default the class Test Asset loader will look for Prefabs in the path: Assets\Prefabs\[Folder]\[AssetName].prefab and will look for scriptable objects in the path: Assets\Scriptable\[Folder]\[AssetName].asset. If the asset is within a subfolder providing the subfolder as the folder argument will work, however if the folder containing scriptable objects \ prefabs is not within the top level of the project or has a different name then the public static strings will need to be modified to get the correct path. This can be done at any time, but it I reccomend either A) copying the file and editing the relevant strings or B) during the SetUp method in testing.
The public static string "PrefabFolder" will change the base folder/s for prefab Assets.
The public static string "ScriptabLeFolder" will change the base folder/s for prefab Assets.
The public static string "PackageFolder" if not length zero will add a folder/s between the prefab or scriptable folders

## Inspector Helpers
Includes static functions to add simple elements to a UIElements inspector script allowing inspector scripts to be written in a handful of lines.
