using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    public bool roomIsCleared;
    public GameObject spawner;
    Vector3 startPos;
    // Start is called before the first frame update
    void Start()
    {
        startPos = spawner.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (roomIsCleared)
        {
            transform.position = startPos;
        }
    }
}
