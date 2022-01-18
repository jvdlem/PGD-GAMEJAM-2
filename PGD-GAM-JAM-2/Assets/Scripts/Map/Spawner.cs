using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Spawner : MonoBehaviour
{
    [SerializeField] List<GameObject> spawnList;
    public UnityEvent onWaveDone;
    public bool opened = false, spawned = false;

    public void Update()
    {
        if (spawned == true)
        {
            foreach (GameObject gameObject in spawnList)
            {
                if (gameObject.transform.childCount <= 0)
                {
                    spawnList.Remove(gameObject);
                    Destroy(gameObject);
                }
            }
            if (spawnList.Count >= 1)
            {
                opened = false;
            }
            if (spawnList.Count == 0 && opened == false)
            {
                opened = true;
                OpenRoom();
            }
        }

    }

    void OpenRoom()
    {
        onWaveDone.Invoke();
    }

    public void SpawnEnemy()
    {
        foreach (GameObject aEnemy in spawnList)
        {
            if (aEnemy != null)
            {
                GameObject parent = aEnemy;
               
                GameObject Test = Instantiate(aEnemy.GetComponent<Enemy>().myEnemy, aEnemy.transform.position, Quaternion.identity);
                Test.transform.SetParent(parent.transform);
            }
        }
        spawned = true;
    }
    public void Awake()
    {
        for (int i = 0; i < this.gameObject.transform.childCount; i++)
        {
            spawnList.Add(this.gameObject.transform.GetChild(i).gameObject);
        }
        //SpawnEnemy();

    }
}
