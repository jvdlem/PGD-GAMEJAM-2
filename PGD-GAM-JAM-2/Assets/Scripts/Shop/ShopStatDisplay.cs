using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopStatDisplay : MonoBehaviour
{
    [SerializeField] public int itemPrice;
    [SerializeField] private string itemName, itemDescription;
    public bool isBought;

    public string GetShopStats()
    {
        return "" + itemName + "\n" + "$" + itemPrice + "\n" + itemDescription;
    }
}
