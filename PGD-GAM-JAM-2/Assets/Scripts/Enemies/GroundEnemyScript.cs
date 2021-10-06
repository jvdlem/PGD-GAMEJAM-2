using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyScript : EnemyBaseScript
{
    private NavMeshAgent navMeshAgent;
    public NavMeshSurface Ground;
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Space))
        //{
            Ground.BuildNavMesh();
        //}

        navMeshAgent.destination = Player.transform.position;
    }
}
