using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crown : MonoBehaviour
{
    public SpriteRenderer sr;
    public Animator anim;
    
    void Awake()
    {
        if (PlayerPrefs.GetInt("crown") == 1)
        {
            sr.enabled = true;
        }
    }

    void Update()
    {
        if (anim.GetInteger("State") == 4)
        {
            transform.localPosition = new Vector2(-1, 0);
            transform.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (anim.GetInteger("State") == 5)
        {
            transform.localPosition = new Vector2(1, 0);
            transform.rotation = Quaternion.Euler(0, 0, -90);
        }
        else
        {
            transform.localPosition = new Vector2(0, 1);
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
    }
}
