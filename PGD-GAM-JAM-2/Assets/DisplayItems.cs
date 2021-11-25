using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    
    public void DisplayItem()
    {
        GameObject instantiatedEnemy = Instantiate(items[1], this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
    }
}
