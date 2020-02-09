using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{

    public float knockBackAmount = 20.0f;
    public float knockBackTime = 0.5f;
    public int damageAmount = 1;

    private GameObject player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if (collision.gameObject.transform.GetChild(0).gameObject.activeSelf)
                return;

            player = collision.gameObject;
            player.GetComponent<PlayerHealth>().TakeDamage(damageAmount);
            player.GetComponent<PlayerController2D>().enabled = false;
            GetComponent<Collider2D>().enabled = false;
            Vector3 dir = (player.transform.position - transform.position).normalized;
            player.GetComponent<Rigidbody2D>().AddForce(dir * knockBackAmount, ForceMode2D.Impulse);
            StartCoroutine(KnockBackDone());
        }
    }

    private IEnumerator KnockBackDone()
    {
        yield return new WaitForSeconds(knockBackTime);

        player.GetComponent<PlayerController2D>().enabled = true;
        GetComponent<Collider2D>().enabled = true;
    }

}
