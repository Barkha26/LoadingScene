using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour {

    public float speed;

    private bool right;

    private Transform player;
	
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(WhereToShoot());
        Destroy(gameObject, 2);
	}
	
	void Update () {
        if(right)
            transform.position = new Vector2(transform.position.x + speed,
                transform.position.y);
        else
            transform.position = new Vector2(transform.position.x - speed,
                transform.position.y);
    }
    
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ground")
            Destroy(gameObject);

        if(target.tag == "Enemy")
        {
            Destroy(target.gameObject);
            Destroy(gameObject);
        }
    }

    IEnumerator WhereToShoot()
    {
        yield return new WaitForSeconds(0);
        if (player.localScale.x == 1)
            right = true;
        else
            right = false;
    }
}
