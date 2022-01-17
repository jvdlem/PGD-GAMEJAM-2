using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    public List<Text> itemNameAndPrice = new List<Text>();
    public GameObject shopCounterPos;
    public GameObject Player;
    public int price;
    public int index;
    public List<GameObject> shopItems;
    private Vector3 boughtItemPos;
    private PlayerHealthScript PlayerScript;

    public void Start()
    {
        boughtItemPos = shopCounterPos.transform.position;
        Player = GameObject.FindGameObjectWithTag("Player");
        PlayerScript = Player.GetComponent<PlayerHealthScript>();
    }

    public void DisplayItem(GameObject itemSpawn)
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/EnterShop");
        GameObject instantiatedItem;
        instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
        shopItems.Add(instantiatedItem);
        itemNameAndPrice[index].text = instantiatedItem.GetComponent<ShopStatDisplay>().GetShopStats();
        index++;
        price = instantiatedItem.GetComponent<ShopStatDisplay>().itemPrice;
    }
    public void BuyItems(int i)
    {
        //Buying items
        if (PlayerScript.coins >= price && shopItems[i].GetComponent<ShopStatDisplay>().isBought == false)
        {
            FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/BoughtItem");
            shopItems[i].transform.position = boughtItemPos;
            shopItems[i].GetComponent<ShopStatDisplay>().isBought = true;
            PlayerScript.coins -= shopItems[i].GetComponent<ShopStatDisplay>().itemPrice;
        }
        //Not enough money
        else if (PlayerScript.coins < price && shopItems[i].GetComponent<ShopStatDisplay>().isBought == false) FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/InsufficientMoney");
        //Item already bought
        else if (shopItems[i].GetComponent<ShopStatDisplay>().isBought == true) FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/NoMoreItem");
    }
}
