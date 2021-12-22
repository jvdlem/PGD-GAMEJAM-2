using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class inventoryHud : Inventory
{
    [SerializeField] public Image[] imageList;


    public void start()
    {
        gameObject.SetActive(false);
    }

    public override void Update()
    {
        base.Update();

        if (attachements[slot] != null)
        {
            imageList[slot].enabled = true;
        }
        else
        {
            imageList[slot].enabled = false;
        }
    }


    public void toggleHudOff()
    {
        gameObject.SetActive(false);
    }
    public void toggleHudOn()
    {
        gameObject.SetActive(true);
    }
}
