using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class PushButtons : MonoBehaviour
{
    LayerMask buttons;
    [SerializeField] GetsPressed getsPressed;
    public bool buttonCanBePressed;
    private void Start()
    {
        buttons = LayerMask.GetMask("Buttons");
    }
    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 1f, buttons))
        {
            buttonCanBePressed = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                getsPressed.ButtonGetsPressed(hit);
                FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/ButtonPress", hit.transform.position);
            }
        }
        else buttonCanBePressed = false;
    }
}
