using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshairScript : MonoBehaviour
{
    private RectTransform reticle;
    [SerializeField] private Pistol pistolle;
    public GameObject Pistol;
    public GameObject subCrosshair;
    public bool isActive = false;
    private bool doneLooking;
    public float rectSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (ControlManager.doneChoosing && !doneLooking)
        {
            Pistol = GameObject.FindGameObjectWithTag("Gun");
            pistolle = Pistol.GetComponent<Pistol>();
            doneLooking = true;
        }


        if (playerAimScript.isAiming || InventoryPlayer.inventoryOn)
        {
            subCrosshair.SetActive(isActive);
        }
        else
        {
            subCrosshair.SetActive(!isActive);
        }

        rectSize = pistolle.allStats.list[0];
        reticle.sizeDelta = new Vector2(rectSize, rectSize);
    }

    public void ToggleFPSCrosshair()
    {
        gameObject.SetActive(true);
    }
}

