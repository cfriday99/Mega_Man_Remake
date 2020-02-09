using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinGame : MonoBehaviour
{
    public Canvas youWin;
    public AudioClip winMusic;
    public AudioSource audioSource;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            other.GetComponent<PlayerController2D>().enabled = false;
            other.GetComponent<AudioSource>().enabled = false;

            audioSource.clip = winMusic;
            audioSource.Play();
            youWin.gameObject.SetActive(true);
        }
    }
}