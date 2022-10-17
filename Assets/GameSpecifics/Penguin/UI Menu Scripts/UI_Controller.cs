using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Controller : MonoBehaviour {

    private PlayerController player;

	void Start () {
        player = FindObjectOfType<PlayerController>();
	}
	
    public void Jump()
    {
        player.Jump();
    }

    public void Shoot()
    {
        player.Shoot();
    }

}
