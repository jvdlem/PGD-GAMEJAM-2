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
    [Header("Player")]
    public GameObject Player;
    private PlayerHealthScript PlayerScript;
    [Header("Music")]
    public FMODUnity.StudioEventEmitter audioEmitter;

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

    public void PlayShopAudio()
    {
        FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/Shop/EnterShop");
        audioEmitter.Play();
    }
}
