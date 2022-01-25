using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    public FPSPlayerMovement FPSPlayer;
    [SerializeField]
    public ChracterMovmentHelper VRPlayer;
    [SerializeField]
    public Pistol VRGun;

    public bool VR = false;
    public bool Keyboard = false;

    // Start is called before the first frame update
    void Start()
    {
        int controlSystem = PlayerPrefs.GetInt("ControlSystem");
        if (controlSystem == 0)
        {
            VRControls();
        }
        else if (controlSystem == 1)
        {
            KeyboardControls();
        }
    }

    public void VRControls()
    {
        Debug.Log("ControlManager VR");
        VR = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSPlayer.ToggleFPSPlayer();
    }

    public void KeyboardControls()
    {
        Debug.Log("ControlManager Keyboard");
        Keyboard = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        VRGun.ToggleVRPistol();
        VRPlayer.ToggleVRPlayer();
    }
}
