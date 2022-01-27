using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScreenKeyboard : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void TurnOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.gameObject.SetActive(true);
    }

    public void TurnOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    }
}
