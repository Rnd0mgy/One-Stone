using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreText : MonoBehaviour
{
    public string start;
    public string varName;
    public TextMesh text;
    
    void Update()
    {
        text.text = start + PlayerPrefs.GetInt(varName);
    }
}
