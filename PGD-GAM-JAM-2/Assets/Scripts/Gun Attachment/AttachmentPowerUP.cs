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
    private bool canPowerDown = false;
    private float elapsedTime;
    private bool colorPower;
    [SerializeField] private float desiredDuration = 1f;
    [SerializeField] private AnimationCurve myCurve;
    [SerializeField] private ParticleSystem myParticle;
    public int test = 100;

    // Start is called before the first frame update
    void Start()
    {
        this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", startEmmision);
        if (colorPower == false)
        {
            colorPower = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        elapsedTime += Time.deltaTime;
        float percantageComplete = elapsedTime / desiredDuration;
        if (superPowered)
        {
            for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(startEmmision, powerEmmision*3, myCurve.Evaluate(elapsedTime)));
            }  
        }
        if (superPowered == false && canPowerDown)
        {
            for (int i = 0; i < this.transform.GetChild(0).childCount; i++)
            {
                this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.Lerp(powerEmmision * 3, startEmmision, myCurve.Evaluate(elapsedTime)));
            }
        }
    }

    public void ChangeColorstate()
    {
        superPowered = false;
        this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", startEmmision);
        ResetAttachment();
    }

    public void SuperPowered(int aOperator)
    {
        elapsedTime = 0;
        if (aOperator >= 1)
        {
            canPowerUp = true;
            superPowered = true;
            canPowerDown = false;
           FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/SetBonusTier4", this.gameObject.transform.position);

        }
        else
        {
            canPowerDown = true;
            canPowerUp = false;
            superPowered = false;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/LoseBonus4", this.gameObject.transform.position);
        }
    }

    

    public void NormalPowered(int aOperator)
    {
        
        if (aOperator >= 1 && canPowerUp == true)
        {
            canPowerUp = false;
            myParticle.Play();
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/SetBonusTier3", this.gameObject.transform.position);
        }
        else if(aOperator <= -1 && canPowerUp == false)
        {
            canPowerUp = true;
            FMODUnity.RuntimeManager.PlayOneShot("event:/Gun/Attachements/LoseBonus3", this.gameObject.transform.position);
        }
    }
    void OnEnable()
    {
        superPowered = false;
        canPowerDown = false;
        this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", startEmmision);
    }
    void ResetAttachment()
    {
        canPowerUp = true;
        superPowered = false;
        canPowerDown = false;
        this.transform.GetChild(0).GetChild(0).GetComponent<Renderer>().material.SetColor("_EmissionColor", startEmmision);
    }
}
