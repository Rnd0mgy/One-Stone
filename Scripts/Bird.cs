using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Birds") || collision.CompareTag("BouncyBirds"))
        {
            transform.position = new Vector3((float)Random.Range(-60, 61) / 10, -3.2f, 0);
        }
    }
}
