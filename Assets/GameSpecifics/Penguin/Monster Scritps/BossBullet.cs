using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBullet : MonoBehaviour {

    public float speed = 0.08f;

	void Start ()
    {
        Destroy(gameObject, 3);	
	}

	void Update ()
    {
        transform.Translate(-speed, 0, 0);
	}

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Player")
        {
            GameManager.Hearts--;
            Destroy(gameObject);
        }
    }
}
