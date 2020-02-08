using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StandEnemy : MonoBehaviour
{

    public GameObject bullet;
    public float shootRate = 0.5f;

    private float shootNext;
    private SpriteRenderer spriteRenderer;
    private GameObject player;
    private bool visible;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnBecameInvisible()
    {
        visible = false;   
    }

    private void Update()
    {
        if (Time.time > shootNext && visible)
        {
            Shoot();
            shootNext = Time.time + 1.0f / shootRate;
        }

        if (player.transform.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }

    private void Shoot()
    {
        Vector3 dir = transform.right;
        if (spriteRenderer.flipX == true)
            dir *= -1;
        GameObject b = (GameObject)(Instantiate(bullet, transform.position + dir * 2.0f, Quaternion.identity));
        Destroy(b, 3.0f);
        b.GetComponent<Rigidbody2D>().AddForce(dir * 1000);
    }

}
