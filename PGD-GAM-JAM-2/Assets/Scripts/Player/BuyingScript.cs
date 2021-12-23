using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class BuyingScript : MonoBehaviour
{
    [SerializeField] DisplayItems displayItems;
    [SerializeField] PlayerHealthScript playerHealthScript;
    LayerMask shopButtons;
    private void Start()
    {
        shopButtons = LayerMask.GetMask("ShopButtons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, shopButtons))
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
