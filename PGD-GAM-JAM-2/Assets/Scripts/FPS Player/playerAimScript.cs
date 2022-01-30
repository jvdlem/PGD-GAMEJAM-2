using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimScript : MonoBehaviour
{
    public GameObject crosshair;
    public Animator anim;
    public bool aiming, allowPickUp;
    public static bool isAiming;
    public static float reloadTimer = 1;
    public float pickUpTimer = 0.5f;
    public Camera aCamera;
    private int zoom = 10;
    private int noZoom = 60;
    private bool zooming = false;
    [SerializeField] protected float desiredDuration = 0.5f;
    protected float elapsedTime = 3;
    private float startFOV;
    private float endFOV;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !aiming)
        {
            anim.SetBool("aiming", true);
            aiming = true;
            isAiming = true;
            elapsedTime = 0;
            MoveCamera();
        }
        else if (Input.GetButtonDown("Fire2") && aiming)
        {
            anim.SetBool("aiming", false);
            aiming = false;
            isAiming = false;
            pickUpTimer = 0.5f;
            elapsedTime = 0;
            MoveCameraBack();
        }

        if (Pistol.reloading)
        {
            if (reloadTimer <= 0.2f) FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Reload");
            reloadTimer -= Time.deltaTime;
            MoveCameraBack();
            aiming = false;
            anim.SetBool("aiming", false);
            anim.SetBool("reloading", true);
        }
        else if (reloadTimer <= 0)
        {
            anim.SetBool("reloading", false);
            reloadTimer = 1;
        }
        if (aiming) pickUpTimer = 0.5f;
        pickUpTimer -= Time.deltaTime;
        if (pickUpTimer <= 0) allowPickUp = true;
        else allowPickUp = false;
        ZoomPistol();
    }

    void ZoomPistol()
    {
        if (isAiming && elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            aCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, percantageComplete);
        }
        else if(elapsedTime < 2)
        {
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            aCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, percantageComplete);
        }
    }
    public void MoveCamera()
    {
        elapsedTime = 0;
        startFOV = aCamera.fieldOfView;
        endFOV = zoom;
    }

    public void MoveCameraBack()
    {
        elapsedTime = 0;
        startFOV = aCamera.fieldOfView;
        endFOV = noZoom;
    }
    
}
