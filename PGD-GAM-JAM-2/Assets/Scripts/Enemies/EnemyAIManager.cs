using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIManager : MonoBehaviour
{
    public NavMeshSurface Ground;

    void Start()
    {
        Ground = GetComponent<NavMeshSurface>();
        if (Ground == null) Ground = gameObject.AddComponent<NavMeshSurface>();

        GenerateNavMesh();
    }

    private void Update()
    {
/*        if (Input.GetKeyDown(KeyCode.Space))
        {
            GenerateNavMesh();
        }*/
    }

    public void GenerateNavMesh()
    {
        Ground.BuildNavMesh();
    }
}
