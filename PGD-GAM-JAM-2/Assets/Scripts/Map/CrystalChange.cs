using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalChange : MonoBehaviour
{
    [SerializeField] protected Color battleColor;
    [SerializeField] protected Light crystalLight;
    [SerializeField] protected Color beginColor;
    [SerializeField] protected Color beginLightColor;
    [SerializeField] protected float desiredDuration = 3f;
    protected float elapsedTime;
    protected bool inBattle = false;
    protected virtual void Start()
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
