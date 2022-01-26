using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class GroundEnemyScript : EnemyBaseScript
{
    public NavMeshAgent navMeshAgent;
    private bool playerInVision;

    private Vector3 targetPosition;
    public float checkForPlayerDistance = 15;
    
    public float WalkSpeed;
    public float RotateSpeed;
    public float AttackRange;

    public override void Start()
    {
        base.Start();

        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.speed = WalkSpeed;
        navMeshAgent.angularSpeed = RotateSpeed;
    }

    public override void Update()
    {
        base.Update();

        DetectPlayer();
        EnemyMovement();
    }

    //Check if there is anything in the way of the enemy and the player, change target depending on result
    public void DetectPlayer()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, checkForPlayerDistance))
        {
            Color color = Color.white;
            if (hit.transform == Player.transform)
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
        }
    }

    public void EnemyMovement()
    {
        float Distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z));
        if (Distance >= AttackRange && playerInVision == true)
        {
            //Enemy moves to player
            navMeshAgent.destination = targetPosition;
        }
        else if (Distance < AttackRange && playerInVision == true)
        {
            //Enemy stops when in attack range
            navMeshAgent.destination = transform.position;
        }
        else if (playerInVision == false)
        {
            //Enemy moves towards players last known position
            navMeshAgent.destination = targetPosition;
        }
    }

    //Draw a sphere at the Target Position
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(targetPosition, 1f);
    }
}
