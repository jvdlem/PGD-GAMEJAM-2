using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    public GameObject shopCounterPos;
    public GameObject Player;
    private PlayerHealthScript PlayerScript;
    public bool isBought;
    public int price = 5;
    
    public List<GameObject> shopItems;
    
    private Vector3 boughtItemPos;

    public void Start()
    {
        boughtItemPos = shopCounterPos.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerHealthScript>();
    }

    public void DisplayItem(GameObject itemSpawn)
    {
        GameObject instantiatedItem;
     instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
     shopItems.Add(instantiatedItem);


    }

    public void BuyItems(int i)
    {
        if (PlayerScript.coins >= price) 
        { 
            shopItems[i].transform.position = boughtItemPos;
            PlayerScript.coins -= price;
        }
    }
}
