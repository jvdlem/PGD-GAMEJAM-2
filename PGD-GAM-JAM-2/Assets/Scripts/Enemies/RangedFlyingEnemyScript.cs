using UnityEngine;

public class RangedFlyingEnemyScript : FlyingEnemyScript
{

    [SerializeField] readonly GameObject projectile; //Projectile which can be filled in

    // Update is called once per frame
    public override void Update()
    {
        base.Update();

        Track(target); //Follow target
        FireOn(targetObject); //Fire projectile at target

        FlyTo(target, flyingSpeed); //Fly in direction of target
    }

    /// <summary>Launches projectile at a target.</summary>
    private void FireOn(GameObject targetObject) 
    {
        Instantiate(projectile, transform.position + target, transform.rotation);
    }
}
