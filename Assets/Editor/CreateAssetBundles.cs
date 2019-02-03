using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class CreateAssetBundles : EditorWindow
{

    [MenuItem("Assets/Build AssetBundles")]
    public static void BuildAllAssetBundles() 
    {
        BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.ChunkBasedCompression, BuildTarget.Android);
        //BuildPipeline.BuildAssetBundles("Assets/AssetBundles", BuildAssetBundleOptions.None, BuildTarget.WSAPlayer);
        AssetDatabase.Refresh();
    }

    //[MenuItem("Assets/Save Asset to Bundle")]
    //public static void SaveSpecificAssets()
    //{
    //    AssetBundleBuild build = new AssetBundleBuild
    //    {
    //        assetBundleName = "primitiveBundle"
    //    };
    //    string[] files = AssetDatabase.GetAllAssetPaths();
    //    List<string> validFiles = new List<string>();
    //    foreach (string file in files)
    //    {
    //        if (file.EndsWith(".png")) validFiles.ToArray();
    //    }
    //    build.assetNames = validFiles.ToArray();
    //    BuildPipeline.BuildAssetBundles("Assets/AssetBundles", new AssetBundleBuild[1] { build }, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows);

    //}
}