using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileEnemyDeathScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Invoke("Destroy",5.0f);
    }

    void Destroy() 
    {
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
