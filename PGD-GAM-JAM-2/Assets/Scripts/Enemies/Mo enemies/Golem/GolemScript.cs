using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : Moenemies
{
    // Start is called before the first frame update
    private bool attackBool = false;
    public override void Start()
    {
        attackTimer = 2f;
        base.Start();
    }

    public override void Update()
    {
        RandomAttackVariations("Punch", "Slam");
        base.Update();
    }
}
