using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDesc : MonoBehaviour
{
    public Text nameAndPrice;
    public bool bought;

    public void Update()
    {
        if (bought)
        {
            nameAndPrice.text = "BOUGHT!!";
            nameAndPrice.color = Color.green;
        }
    }

    public void DisplayText(int price)
    {
        bought = false;
        nameAndPrice.text = "$"+price;
    }
}
