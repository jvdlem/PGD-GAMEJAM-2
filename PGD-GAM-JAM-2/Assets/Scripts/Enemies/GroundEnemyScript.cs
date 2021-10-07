using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyScript : EnemyBaseScript
{
    private NavMeshAgent navMeshAgent;
    public NavMeshSurface Ground;

    private Vector3 targetPosition;
    private float checkForPlayerDistance = 100;
    
    private float attackRange = 5;

    void Start()
    {
        Ground.BuildNavMesh();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        RaycastHit hit;
        if(Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, checkForPlayerDistance))
        {
            Color color = Color.white;
            if(hit.transform == Player.transform)
            {
                color = Color.green;
                targetPosition = Player.transform.position;
            }
            else
            {
                color = Color.red;
            }
            Debug.DrawRay(transform.position, Player.transform.position - transform.position, color);

            float Distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z));
            if (Distance >= attackRange)navMeshAgent.destination = targetPosition;
            else navMeshAgent.destination = transform.position;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }
}
