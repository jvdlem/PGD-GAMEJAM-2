using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemDesc : MonoBehaviour
{
    public Text nameAndPrice;
    public bool bought;
    public void DisplayText(int price)
    {
        bought = false;
        if (!bought) nameAndPrice.text = "$"+price;
        else nameAndPrice.text = "Bought!!";
    }
}
