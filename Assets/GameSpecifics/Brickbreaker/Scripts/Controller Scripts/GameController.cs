using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public static GameController instance;

	public Text scoreText, lifeScoreText, highScoreText, pauseText;
    public Button playBtn, pauseBtn;

    public Sprite[] playSprite;
    public GameObject pausePanel;

    const int layerId = 4;
    const string layerName = "Brick";
    void Awake()
    {
        instance = this;

        if(SceneManager.GetActiveScene().name == "Level 1")
            PlayerPrefs.SetInt("Score", 0);
    }

    private void Start()
    {
        AddSortlayerToSprites();
    }

    private void OnEnable()
    {
        AddSortlayerToSprites();
    }

    private void AddSortlayerToSprites()
    {
        GameObject[] sprite = GameObject.FindGameObjectsWithTag("Brick");
        for (int i = 0; i < sprite.Length; ++i)
        {
            sprite[i].GetComponent<SpriteRenderer>().sortingLayerName = "Brick";
        }
    }

    public void NextLevelPanel()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "WELL DONE!!!";
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        pauseBtn.interactable = true;
        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[0];
        playBtn.onClick.AddListener(() => NextLevel());
    }

    void NextLevel()
    {
        pausePanel.SetActive(false);
        Time.timeScale = 0;

        if(SceneManager.GetActiveScene().name == "Level 1")
        {
            //SceneManager.LoadScene("Level 2");
            //LoadScene.instance.LoadMaterial();
            LoadScene.instance.PlayBtn("Level2");
        }

        if (SceneManager.GetActiveScene().name == "Level 2")
        {
            //SceneManager.LoadScene("Level 3");
            //LoadScene.instance.LoadMaterial();
            LoadScene.instance.PlayBtn("Level3");
        }
    }
    
    public void Died()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "GAME OVER";
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        pauseBtn.interactable = true;
        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[1];
        playBtn.onClick.AddListener(() => PlayAgain());
    }

    public void Pause()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        pauseText.text = "PAUSED";
        highScoreText.text = PlayerPrefs.GetInt("HighScore").ToString();

        playBtn.onClick.RemoveAllListeners();
        playBtn.image.sprite = playSprite[0];
        playBtn.onClick.AddListener(() => UnPause());
    }

    void UnPause()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
    }

    public void PlayAgain()
    {
        Time.timeScale = 1;
        //SceneManager.LoadScene("Gameplay");
        //LoadScene.instance.LoadMaterial();
        LoadScene.instance.PlayBtn("gameplay");
    }
	
    public void Home()
    {
        //SceneManager.LoadScene("MainMenu");
        //LoadScene.instance.LoadMaterial();
        LoadScene.instance.PlayBtn("mainmenu");
    }
}
