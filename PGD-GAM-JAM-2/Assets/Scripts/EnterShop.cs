using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] List<GameObject> itemSpawners = new List<GameObject>();
    public void OnTriggerEnter(Collider other)
    {
        foreach (GameObject item in itemSpawners)
        {
            displayItems.DisplayItem(item);
            Destroy(this.gameObject);
        }
        
    }
}
