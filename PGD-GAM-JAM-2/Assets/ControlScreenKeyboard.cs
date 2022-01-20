using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlScreenKeyboard : MonoBehaviour
{
    private void Start()
    {
        this.gameObject.SetActive(false);
    }

    public void turnOn()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        this.gameObject.SetActive(true);
    }

    public void turnOff()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        this.gameObject.SetActive(false);
    }
}
