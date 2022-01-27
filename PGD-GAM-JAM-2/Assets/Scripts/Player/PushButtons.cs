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
        //Get LayerMask
        buttons = LayerMask.GetMask("Buttons");
    }
    private void Update()
    {
        //Check if you are looking at a button
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 2f, buttons))
        {
            buttonCanBePressed = true;
            if (Input.GetKeyDown(KeyCode.E))
            {
                getsPressed.ButtonGetsPressed(hit);
                FMODUnity.RuntimeManager.PlayOneShot("event:/MISC/ButtonPress", hit.transform.position); //Play sound effect for pressing button
            }
        }
        else buttonCanBePressed = false;
    }
}
