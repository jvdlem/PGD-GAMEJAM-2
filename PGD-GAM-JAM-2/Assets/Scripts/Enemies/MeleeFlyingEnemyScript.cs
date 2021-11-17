using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeFlyingEnemyScript : FlyingEnemyScript
{

    public override void Update()
    {
        base.Update();

        ChargeAt(targetObject); //Dive at target
        SetRotation(target); //Aim at target

        FlyTo(target, flyingSpeed); //Fly in direction of target
    }

    /// <summary>Charge at a specified target</summary>
    private Vector3 ChargeAt(GameObject targetObject) 
    {
        target = targetObject.transform.position - transform.position; //Position vectors directly compared to each other
        target.Normalize();

        return target;
    }

    protected void OnTriggerEnter(Collider other)
    {
        transform.position = new Vector3(other.gameObject.transform.position.x, 
            25, other.gameObject.transform.position.z); //Reset position above target
    }
}
