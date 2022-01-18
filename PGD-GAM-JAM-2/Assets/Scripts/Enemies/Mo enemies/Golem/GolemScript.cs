using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : Moenemies
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        attackSound = "event:/Enemy/Golem/GolemAttackSwing";
        deathSound = "event:/Enemy/Golem/GolemDeath";
        hurtSound = "event:/Enemy/Golem/GolemHurt";
        attackTimer = 2f;
        Damage = 2;
    }
    public override void Update()
    {
        soundPosition = this.gameObject.transform.position;
        RandomAttackVariations("Punch", "Slam");
        base.Update();
    }
}
