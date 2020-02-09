using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{

    public float fireRate = 1.0f;

    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;
    private float currentFire;

    public Animator animator;
    public GameObject bullet;
    public GameObject fireBullet;
    public float speed;
    public float jumpForce;

    bool isGrounded;

    public Transform groundCheck;
    public Transform groundCheckL;
    public Transform groundCheckR;
    [HideInInspector]
    public GameObject currentBullet;

    [HideInInspector]
    public bool fireBulletShootable;

    public AudioClip stage1;
    public AudioClip stage2;
    public AudioClip gameWin;
    public AudioClip death;
    public AudioSource audioSource;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentBullet = bullet;
        audioSource.clip = stage1;
        audioSource.Play();
        audioSource.loop = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            if (Time.time > currentFire)
            {
                currentBullet = bullet;
                Shoot();
                currentFire = Time.time + 1.0f / fireRate;
            }
        }
        else if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) && fireBulletShootable)
        {
            if (Time.time > currentFire)
            {
                currentBullet = fireBullet;
                Shoot();
                currentFire = Time.time + 1.0f / fireRate;
            }
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");
        //if (isGrounded)
        //{
        //    animator.SetTrigger("Shoot");
        //}

        Vector3 dir = transform.right;
        if (spriteRenderer.flipX == true)
            dir *= -1;
        GameObject b = (GameObject)(Instantiate(currentBullet, transform.position + dir, Quaternion.identity));
        Destroy(b, 3.0f);
        b.GetComponent<Rigidbody2D>().AddForce(dir * 1000);
    }

    void FixedUpdate()
    {
        if ((Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, groundCheckL.position, 1 << LayerMask.NameToLayer("Ground"))) ||
           (Physics2D.Linecast(transform.position, groundCheckR.position, 1 << LayerMask.NameToLayer("Ground"))))
        {
            isGrounded = true;
            animator.SetBool("InAir", false);
        }
        else
        {
            isGrounded = false;
            animator.SetBool("InAir", true);
        }

        if (Input.GetKey("d") || Input.GetKey("right"))
        {
            rb2d.velocity = new Vector2(speed, rb2d.velocity.y);
            {
                if (isGrounded)
                {
                    animator.SetFloat("Speed", speed);
                    spriteRenderer.flipX = false;
                }
            }
        }
        else if (Input.GetKey("a") || Input.GetKey("left"))
        {
            rb2d.velocity = new Vector2(-speed, rb2d.velocity.y);
            {
                if (spriteRenderer != null)
                {
                    if (isGrounded)
                    {
                        animator.SetFloat("Speed", speed);
                        spriteRenderer.flipX = true;
                    }
                }
            }
        }

        else
        {
            animator.SetFloat("Speed", 0);
            rb2d.velocity = new Vector2(0, rb2d.velocity.y);
        }
        if (Input.GetKey("w") && isGrounded || Input.GetKey("up") && isGrounded)
        {
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);
        }
        if (Input.GetKey("escape"))
            Application.Quit();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag.Equals("Goal"))
        {
            audioSource.clip = stage2;
            audioSource.Play();
            audioSource.loop = true;
        }
    }

    //Friction For Moving Platforms
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
            this.transform.parent = collision.transform;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Platform"))
            this.transform.parent = null;
    }
}
