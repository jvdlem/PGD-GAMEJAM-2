using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopScript : MonoBehaviour
{
    [Header("Items")]
    [SerializeField] public List<GameObject> items = new List<GameObject>();
    public List<GameObject> shopItems;

    [Header("Item Information")]
    public List<Text> itemNameAndPrice = new List<Text>();
    public int price;
    public int index;
    public GameObject shopCounterPos;
    private Vector3 boughtItemPos;
    private bool doneLooking;

    [Header("Player")]
    public GameObject Player;
    private PlayerHealthScript PlayerScript;

    [Header("Music")]
    public FMODUnity.StudioEventEmitter audioEmitter;

    public void Start()
    {
        //Set position for where the item should appear when bought
        boughtItemPos = shopCounterPos.transform.position;
    }

    public void Update()
    {
        if (ControlManager.doneChoosing && !doneLooking)
        {
            //Get player components
            Player = GameObject.FindGameObjectWithTag("Player");
            PlayerScript = Player.GetComponent<PlayerHealthScript>();
            doneLooking = true;
        }
    }

    public void DisplayItem(GameObject itemSpawn)
    {
        //Play sound effect
        FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/EnterShop");

        //Spawn 3 random items from the list
        GameObject instantiatedItem;
        instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], itemSpawn.transform.position, itemSpawn.transform.rotation);
        shopItems.Add(instantiatedItem);
        //Display the description and price for each item
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
