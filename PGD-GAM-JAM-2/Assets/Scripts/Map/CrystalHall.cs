using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalHall : CrystalChange
{
    protected override void Start()
    {
        crystalLight = this.transform.GetChild(1).GetComponent<Light>();
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
