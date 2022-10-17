using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUP : MonoBehaviour {

    public float speed;

	void Start () {
        if(transform.position.y <= -5)
            Destroy(gameObject);
	}
	
	void Update () {
        transform.position = new Vector2(transform.position.x, transform.position.y + speed);
	}
}
