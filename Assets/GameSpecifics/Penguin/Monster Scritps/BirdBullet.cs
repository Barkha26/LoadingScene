using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdBullet : MonoBehaviour {

    private Transform monsterBird;

	void Start () {
        monsterBird = GameObject.Find("Monster Bird").transform;
        StartCoroutine(ScaleRight());
        Destroy(gameObject, 2);
	}
	
	IEnumerator ScaleRight()
    {
        if (monsterBird.localScale.x == 1)
            transform.localScale = new Vector2(0.3f, 0.3f);
        else
            transform.localScale = new Vector2(-0.3f, 0.3f);
        yield return new WaitForSeconds(0);
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
