using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnterShop : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] List<GameObject> itemSpawners = new List<GameObject>();
    [SerializeField] ItemDesc itemDesc;
    [SerializeField] List<ItemDesc> itemDescs = new List<ItemDesc>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            foreach (GameObject itemSpawners in itemSpawners)
            {
                displayItems.DisplayItem(itemSpawners);
            }
            foreach (ItemDesc itemDescription in itemDescs)
            {
                itemDescription.DisplayText(displayItems.price);
            }
        }
    }
}
