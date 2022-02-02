using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChoiceControlSystem : MonoBehaviour
{
    [SerializeField]
    public FPSUI FPSUI;
    [SerializeField]
    public ControlScreenKeyboard controlscreenkeyboard;
    [SerializeField]
    public FPSPlayerMovement FPSPlayer;
    [SerializeField]
    public ChracterMovmentHelper VRPlayer;
    [SerializeField]
    public Pistol VRGun;
    [SerializeField]
    public crosshairScript reticle;

    public GameObject gun;
    private bool isActive = false;

    public bool VR = false;
    public bool Keyboard = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void VRControls()
    {
        VR = true;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSPlayer.ToggleFPSPlayer();
        PlayerPrefs.SetInt("ControlSystem", 0);
    }

    public void KeyboardControls()
    {
        Keyboard = true;
        this.gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        VRGun.ToggleVRPistol();
        VRPlayer.ToggleVRPlayer();
        controlscreenkeyboard.TurnOn();
        if (FPSUI != null) FPSUI.ToggleFPSUI();
        reticle.ToggleFPSCrosshair();
        PlayerPrefs.SetInt("ControlSystem", 1);

    }

    public void ToggleChoiceScreen()
    {
        this.gameObject.SetActive(true);
        gun.GetComponent<Pistol>().isInMenu = isActive;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
