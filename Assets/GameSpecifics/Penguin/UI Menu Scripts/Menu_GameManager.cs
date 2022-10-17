using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu_GameManager : MonoBehaviour {

    public static Menu_GameManager instance;
    public GameObject UI;
    public GameObject levelComplete;
    public GameObject gameOver;
    public GameObject gamePause;
    public GameObject loading;
    public List<string> gameScenes;
    static int index = 0;

    const string pluginName="com.bng.mobibattle.bridge.CommunicationBridge";
    static AndroidJavaClass _pluginClass;
    static AndroidJavaObject _pluginInstance;

public static AndroidJavaClass PluginClass
{
    get{
        if(_pluginClass==null)
        {
            _pluginClass=new AndroidJavaClass(pluginName);
        }
        return _pluginClass;
    }
}

public static AndroidJavaObject PluginInstance{

    get{
        if(_pluginInstance==null)
        {
_pluginInstance=PluginClass.CallStatic<AndroidJavaObject>("getInstance");
        }
        return _pluginInstance;
    }
}

  double gameEnd()
{
    if(Application.platform==RuntimePlatform.Android)
    return PluginInstance.Call<double>("gameEnd");
    Debug.LogWarning("Wrongend  Platform");
    return 0;


}

	void Awake () {
        instance = this;
        UI.SetActive(true);
        levelComplete.SetActive(false);
        gameOver.SetActive(false);
        gamePause.SetActive(false);
        loading.SetActive(false);
        gameScenes.Add("W1-1");
        gameScenes.Add("W1-2");
        gameScenes.Add("W1-3");
        gameScenes.Add("w2-1");
	}


	/*
    public void Pause()
    {
        if(Time.timeScale == 1)
        {
            gamePause.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            gamePause.SetActive(false);
            Time.timeScale = 1;
        }
    }
*/
    public void Restart()
    {
        Time.timeScale = 1;
        loading.SetActive(true);
        //LoadScene.instance.OnLoadScene(gameScenes[index]);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //LoadScene.instance.OnLoadScene(LoadScene.instance.loadSceneList[0]);
        SceneManager.LoadScene(LoadScene.instance.loadSceneList[LoadScene.index]);
    }

    public void MainMenu()
    {
        Time.timeScale = 1;
        loading.SetActive(true);
        //SceneManager.LoadScene("MainMenu");
        //LoadScene.instance.LoadMaterial();
        //LoadScene.instance.PlayBtn("MainMenu");
        //LoadScene.instance.OnLoadScene("MainMenu");
        //LoadScene.instance.OnLoadScene(LoadScene.instance.loadSceneList[LoadScene.index]);
        SceneManager.LoadScene(LoadScene.instance.loadSceneList[LoadScene.index]);
    }
	
    public void ShowGameOver()
    {
        Debug.Log("Show Elapsed Game End :"+gameEnd());
        StartCoroutine(ShowMenu(1, gameOver));
    }

    public void ShowLevelComplete()
    {
        StartCoroutine(ShowMenu(1, levelComplete));
    }


    public void NextLevel()
    {
        index += 1;
        loading.SetActive(true);
        GlobalValue.levelPlaying++;
        //LoadScene.instance.OnLoadScene(LoadScene.instance.loadSceneList[LoadScene.index]);
        LoadScene.index++;
        SceneManager.LoadScene(LoadScene.instance.loadSceneList[LoadScene.index]);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    IEnumerator ShowMenu(float time, GameObject Menu)
    {
        yield return new WaitForSeconds(time);
        Menu.SetActive(true);
    }

}
