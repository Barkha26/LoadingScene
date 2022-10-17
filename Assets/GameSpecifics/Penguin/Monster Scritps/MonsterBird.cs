using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterBird : MonoBehaviour {

    public GameObject bullet;
    public Transform bulletPos;

	void Start () {
        StartCoroutine(Shoot());
	}
	
	IEnumerator Shoot()
    {
        Instantiate(bullet, bulletPos.position, Quaternion.identity);
        yield return new WaitForSeconds(Random.Range(2, 3));
        StartCoroutine(Shoot());
    }
}
