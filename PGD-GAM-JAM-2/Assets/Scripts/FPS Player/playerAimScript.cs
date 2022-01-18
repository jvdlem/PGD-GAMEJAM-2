using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimScript : MonoBehaviour
{
    public Animator anim;
    public bool aiming, reloading;
    public float reloadTimer = 1;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire2") && !aiming)
        {
            anim.SetBool("aiming", true);
            aiming = true;
        }
        else if (Input.GetButtonDown("Fire2") && aiming)
        {
            anim.SetBool("aiming", false);
            aiming = false;
        }

        if (reloadTimer >= 1 && Input.GetKeyDown("r"))
        {
            reloading = true;
        }

        if (reloading)
        {
            reloadTimer -= Time.deltaTime;
            anim.SetBool("aiming", false);
            aiming = false;
            anim.SetBool("reloading", true);
        }

        if (reloadTimer <= 0)
        {
            anim.SetBool("reloading", false);
            reloading = false;
            reloadTimer = 1;
        }


    }
}
