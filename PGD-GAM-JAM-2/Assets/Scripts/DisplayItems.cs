using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisplayItems : MonoBehaviour
{
    [SerializeField] List<GameObject> items = new List<GameObject>();
    
    public void DisplayItem()
    {
        GameObject instantiatedItem = Instantiate(items[Random.Range(0, items.Count)], this.gameObject.transform.position, this.gameObject.transform.rotation) as GameObject;
        
    }
}
