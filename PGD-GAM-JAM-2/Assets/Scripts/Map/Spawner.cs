using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnList;

   
    public void SpawnEnemy()
    {
        foreach (GameObject aGameobject in spawnList)
        {
            Debug.Log("Instantiete");
            Instantiate(aGameobject.GetComponent<Enemy>().myEnemy, aGameobject.transform.position,Quaternion.identity);
        }
    }
    void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            spawnList.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
        SpawnEnemy();
    }
}
