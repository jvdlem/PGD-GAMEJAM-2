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
        dmg = Mathf.RoundToInt((Vector3.Distance(startpos, this.transform.position)* Vector3.Distance(startpos, this.transform.position)) / 45) + mydmg;
        base.Update();


    }
}
