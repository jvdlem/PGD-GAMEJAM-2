using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GolemScript : Moenemies
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        die = "Die";
        attack = "Punch";
    }
}
