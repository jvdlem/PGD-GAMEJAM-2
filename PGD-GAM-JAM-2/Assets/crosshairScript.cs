using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crosshairScript : MonoBehaviour
{
    private RectTransform reticle;
    [SerializeField ]private Pistol pistolle;

    public float rectSize;

    private void Start()
    {
        reticle = GetComponent<RectTransform>();
    }

    private void Update()
    {
        rectSize = pistolle.allStats.list[0];
        reticle.sizeDelta = new Vector2(rectSize, rectSize);
    }
}

