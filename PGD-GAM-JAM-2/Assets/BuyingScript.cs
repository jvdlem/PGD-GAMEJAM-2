using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    private void Update()
    {
        LayerMask interactibles = LayerMask.GetMask("Interactible");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool objectHit = Physics.Raycast(ray, out RaycastHit hit, interactibles);
        if (objectHit)
        {
            if (Input.GetKeyDown("e"))
            {
                displayItems.BuyItems(hit);
                //player.money -= displayItems.items.price
            }
        }

        
    }
}
