using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EnterShop : MonoBehaviour
{
    [SerializeField] ShopScript shopScript;
    [SerializeField] List<GameObject> itemSpawners = new List<GameObject>();
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            shopScript.PlayShopAudio();
            Destroy(this.gameObject);
            foreach (GameObject itemSpawners in itemSpawners)
            {
                shopScript.DisplayItem(itemSpawners);
            }
        }
    }
}
