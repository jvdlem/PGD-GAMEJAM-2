using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachmentPowerUP : MonoBehaviour
{
    [SerializeField] private Color startEmmision;
    [SerializeField] private Color powerEmmision;
    private Material myMaterial;
    private bool superPowered = false;
    private bool lowerPowered = false;
    private bool canPowerUp = true;
    private bool canPowerDown = true;
    private float elapsedTime;
    [SerializeField] private float desiredDuration = 1f;
    [SerializeField] private AnimationCurve myCurve;

    // Start is called before the first frame update
    void Start()
    {
        //startEmmision = this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.GetColor("_EmissionColor");
        this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", startEmmision);
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percantageComplete = elapsedTime / desiredDuration;

        Debug.Log(myCurve.Evaluate(percantageComplete));
        if (superPowered && canPowerUp)
        {
            Debug.Log("playing that");
            for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(startEmmision, powerEmmision, myCurve.Evaluate(percantageComplete)));
            }
            if (percantageComplete > 1)
            {
                canPowerUp = false;
            }
        }
        if (superPowered && canPowerDown)
        {
            Debug.Log("playing this");
            this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(startEmmision, powerEmmision, myCurve.Evaluate(elapsedTime)));


        }

    }

    public void SetPowerLevel(int aOperator)
    {
        elapsedTime = 0;
        if (aOperator >= 1)
        {
            canPowerUp = true;
            superPowered = true;
        }
        else
        {
            canPowerDown = true;
            canPowerUp = false;
            superPowered = false;
        }
    }
}
