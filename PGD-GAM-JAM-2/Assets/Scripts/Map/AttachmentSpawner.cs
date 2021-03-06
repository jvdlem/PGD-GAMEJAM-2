using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentSpawner : Spawner
{
    bool checking_Player = true;
    [SerializeField] private GameObject spawn;
    [SerializeField] float radius = 10;
    [SerializeField] ParticleSystem spawnParticle;
    [SerializeField] List<GameObject> spawnList;
    // Start is called before the first frame update
    void Start()
    {
        this.spawn = spawnList[Random.Range(0, spawnList.Count)];
    }

    // Update is called once per frame
    void Update()
    {
        if (checking_Player == true)
        {
            checkforspawn(this.transform.position,radius);
        }
    }
    void checkforspawn(Vector3 center, float radius)
    {
        Collider[] hitColliders = Physics.OverlapSphere(center, radius);
        foreach (var hitCollider in hitColliders)
        {
            if (hitCollider.tag == "Player")
            {
                
                spawnParticle.Play();
                Instantiate(spawn, this.transform.position + new Vector3(0, 2, 0), Quaternion.identity);
                checking_Player = false;
            }
        }
    }
}
