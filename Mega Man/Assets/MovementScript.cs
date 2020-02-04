using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class MovementScript : MonoBehaviour
{

    public float movementSpeed = 5.0f;
    public float jumpForce = 300.0f;
    public LayerMask whatIsGround;

    private Rigidbody2D rb;
    private float horizontalMovement;
    private bool jump;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        horizontalMovement = Input.GetAxisRaw("Horizontal") * movementSpeed;
        if (Physics2D.OverlapCircle(transform.position - new Vector3(0.0f, 0.5f, 0.0f), 0.1f, whatIsGround)) 
            jump = Input.GetKeyDown(KeyCode.Space);
    }

    private void FixedUpdate()
    {
        if (jump)
            rb.AddForce(new Vector2(0.0f, jumpForce));
            rb.velocity = new Vector2(horizontalMovement, rb.velocity.y);
            jump = false;
    }

}
