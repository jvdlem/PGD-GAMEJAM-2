using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtons : MonoBehaviour
{
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
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.rigidbody.velocity += Vector3.down * 100;
            }
        }
    }
}
