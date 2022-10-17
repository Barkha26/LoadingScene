using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Level : MonoBehaviour {

    public int level = 1;
    public GameObject locked;
    public GameObject star1;
    public GameObject star2;
    public GameObject star3;

    private string levelName;
    private int stars;
    private int highestLevel;

    void Start ()
    {
        levelName = gameObject.name;
        highestLevel = PlayerPrefs.GetInt("World" + GlobalValue.worldPlaying + 
            "HighestLevel", 1);
        stars = PlayerPrefs.GetInt("World" + GlobalValue.worldPlaying + level + "stars", 0);
        CheckStars();

        if(level > highestLevel)
        {
            locked.SetActive(true);
            GetComponent<Button>().interactable = false;
        }
        else
        {
            locked.SetActive(false);
        }

	}
	
    public void LoadLevel()
    {
        GlobalValue.levelPlaying = level;
        Menu_HomeScreen.instance.LoadLevel(levelName);
    }

    private void CheckStars()
    {
        switch (stars)
        {
            case 1:
                star1.SetActive(true);
                star2.SetActive(false);
                star3.SetActive(false);
                break;

            case 2:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(false);
                break;

            case 3:
                star1.SetActive(true);
                star2.SetActive(true);
                star3.SetActive(true);
                break;
            default:
                star1.SetActive(false);
                star2.SetActive(false);
                star3.SetActive(false);
                break;
        }
    }
	
}
