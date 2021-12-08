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
        interactibles = LayerMask.GetMask("ShopButtons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, interactibles))
        {
            if (Input.GetKeyDown(KeyCode.E) && playerHealthScript.coins >= displayItems.price)
            {
                hit.rigidbody.velocity += Vector3.down * 100;
            }
        }
    }

    public void BuyItemsVR()
    {
        playerHealthScript.coins -= displayItems.price;
    }
}
