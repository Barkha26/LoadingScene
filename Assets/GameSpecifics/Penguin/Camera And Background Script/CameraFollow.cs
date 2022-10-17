using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    private Transform player;
    private float xBound = -2.8f;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
	}

	void Update () {
        transform.position = new Vector3(player.position.x, transform.position.y, -10);

        if(transform.position.x < xBound)
            transform.position = new Vector3(xBound, transform.position.y, -10);
    }
}
