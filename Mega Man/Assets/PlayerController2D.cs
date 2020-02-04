using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb2d;
    private SpriteRenderer spriteRenderer;

    public float speed;
    public float jumpForce;

    bool isGrounded;

    public Transform groundCheck;

    // Start is called before the first frame update
    void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

    }

    void Update()
    {
        
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
}
