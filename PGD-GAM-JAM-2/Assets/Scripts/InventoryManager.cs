using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject inventory;

    void Update()
    {

        // Makes inventory visible to player.
        if (Input.GetKey("u"))
        {
            inventory.SetActive(true);
        }
        else 
        {
            inventory.SetActive(false);
        }
    }
}
