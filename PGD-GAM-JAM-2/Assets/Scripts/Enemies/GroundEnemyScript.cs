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
        //Raycast from this enemy to the player position. Checks if there are objects in the way, also has a max vision distance.
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (Player.transform.position - transform.position), out hit, checkForPlayerDistance))
        {
            Color color = Color.white;
            //If there are no objects in the way and player is visible:
            if (hit.transform == Player.transform)
            {
                color = Color.green;
                playerInVision = true;
                targetPosition = Player.transform.position;
            }
            //Else there are objects in the way and player is not visible
            else
            {
                playerInVision = false;
                color = Color.red;
            }

            //Draw a debug ray from this enemy to the player, color depends on if the player is visible or not
            Debug.DrawRay(transform.position, Player.transform.position - transform.position, color);
        }
    }

    public void EnemyMovement()
    {
        //Calculate the disctance between this enemy and the player, ignores the Y axis
        float Distance = Vector3.Distance(new Vector3(transform.position.x, 0, transform.position.z), new Vector3(targetPosition.x, 0, targetPosition.z));

        //If statesments choosing enemy AI behaviour, depending on distance to player, its attack range and if the player is in vision

        //If player is out of attack range, but in vision:
        if (Distance >= AttackRange && playerInVision == true)
        {
            //Enemy moves to player
            navMeshAgent.destination = targetPosition;
        }
        //If player is in attack range and visible:
        else if (Distance < AttackRange && playerInVision == true)
        {
            //Enemy stops when in attack range
            navMeshAgent.destination = transform.position;
        }
        //If player is out of vision:
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
