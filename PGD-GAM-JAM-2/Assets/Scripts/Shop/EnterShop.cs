using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterShop : MonoBehaviour
{
    //Get Shop script and spawners
    [SerializeField] ShopScript shopScript;
    [SerializeField] List<GameObject> itemSpawners = new List<GameObject>();
    public void OnTriggerEnter(Collider other)
    {
        //When player enters the shop spawn in items and destroy the trigger
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            foreach (GameObject itemSpawners in itemSpawners)
            {
                shopScript.DisplayItem(itemSpawners);
            }
        }
    }
}
