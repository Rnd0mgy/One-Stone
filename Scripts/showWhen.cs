using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class showWhen : MonoBehaviour
{
    public string when;
    public GameObject text;
    public bool on = true;

    void Start()
    {
        if (PlayerPrefs.GetInt(when) > 0)
        {
            text.SetActive(on);
        }
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(when) > 0)
        {
            text.SetActive(on);
        }
    }
}
