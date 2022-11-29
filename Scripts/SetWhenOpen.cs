using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetWhenOpen : MonoBehaviour
{
    void Awake()
    {
        PlayerPrefs.SetInt("menuOpen", 1);
    }
}
