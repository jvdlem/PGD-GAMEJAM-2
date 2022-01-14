using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CaveSpiderController : Moenemies
{
    // Start is called before the first frame update
    override public void Start()
    {
        base.Start();
        attack = "Lunge";
    }
}