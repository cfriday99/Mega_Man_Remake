using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePickup : MonoBehaviour
{
    public AudioSource audioSource;
    public SpriteRenderer spriteRenderer;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>().fireBulletShootable = true;
            FireShield fireShield = collision.gameObject.GetComponent<FireShield>();
            fireShield.enabled = true;
            fireShield.Activate(fireShield.startShieldTime);

            spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
            audioSource.Play();
        }
    }

}
