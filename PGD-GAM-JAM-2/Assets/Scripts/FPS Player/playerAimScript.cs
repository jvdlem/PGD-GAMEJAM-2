using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAimScript : MonoBehaviour
{
    public Animator anim;
    public bool aiming;

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
    }
}
