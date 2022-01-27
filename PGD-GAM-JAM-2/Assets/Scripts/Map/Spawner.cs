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
            int activeEnemies = 0;
            
            foreach (GameObject gameObject in spawnList)
            {
                if (gameObject.transform.childCount <= 0)
                {
                    gameObject.SetActive(false);
                }

                if (gameObject == gameObject.activeSelf)
                {
                    activeEnemies++;
                }
            }
            if (activeEnemies >= 1)
            {
                opened = false;
            }
            if (activeEnemies == 0 && opened == false)
            {
                foreach (GameObject gameObject in spawnList)
                {
                    Destroy(gameObject);
                }

                spawnList.Clear();
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
