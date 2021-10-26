using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyScript : EnemyBaseScript
{
    public NavMeshAgent navMeshAgent;
    private bool playerInVision;

    private Vector3 targetPosition;
    private float checkForPlayerDistance = 15;
    
    public float WalkSpeed;
    public float RotateSpeed;
    public float AttackRange;

    public override void Start()
    {
        base.Start();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = AttackRange;
        navMeshAgent.angularSpeed = RotateSpeed;
    }

    public override void Update()
    {
        base.Update();

        RaycastHit hit;
        if(Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, checkForPlayerDistance))
        {
            Color color = Color.white;
            if(hit.transform == Player.transform)
            {
                color = Color.green;
                playerInVision = true;
                targetPosition = Player.transform.position;
            }
            else
            {
                playerInVision = false;
                color = Color.red;
            }
            Debug.DrawRay(transform.position, Player.transform.position - transform.position, color);

            float Distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z));
            if (Distance >= AttackRange && playerInVision == true) 
            { 
                navMeshAgent.destination = targetPosition; 
            }
            else if(Distance < AttackRange && playerInVision == true)
            { 
                navMeshAgent.destination = transform.position; 
            }
            else if (playerInVision == false)
            {
                navMeshAgent.destination = targetPosition;
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }
}
