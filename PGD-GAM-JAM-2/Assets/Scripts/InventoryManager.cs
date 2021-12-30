using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{

    [SerializeField] GameObject inventory;

    void Update()
    {

        // Makes inventory visible to player.
        if (Input.GetKey("e"))
        {
            inventory.SetActive(true);
            MouseLook.inventoryActive = true;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
        else 
        {
            inventory.SetActive(false);
            MouseLook.inventoryActive = false;
        }
    }
}
