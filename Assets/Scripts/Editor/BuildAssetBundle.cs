using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using System.IO;

public class BuildAssetBundle {

    [MenuItem("AssetBundleTools/BuildAllAssetBundles")]
    public static void buildAllAssetBundle()
    {
        string outPath = string.Empty;
        outPath = Application.streamingAssetsPath;
        if(!Directory.Exists(outPath))
        {
            Directory.CreateDirectory(outPath);
        }
        BuildPipeline.BuildAssetBundles(outPath, BuildAssetBundleOptions.None, BuildTarget.StandaloneWindows64);
    }
}
