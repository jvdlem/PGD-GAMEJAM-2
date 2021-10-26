using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : FlyingEnemyScript
{
   
    public override void Update()
    {
        base.Update();

        TrackObject(targetObject); //Follow target
        SetRotation(new Vector3(target.x, 0, target.z)); //Rotation is set to target without y-axis 

        FlyTo(target, flyingSpeed); //Fly in direction of target
    }
}
