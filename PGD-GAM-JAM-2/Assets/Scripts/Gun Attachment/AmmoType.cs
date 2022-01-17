using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class AmmoType : MonoBehaviour
{
    public GameObject myAmmo;
    public int AmmoAmount = 1;
    public int maxAmmo;
   
    public GameObject GetAmmoType()
    {
        return myAmmo;
    }
    public int GetAmmoAmount()
    {
        return AmmoAmount;
    }
    public void Awake()
    {
       AmmoAmount = maxAmmo;
    }
    public GameObject GetMyObject()
    {
        return this.gameObject;
    }
    public void removeAmmoAmount(int amount)
    {
        AmmoAmount -= amount;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (AmmoAmount <= 0 && collision.transform.tag == "Ground")
        {
            Object.Destroy(this.gameObject);

        }
    }


}
