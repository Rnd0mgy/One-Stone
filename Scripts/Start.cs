using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Start : MonoBehaviour
{
    public Color offColor;
    public Color onColor;
    public SpriteRenderer sr;
    public bool working = false;

    void Awake()
    {
        if (PlayerPrefs.GetInt("score") + PlayerPrefs.GetInt("faster") + PlayerPrefs.GetInt("dash") + PlayerPrefs.GetInt("parachute") + PlayerPrefs.GetInt("BouncyBirds") + PlayerPrefs.GetInt("crown") == 0)
        {
            sr.color = offColor;
        }
        else
        {
            sr.color = onColor;
            working = true;
        }
    }
}
