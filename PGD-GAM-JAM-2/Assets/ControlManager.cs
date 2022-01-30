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

    [SerializeField] private crosshairScript Croshare;
    public GameObject crosshair;
    int controlSystem;
    public bool VR, Keyboard;
    public static bool doneChoosing;

    // Start is called before the first frame update
    void Awake()
    {
        controlSystem = PlayerPrefs.GetInt("ControlSystem");
        crosshair = GameObject.FindGameObjectWithTag("Crosshair");
        Croshare = crosshair.GetComponent<crosshairScript>();

        if (controlSystem == 0) VRControls();
        else if (controlSystem == 1) KeyboardControls();
    }

    public void VRControls()
    {
        VR = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSPlayer.ToggleFPSPlayer();
        doneChoosing = true;
    }

    public void KeyboardControls()
    {
        Keyboard = true;
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        VRGun.ToggleVRPistol();
        VRPlayer.ToggleVRPlayer();
        Croshare.ToggleFPSCrosshair();
        doneChoosing = true;
    }
}
