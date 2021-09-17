using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    private static int amountOfEyes = 3;
    private EyeBossScript[] Eyes = new EyeBossScript[amountOfEyes];
    public GameObject Player;

    private int bossMaxHealth = 100;
    public int BossHealth;
    public Slider healthSlider;
    public Transform MinionSpawnLocation;
    public GameObject Minions;

    private float EyeLookSpeedDefault = 3;
    private float EyeLookSpeedTracking = 10;

    private float laserChargeTime = 3;
    private float laserChargeTimer;
    public GameObject Laser;

    private float eyeShootDelay = 1;
    private float eyeShootTimer;
    public GameObject Bullets;

    public int EyeHits;
    private int MaxEyeHits = 4;

    private float BossDiesFadeTime = 3;
    private float BossDiesFadeTimer;

    public int bossState;

    private float bossIsWaitingTime = 5;
    private float bossIsWaitingTimer;

    void Start()
    {
        bossState = 0;

        for (int i = 0; i < amountOfEyes; i++)
        {
            Eyes[i] = gameObject.transform.GetChild(i).GetComponent<EyeBossScript>();
        }
    }

    void Update()
    {
        healthSlider.value = BossHealth;

        switch (bossState)
        {
            //boss spawnes/resets state
            case 0:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(true);
                    eye.renderer.material.color = Color.white;
                }
                healthSlider.gameObject.SetActive(true);
                BossHealth = bossMaxHealth;
                bossState = 1;
                break;

            //boss is waiting state
            case 1:
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.EyeIsActive = false;
                }

                if (bossIsWaitingTimer < bossIsWaitingTime)
                {
                    bossIsWaitingTimer += Time.deltaTime;
                }
                else
                {
                    bossState = Random.Range(2, 5);
                    bossIsWaitingTimer = 0;
                }
                break;

            //boss uses left eye state
            case 2:
                Eyes[0].EyeIsActive = true;
                Eyes[0].lookSpeed = EyeLookSpeedTracking;

                if (eyeShootTimer < eyeShootDelay)
                {
                    eyeShootTimer += Time.deltaTime;
                }
                else
                {
                    Instantiate(Bullets, Eyes[0].transform.position, Eyes[0].transform.rotation, transform);
                    eyeShootTimer = 0;
                }

                if (EyeHits >= MaxEyeHits)
                {
                    Eyes[0].lookSpeed = EyeLookSpeedDefault;
                    EyeHits = 0;
                    bossState = 1;
                }
                break;

            //boss uses middle eye state
            case 3:
                Eyes[1].EyeIsActive = true;
                if (Minions != null)
                {
                    Instantiate(Minions, MinionSpawnLocation.position, MinionSpawnLocation.rotation, MinionSpawnLocation);
                }

                if (EyeHits >= MaxEyeHits)
                {
                    EyeHits = 0;
                    bossState = 1;
                }
                break;

            //boss uses right eye state
            case 4:
                Eyes[2].EyeIsActive = true;
                Eyes[2].lookSpeed = EyeLookSpeedTracking;

                if (laserChargeTimer < laserChargeTime)
                {
                    laserChargeTimer += Time.deltaTime;
                    Eyes[2].renderer.material.color = Color.Lerp(Color.white, Color.red, laserChargeTimer / 3);
                }
                else
                {
                    if (Eyes[2].transform.childCount == 0)
                    {
                        Instantiate(Laser, Eyes[2].transform.position, Eyes[2].transform.rotation, Eyes[2].transform);
                    }
                    Eyes[2].renderer.material.color = Color.red;
                }

                if (EyeHits >= MaxEyeHits)
                {
                    if(Eyes[2].transform.childCount != 0)
                    {
                        Destroy(Eyes[2].transform.GetChild(0).gameObject);
                    }
                    Eyes[2].lookSpeed = EyeLookSpeedDefault;
                    laserChargeTimer = 0;
                    EyeHits = 0;
                    bossState = 1;
                }
                break;

            //boss dies state;
            case 5:
                if (BossDiesFadeTimer < BossDiesFadeTime)
                {
                    BossDiesFadeTimer += Time.deltaTime;

                    foreach (EyeBossScript eyes in Eyes)
                    {
                        eyes.renderer.material.color = Color.white;

                        eyes.renderer.material.color = Color.Lerp(eyes.renderer.material.color, new Color(
                            eyes.renderer.material.color.r,
                            eyes.renderer.material.color.g,
                            eyes.renderer.material.color.b,
                            0),
                            BossDiesFadeTimer/ 2);
                    }
                }
                else
                {
                    bossState = 6;
                }

                break;

            //boss goes inactive state
            case 6:
                healthSlider.gameObject.SetActive(false);
                foreach (EyeBossScript eye in Eyes)
                {
                    eye.gameObject.SetActive(false);
                }

                BossDiesFadeTimer = 0;

                bossState = 7;
                break;

            //boss is inactive state
            case 7:
                break;
        }
    }
}
