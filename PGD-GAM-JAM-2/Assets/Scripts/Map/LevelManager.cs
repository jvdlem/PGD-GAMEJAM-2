using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public GameObject[] Levels;
    public int CurrentActiveLevel;
    public Transform ElevatorPosition;

    // Start is called before the first frame update
    void Start()
    {
        CurrentActiveLevel = 0;  //CurrentActiveLevel starts with 0, which is the main menu
    }

    public void LoadLevel(int LevelId)
    {
        UnloadAllLevels();

        CurrentActiveLevel = LevelId;
        Instantiate(Levels[LevelId], ElevatorPosition.position, ElevatorPosition.rotation, this.transform);
    }

    public void UnloadAllLevels()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }
    }
}
