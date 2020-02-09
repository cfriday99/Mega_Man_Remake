using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireShield : MonoBehaviour
{

    public float startShieldTime;
    public float shieldTime;

    private PlayerController2D playerController2D;

    private void Start()
    {
        shieldTime = startShieldTime;
        playerController2D = FindObjectOfType<PlayerController2D>();
    }

    private void Update()
    {
        if (!playerController2D.fireBulletShootable)
            transform.GetChild(0).gameObject.SetActive(false);

        shieldTime = Mathf.Max(0.0f, shieldTime - Time.deltaTime);

        if (shieldTime == 0.0f)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }

    public void Activate(float seconds)
    {
        shieldTime = seconds;
        transform.GetChild(0).gameObject.SetActive(true);
    }

}
