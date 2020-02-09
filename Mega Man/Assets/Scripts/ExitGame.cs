using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitGame : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (Input.GetKey("escape"))
        {
            Debug.Log("End");
            Application.Quit();
        }
    }
}
