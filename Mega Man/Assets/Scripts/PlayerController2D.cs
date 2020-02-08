using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public Animator animator;
    public GameObject bullet;
    public GameObject fireBullet;
    public float speed;
    public float jumpForce;

    bool isGrounded;

    public Transform groundCheck;
    [HideInInspector]
    public GameObject currentBullet;

    [HideInInspector]
    public bool fireBulletShootable;

    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        currentBullet = bullet;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z) || Input.GetKeyDown(KeyCode.J))
        {
            currentBullet = bullet;
            Shoot();
        }
        else if ((Input.GetKeyDown(KeyCode.X) || Input.GetKeyDown(KeyCode.K)) && fireBulletShootable)
        {
            currentBullet = fireBullet;
            Shoot();
        }
    }

    private void Shoot()
    {
        animator.SetTrigger("Shoot");

        Vector3 dir = transform.right;
        if (spriteRenderer.flipX == true)
            dir *= -1;
        GameObject b = (GameObject)(Instantiate(currentBullet, transform.position + dir, Quaternion.identity));
        Destroy(b, 3.0f);
        b.GetComponent<Rigidbody2D>().AddForce(dir * 1000);
    }

    void FixedUpdate()
    {
        if (Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground")))
        {
            isGrounded = true;
        }
        else
        {
            isGrounded = false;
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
            //animator.Play("Player_idle");
            //if (isGrounded)
            //{
                animator.SetFloat("Speed", 0);
                rb2d.velocity = new Vector2(0, rb2d.velocity.y);
            //}
        }
        if (Input.GetKey("w") && isGrounded || Input.GetKey("up") && isGrounded)
        {
            //animator.Play("Player_jump");
            rb2d.velocity = new Vector2(rb2d.velocity.x, jumpForce);

        }
        if (Input.GetKey("escape"))
            Application.Quit();
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
