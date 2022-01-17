using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AmmoSocket : SocketCheck
{
    public GameObject ammo;

    public void SetAmmo(GameObject someAmmo)
    {
        ammo = someAmmo;
    }
    private void Update()
    {
        if(attached == 1)
        {
            ReloadAmmo();
        }
    }
    public void ReloadAmmo()
    {
        if (ammo != null)
        {
            GameObject newAmmo = Instantiate(ammo, this.transform.position, this.transform.rotation);
            newAmmo.GetComponent<Rigidbody>().isKinematic = false;
            newAmmo.GetComponent<Rigidbody>().useGravity = true;
        }
    }
}
