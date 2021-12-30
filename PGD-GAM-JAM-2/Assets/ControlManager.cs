using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlManager : MonoBehaviour
{
    [SerializeField]
    public StartChoiceControlSystem Startchoicecontrolsystem;

    [SerializeField]
    public FPSUI FPSUI;
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
        else if (controlSystem == 3)
        {
            Startchoicecontrolsystem.ToggleChoiceScreen();
        }
    }

    public void VRControls()
    {
        VR = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSPlayer.ToggleFPSPlayer();
        PlayerPrefs.SetInt("ControlSystem", 3);
    }

    public void KeyboardControls()
    {
        Keyboard = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        VRGun.ToggleVRPistol();
        VRPlayer.ToggleVRPlayer();
        FPSUI.ToggleFPSUI();
        PlayerPrefs.SetInt("ControlSystem", 3);
    }
}
