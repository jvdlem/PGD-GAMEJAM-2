using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    public GameObject shopCounterPos;
    public bool isBought;
    public int price = 5;
    public GameObject instantiatedItem;
    private Vector3 boughtItemPos;

    public void Start()
    {
        boughtItemPos = shopCounterPos.transform.position;
    }

    public void DisplayItem(GameObject itemSpawn)
    {
        instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
    }

    public void BuyItems(RaycastHit hit)
    {
        hit.transform.position = boughtItemPos;
    }
}
