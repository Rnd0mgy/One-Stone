using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlideToCamera : MonoBehaviour
{
    public Transform cam;
    public float rotIntensity;
    public bool shaking;
    
    void Update()
    {
        if (shaking)
        {
            transform.rotation = Quaternion.Euler(new Vector3(transform.rotation.x, transform.rotation.y, (Mathf.Abs(Time.time % 0.2f - 0.1f) - 0.05f) * rotIntensity));
        }
        else
        {
            transform.rotation = Quaternion.Euler(Vector3.zero);
        }
    }
}
