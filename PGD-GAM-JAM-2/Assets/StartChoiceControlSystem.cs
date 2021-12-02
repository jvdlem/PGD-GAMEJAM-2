using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartChoiceControlSystem : MonoBehaviour
{
    [SerializeField]
    public FPSUI FPSUI;
    [SerializeField]
    public FPSPlayerMovement FPSPlayer;
    [SerializeField]
    public ChracterMovmentHelper VRPlayer;
    [SerializeField]
    public FPSUI VRUI;



    public bool VR = false;
    public bool Keyboard = false;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void VRControls()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
       
        //VRUI.ToggleVRUI();
        FPSPlayer.ToggleFPSPlayer();
    }
   
    public void KeyboardControls()
    {
        gameObject.SetActive(false);
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        FPSUI.ToggleFPSUI();
        VRPlayer.ToggleVRPlayer();

    }
}
