using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterShop : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] List<GameObject> itemSpawners = new List<GameObject>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
            foreach (GameObject itemSpawners in itemSpawners)
            {
                displayItems.DisplayItem(itemSpawners);
            }
        }
    }
}
