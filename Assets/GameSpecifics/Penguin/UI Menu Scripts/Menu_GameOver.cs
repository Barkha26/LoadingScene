using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_GameOver : MonoBehaviour {

    public GameObject Next;

	void Start () {
        if (GlobalValue.levelPlaying >= GameManager.HighestLevel ||
            GameManager.instance.isFinishWorld)
            Next.SetActive(false);
	}
	
}
