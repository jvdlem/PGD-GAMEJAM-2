using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] List<DisplayItems> displayItemsList;
    public void OnTriggerEnter(Collider other)
    {
        foreach(DisplayItems display in displayItemsList)
        {
            displayItems.DisplayItem();
        }
        Destroy(this.gameObject);
    }
}
