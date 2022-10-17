using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour {

    public float speed = 0.06f, xBounds = 2.15f;

    private float timer = 8, laserTimer = 0.5f;

    int direction = 0;
    float previousPositionX;

    private GameObject ball;
    private float speedBall;

    public GameObject laser;
    public Transform pos1, pos2;

    public AudioClip laserShootClip, ballClip;

    bool moveRight = false;
    bool moveLeft = false;

	void Start () { 
        ball = GameObject.Find("Ball");
        gameObject.GetComponent<SpriteRenderer>().sortingLayerName = "Player";
    }

	void Update () {
        if(Time.timeScale == 1)
            Movement();

        if (direction < 1)
            ball.GetComponent<Ball>().ballDir = -100;
        else if(direction > -1)
            ball.GetComponent<Ball>().ballDir = 100;
        else
            ball.GetComponent<Ball>().ballDir = 0;
            
    }

    void LateUpdate()
    {
        previousPositionX = transform.position.x;
    }

    void Movement()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            if (Input.GetTouch(0).position.x > Screen.width / 2)
            {
                transform.position = new Vector2((transform.position.x + speed * 10f),
                    transform.position.y);
            }
            else if (Input.GetTouch(0).position.x < Screen.width / 2)
            {
                transform.position = new Vector2((transform.position.x - speed * 10f),
                    transform.position.y);
            }
        }
        else
        {
            if (h > 0)
            {
                transform.position = new Vector2((transform.position.x + speed),
                    transform.position.y);
            }
            else if (h < 0)
            {
                transform.position = new Vector2((transform.position.x - speed),
                    transform.position.y);
            }
        }

        if (previousPositionX > transform.position.x)
            direction = -1;
        else if (previousPositionX < transform.position.x)
            direction = 1;
        else
            direction = 0;

        transform.position = new Vector2(Mathf.Clamp(transform.position.x,
            -xBounds, xBounds), transform.position.y);
    }

    public void EnableMoveRight()
    {
        moveRight = true;
    }

    public void DisableMoveRight()
    {
        moveRight = false;
    }

    public void EnableMoveLeft()
    {
        moveLeft = true;
    }

    public void DisableMoveLeft()
    {
        moveLeft = false;
    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.name == "SlideExpand(Clone)") 
        {
            StartCoroutine(PaddleExpand());
            Destroy(target.gameObject);
        }

        if (target.name == "SlideShrink(Clone)")
        {
            StartCoroutine(PaddleShrink());
            Destroy(target.gameObject);
        }

        if (target.name == "BallExpand(Clone)")
        {
            StartCoroutine(BallExpand());
            Destroy(target.gameObject);
        }

        if (target.name == "BallSpeedUp(Clone)")
        {
            Destroy(target.gameObject);
            StartCoroutine(BallSpeedUp());
        }

        if (target.name == "BallSlowDown(Clone)")
        {
            StartCoroutine(BallSlowDown());
            Destroy(target.gameObject);
        }

        if (target.name == "LaserPowerUp")
        {
            StartCoroutine(LaserShooting());
            Destroy(target.gameObject);
        }
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        float adjust = 3 * direction;
        target.rigidbody.velocity = new Vector2(target.rigidbody.velocity.x + adjust,
            target.rigidbody.velocity.y);

        AudioSource.PlayClipAtPoint(ballClip, transform.position);
    }

    IEnumerator PaddleExpand()
    {
        transform.localScale = new Vector2(0.6f, transform.localScale.y);

        yield return new WaitForSeconds(timer);

        transform.localScale = new Vector2(0.4f, transform.localScale.y);
    }

    IEnumerator PaddleShrink()
    {
        transform.localScale = new Vector2(0.25f, transform.localScale.y);

        yield return new WaitForSeconds(timer);

        transform.localScale = new Vector2(0.4f, transform.localScale.y);
    }


    IEnumerator BallExpand()
    {
        ball.transform.localScale = new Vector2(0.5f, 0.5f);

        yield return new WaitForSeconds(timer);

        ball.transform.localScale = new Vector2(0.3f, 0.3f);
    }

    IEnumerator BallSpeedUp()
    {
        ball.GetComponent<Ball>().constantSpeed = 6.5f;

        yield return new WaitForSeconds(timer);

        ball.GetComponent<Ball>().constantSpeed = 5f;
    }

    IEnumerator BallSlowDown()
    {
        ball.GetComponent<Ball>().constantSpeed = 3.5f;

        yield return new WaitForSeconds(timer);

        ball.GetComponent<Ball>().constantSpeed = 5f;
    }

  
    IEnumerator LaserShooting()
    {
        for(int i = 0; i < 6; i++)
        {
            Instantiate(laser, pos1.position, Quaternion.identity);
            Instantiate(laser, pos2.position, Quaternion.identity);
            AudioSource.PlayClipAtPoint(laserShootClip, transform.position);
            yield return new WaitForSeconds(laserTimer);
        }
    }

}
