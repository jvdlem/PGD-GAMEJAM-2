using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    [SerializeField] GameObject spawnPoint;
    public bool isBought;
    public float price;
    public void DisplayItem(GameObject itemSpawn)
    {
        GameObject instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
    }

    public void BuyItems()
    {
        
    }
}
