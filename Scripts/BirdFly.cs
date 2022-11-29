using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdFly : MonoBehaviour
{
    public float speed;
    
    void Update()
    {
        transform.position += transform.right * speed * Time.deltaTime;
        if (transform.position.x > 15)
        {
            Destroy(gameObject);
        }
    }
}
