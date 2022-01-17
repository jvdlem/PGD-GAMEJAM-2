using UnityEngine;

public class DragonController : MeleeFlyingEnemyScript
{

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = 100;
        Damage = 10;

        currentState = States.Patrolling; //Enemy starts patrolling
    }
}
