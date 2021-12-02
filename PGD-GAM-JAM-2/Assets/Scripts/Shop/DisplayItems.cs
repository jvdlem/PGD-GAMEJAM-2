using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    public bool isBought;
    public int price;
    public GameObject instantiatedItem;
    public void DisplayItem(GameObject itemSpawn)
    {
        instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
    }

    public void BuyItems()
    {
        
    }
}
