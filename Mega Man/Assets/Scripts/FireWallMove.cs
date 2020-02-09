using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireWallMove : MonoBehaviour
{
    public Vector2 posA;
    public Vector2 posB;

    void Update()
    {
        transform.position = Vector3.Lerp(posA, posB, Mathf.PingPong(Time.time, 1.5f));
    }
}
