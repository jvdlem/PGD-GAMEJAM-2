using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : Moenemies
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject arrow;
    public float arrowWaitTime =.5f;

    override public void Start()
    {
        base.Start();
        currentState = States.Chasing;
        attackSound = "event:/Enemy/Skeleton/Skeleton Attacks";
        deathSound = "event:/Enemy/Skeleton/Skeleton Death";
        hurtSound = "event:/Enemy/Skeleton/Skeleton Hurt";
        attack = "Shoot";
    }
    public override void Update()
    {
        soundPosition = this.gameObject.transform.position;
        base.Update();
    }
    public override void SearchRandomWalkPoint()
    {
        //Determine a random point in the Golems detection range 
        float randomZ = Random.Range(-detectionDistance * 1.5f, detectionDistance * 1.5f);
        float randomX = Random.Range(-detectionDistance * 1.5f, detectionDistance * 1.5f);

        walkPoint = new Vector3(this.gameObject.transform.position.x + randomX, this.gameObject.transform.position.y, this.gameObject.transform.position.y + randomZ);

        //Check if walkpoint is on the ground
        if (Physics.Raycast(walkPoint, -transform.up, 2f, groundLayer)) { walkPointSet = true; }
    }
    public override IEnumerator TimedAttack()
    {
        PlaySound(attackSound, soundPosition);
        AnimationTrigger(attack);
        yield return new WaitForSeconds(arrowWaitTime);
        Shoot();
        yield return new WaitForSeconds(attackTimer);
        isAttacking = false;
    }
    public void Shoot()
    {
        Vector3 direction = shootPoint.position - new Vector3(transform.position.x, shootPoint.position.y, transform.position.z) ;

        //Create a new instantiation of an arrow
        Rigidbody currentArrow = Instantiate(arrow, shootPoint.position, Quaternion.LookRotation(direction)).GetComponent<Rigidbody>();

        //Rotate the arrow
       // currentArrow.rotation = Quaternion.LookRotation(direction);

        currentArrow.AddForce(transform.forward * 10f, ForceMode.Impulse);
        currentArrow.AddForce(transform.up * .25f, ForceMode.Impulse);
    }

}
