using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] PlayerHealthScript playerHealthScript;
    private void Update()
    {
        LayerMask interactibles = LayerMask.GetMask("Interactible");
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        bool objectHit = Physics.Raycast(ray, out RaycastHit hit, interactibles);
        if (objectHit)
        {
            if (Input.GetKeyDown("e") && playerHealthScript.coins >= displayItems.price)
            {
                displayItems.BuyItems(hit);
                playerHealthScript.coins -= displayItems.price;
            }
        }

        
    }
}
