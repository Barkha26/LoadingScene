using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Menu_WorldChoose : MonoBehaviour {

    public GameObject lock2;
    public GameObject lock3; //if you have world 3

    public Button world2;
    public Button world3; //if you have world 3

    int worldReached;

    void Start () {
		worldReached = PlayerPrefs.GetInt("WorldReached", 1);

        if (worldReached > 1)
            lock2.SetActive(false);
        else
            world2.interactable = false;

        if (worldReached > 2)
            lock3.SetActive(false);
        else
            world3.interactable = false;
    }
	
}
