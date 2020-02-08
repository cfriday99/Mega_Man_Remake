using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletHit : MonoBehaviour
{

    public int bulletStrength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            collision.gameObject.GetComponent<EnemyHealth>().TakeDamage(bulletStrength);
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "Player")
        {
            if (!collision.gameObject.transform.GetChild(0).gameObject.activeSelf)
                collision.gameObject.GetComponent<PlayerHealth>().TakeDamage(bulletStrength);
            Destroy(gameObject);
        }
    }

}
