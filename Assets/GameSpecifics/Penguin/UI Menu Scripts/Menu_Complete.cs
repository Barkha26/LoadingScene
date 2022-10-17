using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_Complete : MonoBehaviour {

    public GameObject menu;
    public GameObject restart;
    public GameObject next;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    public Text scoreText;
    public Text bestScore;

    private int scoreRunning;
    private int score = 0;
    private bool finishCounting = false;

    void Awake ()
    {
        menu.SetActive(false);
        restart.SetActive(false);
        next.SetActive(false);
        star1.SetActive(false);
        star2.SetActive(false);
        star3.SetActive(false);
    }

    void Start()
    {
        bestScore.text = GameManager.Best.ToString();
        scoreRunning = GameManager.Score / 90;
    }
	
	void Update () {

        if (!finishCounting)
        {
            score += scoreRunning;

            if (score > GameManager.instance.star1)
                star1.SetActive(true);
            if (score > GameManager.instance.star2)
                star2.SetActive(true);
            if (score > GameManager.instance.star3)
                star3.SetActive(true);

            if(score > GameManager.Score)
            {
                finishCounting = true;
                score = GameManager.Score;

                menu.SetActive(true);
                restart.SetActive(true);
                if (!GameManager.instance.isFinishWorld)
                    next.SetActive(true);
            }

            scoreText.text = score.ToString();
        }

	}
}
