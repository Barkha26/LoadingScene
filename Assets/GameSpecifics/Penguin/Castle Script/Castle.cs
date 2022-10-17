using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour {

    public GameObject light;

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Player")
        {
            GameManager.instance.GameSuccess();
            light.SetActive(true);
            GetComponent<Animator>().SetTrigger("Close");
            target.gameObject.SetActive(false);
            enabled = false;
        }
    }
	
}
