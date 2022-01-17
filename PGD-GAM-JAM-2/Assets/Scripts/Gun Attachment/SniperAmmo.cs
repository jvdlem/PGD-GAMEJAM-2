using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAmmo : Projectille
{
    Vector3 startpos;
    float actualDmg;
    protected override void Awake()
    {
        base.Awake();
        startpos = this.transform.position;
    }
    void Update()
    {
        dmg = Mathf.RoundToInt((Vector3.Distance(startpos, this.transform.position)* Vector3.Distance(startpos, this.transform.position)) / 50) + mydmg;
        base.Update();


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other);
        if (other.GetComponent<Dummy>() != null)
        {
            other.GetComponent<Dummy>().GetDamage(other.gameObject);
        }
    }
}
