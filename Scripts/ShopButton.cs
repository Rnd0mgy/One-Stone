using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopButton : MonoBehaviour
{
    public string itemName;
    public Button button;
    public int price;
    public Text priceText;

    void Awake()
    {
        if (itemName == "crown")
        {
            price = (PlayerPrefs.GetInt("crowns") + 1) * 100;
        }
        if (PlayerPrefs.GetInt(itemName) == 1)
        {
            button.interactable = false;
        }
        priceText.text = "$" + price;
    }

    void Update()
    {
        if (PlayerPrefs.GetInt(itemName) == 1)
        {
            button.interactable = false;
        }
    }

    public void Click()
    {
        if (PlayerPrefs.GetInt("score") >= price)
        {
            PlayerPrefs.SetInt("score", PlayerPrefs.GetInt("score") - price);
            PlayerPrefs.SetInt(itemName, 1);
            if (itemName == "crown")
            {
                PlayerPrefs.SetInt("crowns", PlayerPrefs.GetInt("crowns") + 1);
            }
        }
    }
}
