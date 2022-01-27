using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimScript : MonoBehaviour
{
    public GameObject crosshair;
    public Animator anim;
    public bool aiming;
    public static bool isAiming;
    public static float reloadTimer = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !aiming)
        {
            anim.SetBool("aiming", true);
            aiming = true;
            isAiming = true;
        }
        else if (Input.GetButtonDown("Fire2") && aiming)
        {
            anim.SetBool("aiming", false);
            aiming = false;
            isAiming = false;
        }

        if (Pistol.reloading)
        {
            if (reloadTimer <= 0.2f) FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Reload");
            reloadTimer -= Time.deltaTime;
            aiming = false;
            anim.SetBool("aiming", false);
            anim.SetBool("reloading", true);
        }
        else if (reloadTimer <= 0)
        {
            anim.SetBool("reloading", false);
            reloadTimer = 1;
        }






    }
}
