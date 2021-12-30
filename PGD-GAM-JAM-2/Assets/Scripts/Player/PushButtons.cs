using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtons : MonoBehaviour
{
    LayerMask buttons;
    public bool buttonCanBePressed;
    private void Start()
    {
        buttons = LayerMask.GetMask("Buttons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 30f, buttons))
        {
            buttonCanBePressed = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                hit.rigidbody.velocity += Vector3.left * 100;
            }
        }
        else buttonCanBePressed = false;
        Debug.Log(buttonCanBePressed);
    }
}
