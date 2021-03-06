using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaveSpiderController : Moenemies
{
    // Start is called before the first frame update

    public bool canDamage => currentState != States.Death;
    override public void Start()
    {
        base.Start();
        attackSound = "event:/Enemy/Spider/Spider Attack";
        deathSound = "event:/Enemy/Spider/Spider Death";
        hurtSound = "event:/Enemy/Spider/Spider Hurt";
        attack = "Lunge";
        attackTimer = 1f;
    }

    public override void Update()
    {
        soundPosition = this.gameObject.transform.position;
        base.Update();
    }
}