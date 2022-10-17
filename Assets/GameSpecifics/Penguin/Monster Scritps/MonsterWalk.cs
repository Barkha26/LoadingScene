using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterWalk : MonoBehaviour {

    public float speedWalk = 0.02f;
    public float minX, maxX;
    public int distance;
    private int direction;

    private bool isDead;

    public Transform topCollsion;
    public LayerMask playerLayer;

	void Start () {
        direction = -1;
        maxX = transform.position.x;
        minX = maxX - distance;
	}

	void Update () {
        if(GameManager.CurrentState == GameManager.GameState.Playing) { 
            Movement();
            DetectCollison();
        }
    }

    void DetectCollison()
    {
        Collider2D topHit = Physics2D.OverlapCircle(topCollsion.position,
            0.1f, playerLayer);

        if (topHit != null)
        {
            if (topHit.gameObject.tag == "Player")
            {
                if (!isDead)
                {
                    topHit.gameObject.GetComponent<Rigidbody2D>().velocity =
                        new Vector2(topHit.gameObject.GetComponent<Rigidbody2D>().velocity.x,
                        7f);
                    transform.localScale = new Vector3(1, 0.2f, 1);
                    isDead = true;
                    Destroy(gameObject, 1);
                }
            }

        }
    }

    void Movement()
    {
        if (!isDead)
        {
            switch (direction)
            {
                case -1:
                    if (transform.position.x > minX)
                        transform.Translate(-speedWalk, 0, 0);
                    else
                    {
                        direction = 1;
                        transform.localScale = new Vector2(transform.localScale.x * -1,
                            transform.localScale.y);
                    }
                    break;

                case 1:
                    if (transform.position.x < maxX)
                        transform.Translate(speedWalk, 0, 0);
                    else
                    {
                        direction = -1;
                        transform.localScale = new Vector2(transform.localScale.x * -1,
                            transform.localScale.y);
                    }

                    break;
            }
        }
    }
}
