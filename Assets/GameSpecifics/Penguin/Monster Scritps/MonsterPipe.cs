using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterPipe : MonoBehaviour {

    private Vector2 moveDirection = Vector2.up;
	
	void Start () {
        StartCoroutine(ChangeDirection());
	}

	void Update () {
        PipeMovement();
	}

    void PipeMovement()
    {
        transform.Translate(moveDirection * Time.smoothDeltaTime);
    }

    IEnumerator ChangeDirection()
    {
        yield return new WaitForSeconds(.6f);

        if (moveDirection == Vector2.up)
            moveDirection = Vector2.down;
        else
            moveDirection = Vector2.up;

        StartCoroutine(ChangeDirection());
    }

}
