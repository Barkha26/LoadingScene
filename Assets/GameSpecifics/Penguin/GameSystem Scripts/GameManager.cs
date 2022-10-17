using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public bool isFinishWorld;

    public int star1 = 50;
    public int star2 = 100;
    public int star3 = 150;

    public static GameManager instance;

    public AudioClip soundSuccess, soundFail;


    const string pluginName = "com.bng.mobibattle.bridge.CommunicationBridge";
    static AndroidJavaClass _pluginClass;
    //static AndroidJavaClass=_pluginClass;
    static AndroidJavaObject _pluginInstance;
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(pluginName);
            }
            return _pluginClass;
        }
    }

    public static AndroidJavaObject PluginInstance
    {

        get
        {
            if (_pluginInstance == null)
            {
                _pluginInstance = PluginClass.CallStatic<AndroidJavaObject>("getInstance");
            }
            return _pluginInstance;
        }
    }

    double gameEnd()
    {
        if (Application.platform == RuntimePlatform.Android)
            return PluginInstance.Call<double>("gameEnd");
        Debug.LogWarning("Wrongend  Platform");
        return 0;


    }
    double gameStart()
    {
        if (Application.platform == RuntimePlatform.Android)
            return PluginInstance.Call<double>("gameStart");
        Debug.LogWarning("Wrong Platform");
        return 0;


    }
    public enum GameState
    {
        Menu, Playing, Pause
    }
    [HideInInspector]
    public GameState state;

    private int score = 0;
    private int stars = 0;
    private int hearts = 3;

    public static int Score
    {
        get { return instance.score; }
        set { instance.score = value; }
    }

    public static int Stars
    {
        get { return instance.stars; }
        set { instance.stars = value; }
    }

    public static int Hearts
    {
        get { return instance.hearts; }
        set { instance.hearts = value; }
    }

    public static int Best
    {
        get
        {
            return PlayerPrefs.GetInt("World" + GlobalValue.worldPlaying +
          GlobalValue.levelPlaying + "best", 0);
        }
        set
        {
            PlayerPrefs.SetInt("World" + GlobalValue.worldPlaying +
          GlobalValue.levelPlaying + "best", value);
        }
    }

    public static int BestStars
    {
        get
        {
            return PlayerPrefs.GetInt("World" + GlobalValue.worldPlaying +
          GlobalValue.levelPlaying + "stars", 0);
        }
        set
        {
            PlayerPrefs.SetInt("World" + GlobalValue.worldPlaying +
          GlobalValue.levelPlaying + "stars", value);
        }
    }

    public static int HighestLevel
    {
        get
        {
            return PlayerPrefs.GetInt("World" + GlobalValue.worldPlaying +
          "HighestLevel", 1);
        }
        set
        {
            PlayerPrefs.SetInt("World" + GlobalValue.worldPlaying +
          "HighestLevel", value);
        }
    }

    public static GameState CurrentState
    {
        get { return instance.state; }
        set { instance.state = value; }
    }

    private PlayerController player;

    void Awake()
    {
        instance = this;
        player = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        Debug.Log("Elapsed gmase start :" + gameStart());

        state = GameState.Menu;
    }

    void Update()
    {
        if (Input.anyKeyDown && state != GameState.Playing)

            Play();
    }

    public void MoveThePlayerToLeft()
    {
        player.moveLeft = true;
        player.moveRight = false;
    }

    public void MoveThePlayerToRight()
    {
        player.moveLeft = false;
        player.moveRight = true;
    }

    public void StopTheMovement()
    {
        player.moveLeft = player.moveRight = false;
    }

    public void Play()
    {
        state = GameState.Playing;
    }

    public void GameSuccess()
    {
        state = GameState.Menu;

        if (GlobalValue.levelPlaying >= HighestLevel)
            HighestLevel = GlobalValue.levelPlaying + 1;

        if (score > Best)
        {
            Best = score;

            if (score >= star3 && BestStars < 3)
                BestStars = 3;
            else if (score >= star2 && BestStars < 2)
                BestStars = 2;
            else if (score >= star1 && BestStars < 1)
                BestStars = 1;
        }



        Menu_GameManager.instance.ShowLevelComplete();
        SoundManager.PlaySfx(soundSuccess);

        //Check if its the last world in the World 1 or World 2

        if (isFinishWorld)
        {
            Debug.Log("Elapsed Game start :" + gameEnd());

            int WorldReached = PlayerPrefs.GetInt("WorldReached", 1);
            if (WorldReached == GlobalValue.worldPlaying)
                PlayerPrefs.SetInt("WorldReached", GlobalValue.worldPlaying + 1);
        }

    }

    public void GameOver()
    {
        if (state == GameState.Playing)
        {
            Debug.Log("Elapsed Game End :" + gameEnd());

            state = GameState.Menu;
            //Menu_GameManager.instance.ShowGameOver();
            SoundManager.PlaySfx(soundFail, 0.5f);
            player.Dead();
        }
    }

}
