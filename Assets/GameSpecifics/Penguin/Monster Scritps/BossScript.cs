using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPos, playerRangePos;
    public GameObject death, bossBody;
    public LayerMask playerLayer;

    private int hearts = 5;
    private bool isPlayerInTheRange;

	void Start () {
        StartCoroutine(Shoot());
	}
	
	void Update () {
        if (hearts == 0)
            StartCoroutine(Death());

        Collider2D leftHit = Physics2D.OverlapCircle(playerRangePos.position, 0.3f,
            playerLayer);

        if(leftHit != null)
        {
            if (leftHit.gameObject.tag == "Player")
                isPlayerInTheRange = true;
        }

	}

    IEnumerator Shoot()
    {
        if (isPlayerInTheRange)
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2, 4));
        StartCoroutine(Shoot());
    }

    IEnumerator Death()
    {
        isPlayerInTheRange = false;
        bossBody.SetActive(false);
        death.SetActive(true);
        yield return new WaitForSeconds(0.7f);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.tag == "Bullet")
        {
            hearts--;
            Destroy(target.gameObject);
        }
    }

}
