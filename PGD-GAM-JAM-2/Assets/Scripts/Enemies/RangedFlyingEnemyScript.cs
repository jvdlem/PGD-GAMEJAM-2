using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedFlyingEnemyScript : FlyingEnemyScript
{

    [SerializeField] readonly GameObject projectile; //Projectile which can be filled in

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Track(target); //Follow target
        FireOn(target); //Fire projectile at target

        FlyTo(target, flyingSpeed); //Fly in direction of target
    }

    /// <summary>Launches projectile at a target.</summary>
    private void FireOn(Vector3 targetVector) 
    {
        Instantiate(projectile, transform.position + target, transform.rotation);
    }
}
