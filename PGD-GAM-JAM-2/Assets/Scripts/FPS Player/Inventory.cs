using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject barrelGunSlot;
    public GameObject sightGunSlot;
    public GameObject magazineGunSlot;
    public GameObject stockGunSlot;

    public GameObject barrelInventorySlot;
    public GameObject sightInventorySlot;
    public GameObject magazineInventorySlot;
    public GameObject stockInventorySlot;
    // Update is called once per frame
    void Update()
    {
    }

    public void GrabAttachment()
    {
    
    }

    public void Attach(GameObject currentAttachemnt, int slot)
    {
        switch (slot)
        {
            case 5:
                print("Why hello there good sir! Let me teach you about Trigonometry!");
                break;
            case 4:
                print("Hello and good day!");
                break;
            case 3:
                print("Whadya want?");
                break;
            case 2:
                print("Grog SMASH!");
                break;
            case 1:
                barrelGunSlot = currentAttachemnt;
                
                break;
            default:
                print("Incorrect Slot chozen.");
                break;
        }

    }

    public void Dettach()
    {
    
    }
}
