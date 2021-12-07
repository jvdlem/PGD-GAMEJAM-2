using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] PlayerHealthScript playerHealthScript;
    LayerMask interactibles;
    private void Start()
    {
        interactibles = LayerMask.GetMask("Interactible");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, interactibles))
        {
            if (Input.GetKeyDown(KeyCode.E) && playerHealthScript.coins >= displayItems.price)
            {
                displayItems.BuyItems(hit);
                playerHealthScript.coins -= displayItems.price;
            }
        }
    }

    public void BuyItemsVR()
    {
        RaycastHit hit;
        if(this.GetComponent<XRRayInteractor>().TryGetCurrent3DRaycastHit(out hit)) displayItems.BuyItems(hit);
        playerHealthScript.coins -= displayItems.price;
    }
}
