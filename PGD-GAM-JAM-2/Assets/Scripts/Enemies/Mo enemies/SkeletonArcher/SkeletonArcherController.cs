using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherController : Moenemies
{
    // Start is called before the first frame update
    [SerializeField] Transform shootPoint;
    [SerializeField] GameObject arrow;

    override public void Start()
    {
        base.Start();
        attackSound = "event:/Enemy/Skeleton/Skeleton Attacks";
        deathSound = "event:/Enemy/Skeleton/Skeleton Death";
        hurtSound = "event:/Enemy/Skeleton/Skeleton Hurt";
        attack = "Shoot";
        detectionDistance = 25f;
        attackDistance = 20f;
        Damage = 1;
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
        yield return new WaitForSeconds(attackTimer);
        Shoot();
        isAttacking = false;
    }  
    public void Shoot()
    {
        AnimationTrigger(attack);

        //Create a new instantiation of an arrow
        Rigidbody currentArrow = Instantiate(arrow, shootPoint.position, Quaternion.identity).GetComponent<Rigidbody>();

        //Rotate arrow
        Vector3 direction = new Vector3(transform.position.x, shootPoint.position.y, transform.position.z) - shootPoint.position;
        Vector3 lookDirection = direction.normalized;
        currentArrow.rotation = Quaternion.LookRotation(lookDirection);

        currentArrow.AddForce(transform.forward * 10f, ForceMode.Impulse);
        currentArrow.AddForce(transform.up * .25f, ForceMode.Impulse);
    }

}
