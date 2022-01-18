using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalChange : MonoBehaviour
{
    [SerializeField] private Color battleColor;
    [SerializeField] private Light crystalLight;
    [SerializeField] private Color beginColor;
    [SerializeField] private Color beginLightColor;
    [SerializeField] private float desiredDuration = 3f;
    private float elapsedTime;
    private bool inBattle = false;
    private void Start()
    {
        beginColor = this.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_EmissionColor");
        crystalLight = this.transform.GetChild(1).GetComponent<Light>();
        beginLightColor = this.transform.GetChild(0).GetComponent<Renderer>().material.GetColor("_EmissionColor");
    }
    public void BattelForm()
    {
        elapsedTime = 0;
        inBattle = true;

    }
    public void normalForm()
    {
        elapsedTime = 0;
        inBattle = false;
    }
    private void Update()
    {
        if (inBattle&&elapsedTime<desiredDuration)
        {
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            this.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(beginColor,battleColor, percantageComplete));
            crystalLight.color = Color.Lerp(beginLightColor, battleColor, percantageComplete);
        }
        else if(elapsedTime < desiredDuration)
        {
            elapsedTime += Time.deltaTime;
            float percantageComplete = elapsedTime / desiredDuration;
            this.transform.GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(battleColor, beginColor, percantageComplete));
            crystalLight.color = Color.Lerp(battleColor, beginColor, percantageComplete);
        }
    }
}
