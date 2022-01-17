using UnityEngine;

public class DragonController : MeleeFlyingEnemyScript
{

    public override void Start()
    { 
        base.Start();

        Tier = 1;

        Health = 100;
        Damage = 10;

        currentState = States.Chasing; //Enemy starts patrolling
    }
}
