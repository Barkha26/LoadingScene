using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    public GameObject[] powerUps;

    public float speed, multiplier, constantSpeed;
    public float ballDir;

    public Transform startPoint;
    private Rigidbody2D myBody;

    public bool canMove, shoot;

    public AudioClip hitBrickClip;

	void Awake ()
    {
        myBody = GetComponent<Rigidbody2D>();
	}

    private void Start()
    {
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Ball";
        Invoke("StartGame", 2f);
    }

    void Update ()
    {
        if (canMove)
        {
            if (!shoot)
            {
                shoot = true;
                myBody.AddForce(new Vector2(ballDir, speed * multiplier));
            }
        }
        else
        {
            transform.position = startPoint.position;
        }

        if (Input.GetKeyDown(KeyCode.Space))
            canMove = true;

        myBody.velocity = constantSpeed * myBody.velocity.normalized;


    }

    public void StartGame()
    {
        canMove = true;
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.name == "Brick")
        { 
            Destroy(target.gameObject);

            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);

            if (Random.value < .3f)
            {
                int randomPowerUP = Random.Range(0, powerUps.Length);
                Instantiate(powerUps[randomPowerUP], target.transform.position,
                    Quaternion.identity);
            }

            AudioSource.PlayClipAtPoint(hitBrickClip, transform.position);
        }

        if(target.gameObject.name == "Brick2")
        {
            target.gameObject.name = "Brick";
            PlayerPrefs.SetInt("Score", PlayerPrefs.GetInt("Score") + 100);

            if (Random.value < .3f)
            {
                int randomPowerUP = Random.Range(0, powerUps.Length);
                Instantiate(powerUps[randomPowerUP], target.transform.position,
                    Quaternion.identity);
            }
            AudioSource.PlayClipAtPoint(hitBrickClip, transform.position);
        }

    }

}
