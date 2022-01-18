using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Granade : Projectille
{
    List<ParticleSystem> myparticles = new List<ParticleSystem>();
    [SerializeField] private ParticleSystem Sparks;
    [SerializeField] private ParticleSystem Smoke;
    [SerializeField] private ParticleSystem explosion;
    [SerializeField] private GameObject Parent;
    [SerializeField] private float delay = 3;
    private float scale;
    [SerializeField]private float ExplodeTimer;
    [SerializeField]private bool onCollision = false;
    [SerializeField]private float radius = 15;
    private bool canExplode = false;
    private bool damageTaken = false;

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {
        myparticles.Add(Sparks);
        myparticles.Add(Smoke);
        myparticles.Add(explosion);
        Sparks.startDelay = delay;
        Smoke.startDelay = delay;
        explosion.startDelay = delay;
        ExplodeTimer = timer - 1;

    }

    public void UpdateScale(float explosionSize, bool OnHit, int add)
    {
        Parent.transform.localScale /= Parent.transform.localScale.x;
        if (add >= 1)
        {
            Parent.transform.localScale *= explosionSize;
                
        }
        onCollision = OnHit;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        if (ExplodeTimer >= 0)
        {
            ExplodeTimer -= Time.deltaTime;
            
            if (ExplodeTimer <= 0 && canExplode == true)
            {
                delay = 0;
                Sparks.startDelay = delay;
                Smoke.startDelay = delay;
                explosion.startDelay = delay;
                FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Grenade Launcher/Explosion/Grenade Explosion", this.gameObject.transform.position);

                Collider[] hitColliders = Physics.OverlapSphere(this.transform.position, radius);

                if (canExplode)
                {
                    foreach (var collider in hitColliders)
                    {
                        if (collider.gameObject.transform.GetComponent<EnemyBaseScript>() != null)
                        {
                            Debug.Log("Boom");
                            collider.gameObject.GetComponent<EnemyBaseScript>().TakeDamage((int)((radius) * 6 / (Vector3.Distance(this.transform.position, collider.gameObject.transform.position))));
                            Debug.Log(collider.gameObject.name + ": " + (int)((radius) * 6 / (Vector3.Distance(this.transform.position, collider.gameObject.transform.position))));
                        }

                    }
                }
                canExplode = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (onCollision && collision.transform.tag != "Projectile")
        {
            if (delay >= 1)
            {
                delay = 0;
                ExplodeTimer = 0;
                canExplode = true;
            }
        }
    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, radius);
    }

}
