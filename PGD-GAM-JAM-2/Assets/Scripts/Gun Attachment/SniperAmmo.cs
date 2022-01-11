using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperAmmo : Projectille
{
    void Update()
    {
        base.Update();
        dmg += 0.1f;
    }
}
