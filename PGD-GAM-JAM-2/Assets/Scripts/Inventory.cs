using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    InventorySlot[] inventorySlots = new InventorySlot[10]; //Number of inventory slots

    int activeSlot; //Slot selected by player

    //Sets a new active slot
    public void SetSlot(int slot) 
    {
        activeSlot = slot;
    }
    
    //Handles input for inventory
    public void HandleInventory(GameObject item) 
    {
        if (Input.GetKey("e"))
        {
            inventorySlots[activeSlot].PickUpItem(item);
        }
        else if (Input.GetKey("q")) 
        {
            inventorySlots[activeSlot].DropItem(item);
        }
    }
}
