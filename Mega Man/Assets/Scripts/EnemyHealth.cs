using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{

    public GameObject particlePrefab;
    public float bonusShieldTime = 3.0f;
    public int healthAmount;

    private FireShield fireShield;
    private ParticleSystem particle;
    private bool killed;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        GameObject p = Instantiate(particlePrefab);
        p.transform.position = transform.position;
        p.transform.parent = transform;
        particle = p.GetComponent<ParticleSystem>();
        fireShield = FindObjectOfType<FireShield>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }


    public void TakeDamage(int amount)
    {
        healthAmount -= amount;
        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f);
    }

    private void Update()
    {
        if (healthAmount <= 0 && !killed)
            Kill();

        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Min(1.0f, spriteRenderer.color.a + 0.05f));
    }

    private void Kill()
    {
        killed = true;
        if (fireShield.enabled)
        {
            fireShield.Activate(fireShield.shieldTime + bonusShieldTime);
        }
        particle.Play();
        GetComponent<SpriteRenderer>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 2.0f);
    }

}
