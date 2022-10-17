using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBladeScript : MonoBehaviour {

    private Rigidbody2D myBody;

    public float rotateSpeed, movingSpeed;
    private bool isPlayerInTheRange;

    private Transform playerRange;
    public LayerMask playerLayer;

	void Start () {
        myBody = GetComponent<Rigidbody2D>();
        playerRange = GameObject.Find("Player Range").transform;
	}
	
	void Update () {
        if (isPlayerInTheRange) { 
        transform.Rotate(Vector3.forward, rotateSpeed * Time.smoothDeltaTime);

        myBody.velocity = new Vector2(-movingSpeed * Time.smoothDeltaTime,
            myBody.velocity.y);

            Destroy(gameObject, 7);
        }

        Collider2D rangeHit = Physics2D.OverlapCircle(playerRange.transform.position,
            0.3f, playerLayer);

        if(rangeHit != null)
        {
            if(rangeHit.gameObject.tag == "Player")
            {
                isPlayerInTheRange = true;
            }
        }

    }
}
