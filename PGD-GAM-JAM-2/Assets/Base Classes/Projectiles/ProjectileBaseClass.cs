using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileBaseClass : MonoBehaviour
{
    public Vector3 speed;
    public int damage;
    private Rigidbody projectileRigidbody;

    // Start is called before the first frame update
    void Start()
    {
        projectileRigidbody = GetComponent<Rigidbody>();
        if (projectileRigidbody == null) projectileRigidbody = gameObject.AddComponent<Rigidbody>();
            
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed;
    }

    private void OnCollisionEnter(Collision collision)
    {

    }
}
