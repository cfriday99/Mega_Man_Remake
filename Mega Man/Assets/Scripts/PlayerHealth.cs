using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{

    public Image healthBar;
    public Sprite[] healthBarSprites;
    public int healthAmount;
    public Canvas gameOver;
    public AudioClip death;
    public AudioClip gameOverMusic;
    public AudioClip damageClip;
    public AudioSource audioSource;

    private bool killed;
    private SpriteRenderer spriteRenderer;


    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (healthAmount <= 0 && !killed)
            Kill();

        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, Mathf.Min(1.0f, spriteRenderer.color.a + 0.05f));
        healthBar.sprite = healthBarSprites[healthAmount];
    }


    public void TakeDamage(int amount)
    {
        healthAmount -= amount;
        spriteRenderer.color = new Vector4(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.0f);
        audioSource.clip = damageClip;
        audioSource.Play();
    }

    private void Kill()
    {
        transform.GetChild(1).transform.parent = null;
        spriteRenderer.enabled = false;
        GetComponent<PlayerController2D>().enabled = false;
        GetComponent<Collider2D>().enabled = false;
        GetComponent<Rigidbody2D>().simulated = false;
        GetComponentInChildren<ParticleSystem>().Play();
        killed = true;
        audioSource.clip = death;
        audioSource.Play();
        StartCoroutine(GoToMenu());
    }

    private IEnumerator GoToMenu()
    {
        yield return new WaitForSeconds(2.0f);
        audioSource.clip = gameOverMusic;
        audioSource.Play();
        gameOver.gameObject.SetActive(true);
    }

}
