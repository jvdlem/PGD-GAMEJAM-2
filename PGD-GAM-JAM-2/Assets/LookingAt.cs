using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookingAt : MonoBehaviour
{
    public int AmountHit;
    public float minDistFromObject = 5;
    public GameObject lookedAtObject;
    LayerMask Watch;

    public void Start()
    {
        Watch = LayerMask.GetMask("Watch");
    }
    public void Update()
    {

        
        Vector3 pos = transform.position;
        AmountHit = 0;



        //for (int x = -1; x <= 1; x++)
        //{
        //    for (int y = -1; y <= 1; y++)
        //    {
        //        Ray ray = new Ray(pos + new Vector3(x * 0.1f, y * 0.07f, 0), transform.TransformDirection(Vector3.forward));
        //        RaycastHit hit;
        //        Debug.DrawRay(pos + new Vector3(x * 0.1f, y * 0.07f, 0), transform.TransformDirection(Vector3.forward) * minDistFromObject, Color.yellow);

        //        if (Physics.Raycast(ray, out hit, minDistFromObject, Watch))
        //        {
                    
        //            lookedAtObject.GetComponentInChildren<WatchUi>().lookedAt = true;
        //            AmountHit++;
        //        }
                
        //        if (AmountHit == 0) { lookedAtObject.GetComponentInChildren<WatchUi>().lookedAt = false; }
        //    }
            
        //}
        Ray ray = new Ray(pos , transform.TransformDirection(Vector3.forward));
        RaycastHit hit;
        Debug.DrawRay(pos , transform.TransformDirection(Vector3.forward) * minDistFromObject, Color.yellow);

        if (Physics.Raycast(ray, out hit, minDistFromObject, Watch))
        {

            lookedAtObject.GetComponentInChildren<WatchUi>().lookedAt = true;
            
        }

        else  lookedAtObject.GetComponentInChildren<WatchUi>().lookedAt = false; 
    }
}
