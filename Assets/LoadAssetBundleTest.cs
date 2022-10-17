using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class LoadAssetBundleTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        AssetBundle localAssetBundle = AssetBundle.LoadFromFile(Path.Combine(Application.streamingAssetsPath, "testasset"));

        if (localAssetBundle == null)
        {
            Debug.LogError("Failed to load AssetBundle!");
            return;
        }

        GameObject asset = localAssetBundle.LoadAsset<GameObject>("Square");
        Instantiate(asset);
        localAssetBundle.Unload(false);

        if(asset.tag == "TestTag")
        {
            Debug.Log("true");
        }
        else
        {
            Debug.Log("false");
        }
    }

   
}
