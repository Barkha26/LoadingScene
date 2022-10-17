using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.Android;
using System.IO;
using System;
using UnityEngine.Networking;
using UnityEngine.UI;

public class LoadScene : MonoBehaviour
{
    public static LoadScene instance;

    [SerializeField] internal static string gameName;
    public string path;
    public static int index = 0;

    private void Awake()
    {
        instance = this;
        DontDestroyOnLoad(this.gameObject);
    }

    public void SetGamePath(string gameName_)
    {
        gameName = gameName_;

        if (File.Exists(Application.persistentDataPath + "/AssetBundles/" + gameName))
        {
            path = Application.persistentDataPath + "/AssetBundles/";// + gameName;
            Debug.LogError("rajni:" + path);
            OpenGameScene(gameName);
        }
        else
        {
            Debug.Log("DownloadBundle");
            StartCoroutine(GetAssetBundle());
            //path = "http://192.168.29.167/AssetBundles/" + gameName;
            //Debug.Log(path);
        }
        //ReadTags.instance.ReadTagsFromJson(gameName);
        //ReadTags.instance.ReadSortLayersFromJson(gameName);
        //path = "http://192.168.29.167/AssetBundles/"; //+ gameName;
        //path = "C:/xampp/htdocs/AssetBundles/" + gameName;
        //path = Application.streamingAssetsPath + "/AssetBundles/"; //+ gameName;
        //path = Application.persistentDataPath + "/AssetBundles/";// + gameName;
        //Debug.LogError("rajni:" + path);
        //OpenGameScene(gameName);
    }

    IEnumerator GetAssetBundle()
    {
        UnityWebRequest www = UnityWebRequest.Get("http://192.168.29.167/AssetBundles/" + gameName);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            string tempPath = Path.Combine(Application.persistentDataPath,"AssetBundles",gameName);
            Debug.Log(tempPath);
            DownloadHandler handle = www.downloadHandler;
            byte[] bytes = handle.data;
            Save(bytes, tempPath);
        }
    }
    void Save(byte[] data, string path_)
    {
        //Create the Directory if it does not exist
        if (!Directory.Exists(Path.GetDirectoryName(path_)))
        {
            Directory.CreateDirectory(Path.GetDirectoryName(path_));
        }

        try
        {
            File.WriteAllBytes(path_, data);
            Debug.Log("Saved Data to: " + path.Replace("/", "\\"));
            SetGamePath(gameName);
        }
        catch (Exception e)
        {
            Debug.LogWarning("Failed To Save Data to: " + path.Replace("/", "\\"));
            Debug.LogWarning("Error: " + e.Message);
        }
    }
    //public void JavaMessage(string message)
    //{
    //    Debug.Log("Unity///: " + message);
    //}

    public string[] loadSceneList;

    public void OpenGameScene(string scenePath)
    {
        OnLoadScene(scenePath);
    }

    public void OnLoadScene(string _scenename)
    {
        Debug.LogError(_scenename);

        var bundleLoadRequest = AssetBundle.LoadFromFileAsync(Path.Combine(path, _scenename));
        var myLoadedAssetBundle = bundleLoadRequest.assetBundle;
        Debug.Log(myLoadedAssetBundle);
        if (myLoadedAssetBundle == null)
        {
            Debug.Log("Failed to load AssetBundle!");
            return;
        }

        loadSceneList = myLoadedAssetBundle.GetAllScenePaths();
        for (int i = 0; i < loadSceneList.Length; i++)
        {
            Debug.Log(loadSceneList[i].ToString());
        }

        Debug.Log(loadSceneList.Length);
        Open();
        //StartCoroutine(GetAssetBundle(_scenename));
    }

    IEnumerator GetAssetBundle(string _scenename)
    {
        UnityWebRequest www = UnityWebRequestAssetBundle.GetAssetBundle(path + "/" + _scenename);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.Log(www.error);
        }
        else
        {
            AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(www);
            loadSceneList = bundle.GetAllScenePaths();
            for (int i = 0; i < loadSceneList.Length; i++)
            {
                Debug.Log(loadSceneList[i].ToString());
            }
        }
        Debug.Log(loadSceneList.Length);
        Open();
    }


    public void Open()
    {
        SceneManager.LoadScene(loadSceneList[index]);
        index++;
    }

    public void PlayBtn(string sceneName)
    {
        //OpenGameScene(sceneName);
        Debug.Log(index);
        SceneManager.LoadScene(loadSceneList[index]);
        //index++;
    }

}