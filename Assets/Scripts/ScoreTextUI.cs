using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreTextUI : MonoBehaviour
{
    public string start;
    public string varName;
    public string end;
    public Text text;

    void Update()
    {
        text.text = start + PlayerPrefs.GetInt(varName) + end;
    }
}
