using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour {

    public float speed = 0.12f;

	void Update () {
        if (transform.position.y >= 5)
            Destroy(gameObject);

        transform.position = new Vector2(transform.position.x, transform.position.y + speed);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.name == "Brick" || target.name == "Brick2")
        {
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);

            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }

}
