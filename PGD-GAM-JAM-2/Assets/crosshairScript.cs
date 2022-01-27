using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshairScript : MonoBehaviour
{
    private RectTransform reticle;
    [SerializeField] private Pistol pistolle;
    public GameObject subCrosshair;
    public bool isActive = false;

    public float rectSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (playerAimScript.isAiming)
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
}

