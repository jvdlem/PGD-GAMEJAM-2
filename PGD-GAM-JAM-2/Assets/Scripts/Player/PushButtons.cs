using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtons : MonoBehaviour
{
    [SerializeField] PlayerHealthScript playerHealthScript;
    LayerMask buttons;
    private void Start()
    {
        buttons = LayerMask.GetMask("Buttons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 10f, buttons))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.rigidbody.velocity += Vector3.down * 100;
            }
        }
    }
}
