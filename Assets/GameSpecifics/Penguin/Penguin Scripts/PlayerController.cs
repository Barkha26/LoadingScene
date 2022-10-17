using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5f;
    private float jumpForce = 11f;
    private float xBound = -9f, yBound = -7.8f;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool dead;
    private bool isGrounded;
    private bool canShoot = true;
    public bool moveLeft, moveRight;

    public GameObject bullet;
    public Transform bulletPos;

    public AudioClip jumpClip, dieClip, collectClip, shootClip, hitClip, eatClip;

    void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

	void Update () 
    {

        if (Input.GetKeyDown(KeyCode.LeftShift) && !dead)
            Shoot();

        if (transform.position.y <= yBound)
            GameManager.Hearts = 0;
	}
    
    void FixedUpdate()
    {
        if (!dead) { 
            PlayerWalk();
            MoveThePlayer();
        }
    }

    void PlayerWalk()
    {
        float h = Input.GetAxisRaw("Horizontal");

        if(h > 0)
        {
            myBody.velocity = new Vector2(speed * h, myBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(h));
            transform.localScale = new Vector2(h, transform.localScale.y);

        }else if(h < 0)
        {
            myBody.velocity = new Vector2(speed * h, myBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(h));
            transform.localScale = new Vector2(h, transform.localScale.y);
        }
        else
        {
            myBody.velocity = new Vector2(0, myBody.velocity.y);
            anim.SetFloat("Speed", Mathf.Abs(h));
        }

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
        if (transform.position.x < xBound)
            transform.position = new Vector2(xBound, transform.position.y);
    }

    void MoveThePlayer()
    {
        if (moveLeft)
            MoveLeft();
        if (moveRight)
            MoveRight();
    }

    void MoveRight()
    {
        myBody.velocity = new Vector2(speed, myBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(speed));
        transform.localScale = new Vector2(1, transform.localScale.y);
    }

    void MoveLeft()
    {
        myBody.velocity = new Vector2(-speed, myBody.velocity.y);
        anim.SetFloat("Speed", Mathf.Abs(speed));
        transform.localScale = new Vector2(-1, transform.localScale.y);
    }

    public void Shoot()
    {
        
        if (canShoot)
        {
            Instantiate(bullet, bulletPos.position, Quaternion.identity);
            SoundManager.PlaySfx(shootClip);
            StartCoroutine(CanShoot());
        }

    }

    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            myBody.velocity = new Vector2(myBody.velocity.x, jumpForce);
            anim.SetBool("isGround", false);
            SoundManager.PlaySfx(jumpClip);
        }
    }

    public void Dead()
    {
        anim.SetTrigger("Die");
        StopAllCoroutines();
        myBody.velocity = Vector2.zero;
        myBody.gravityScale = 0.5f;
        dead = true;
        SoundManager.PlaySfx(dieClip);
    }

    void OnCollisionEnter2D(Collision2D target)
    {
        if(target.gameObject.name == "Ground")
        {
            isGrounded = true;
            anim.SetBool("isGround", true);
        }

        if (target.gameObject.name == "Enemy")
        {
            GameManager.Hearts--;
            SoundManager.PlaySfx(hitClip);
        }

    }

    void OnTriggerEnter2D(Collider2D target)
    {
        if(target.name == "Star")
        {
            Destroy(target.GetComponent<CircleCollider2D>());
            target.GetComponent<Animator>().SetTrigger("Collected");
            GameManager.Stars++;
            GameManager.Score += 30;
            SoundManager.PlaySfx(collectClip);
        }

        if(target.name == "Fruit")
        {
            Destroy(target.GetComponent<CircleCollider2D>());
            target.GetComponent<Animator>().SetTrigger("Collected");
            GameManager.Score += 20;
            GameManager.Hearts++;
            SoundManager.PlaySfx(eatClip);
        }

        if(target.name == "Trap")
        {
            GameManager.Hearts--;
            SoundManager.PlaySfx(hitClip);
        }

        if (target.name == "Boss")
            GameManager.Hearts = 0;

    }

    IEnumerator CanShoot()
    {
        canShoot = false;
        yield return new WaitForSeconds(.5f);
        canShoot = true;
    }

}
