using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimedDestruction : MonoBehaviour
{
    public float time;

    void Awake()
    {
        time += Time.time;
    }

    void Update()
    {
        if (Time.time > time)
        {
            Destroy(gameObject);
        }
    }
}
