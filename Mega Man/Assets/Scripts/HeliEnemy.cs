using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeliEnemy : MonoBehaviour
{

    public float flyingAmount;
    public float flyingVerticalSpeed;
    public float flyingHorizontalSpeed;

    private float startPos;
    private bool ableToMove;

    private void OnBecameVisible()
    {
        startPos = transform.position.y;
        ableToMove = true;
        flyingAmount = startPos + flyingAmount;
    }

    private void Update()
    {
        if (ableToMove)
        {
            transform.position = new Vector3(transform.position.x, Mathf.PingPong(Time.time * flyingVerticalSpeed, flyingAmount) + startPos, transform.position.z);
            transform.position += transform.right * flyingHorizontalSpeed * Time.deltaTime;
        }
    }

}
