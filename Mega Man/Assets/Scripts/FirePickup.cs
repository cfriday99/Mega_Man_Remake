using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirePickup : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.GetComponent<PlayerController2D>().fireBulletShootable = true;
            FireShield fireShield = collision.gameObject.GetComponent<FireShield>();
            fireShield.enabled = true;
            fireShield.Activate(fireShield.startShieldTime);
            Destroy(gameObject);
        }
    }

}
