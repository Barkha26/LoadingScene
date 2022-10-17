using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameplayController : MonoBehaviour
{

    public static GameplayController instance;

    private GameObject[] bricks1, bricks2;
    private Ball ball;

    public int brickCounter;
    private int life = 2;
    private bool died;

    public float yBound = -5.2f;


    const string packageName = "com.example.mygameex";
    static AndroidJavaClass _pluginClass;
    //static AndroidJavaClass=_pluginClass;
    static AndroidJavaObject _pluginInstance;
    public static AndroidJavaClass PluginClass
    {
        get
        {
            if (_pluginClass == null)
            {
                _pluginClass = new AndroidJavaClass(packageName);
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

    void Awake()
    {
        instance = this;
        ball = GameObject.FindObjectOfType<Ball>();
        Time.timeScale = 1;

    }

    void SendGameScore(string str)
    {
        if (Application.platform == RuntimePlatform.Android)
            PluginInstance.Call("gameSendScore", str);
    }

    void Update()
    {
        BrickCounter();
        ScoreTextFunction();
        LifeCounter();

        if (brickCounter == 0)
        {
            GameController.instance.NextLevelPanel();
        }

    }

    void LifeCounter()
    {
        GameController.instance.lifeScoreText.text = life.ToString();

        if (ball.transform.position.y < yBound)
        {
            died = true;
            ball.GetComponent<Rigidbody2D>().velocity = Vector2.zero;

            if (life == 0)
            {
                GameController.instance.Died();
            }
            else if (life > 0)
            {
                ball.canMove = false;
                ball.shoot = false;
            }

            if (died)
            {
                if (Time.timeScale == 1)
                    life--;

                died = false;
            }
        }
    }

    void ScoreTextFunction()
    {
        GameController.instance.scoreText.text = "Score: " + PlayerPrefs.GetInt("Score");
        SendGameScore("" + PlayerPrefs.GetInt("Score"));


        if (PlayerPrefs.GetInt("Score", 0) > PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", PlayerPrefs.GetInt("Score"));

        }
    }

    void BrickCounter()
    {
        bricks1 = GameObject.FindGameObjectsWithTag("Brick");
        bricks2 = GameObject.FindGameObjectsWithTag("Brick2");

        brickCounter = bricks1.Length + bricks2.Length;
        Debug.Log("Brick Counter function is called" + brickCounter);

    }
}
