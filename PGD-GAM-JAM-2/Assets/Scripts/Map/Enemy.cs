using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject myEnemy;

    public void Update()
    {
        if (myEnemy == null)
        {
            Destroy(this.gameObject);
        }
    }
}
