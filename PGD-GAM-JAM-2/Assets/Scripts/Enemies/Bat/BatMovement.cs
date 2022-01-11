using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : MeleeFlyingEnemyScript
{

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = 100;
        Damage = 10;
    }

    public override void Update()
    {
        base.Update();
    }
}
