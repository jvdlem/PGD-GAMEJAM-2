using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtons : MonoBehaviour
{
    LayerMask buttons;
    Vector3 pushforce;
    private void Start()
    {
        buttons = LayerMask.GetMask("Buttons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, buttons))
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("button pressed");
                hit.rigidbody.velocity += Vector3.down * 100;
            }
        }
    }
}
