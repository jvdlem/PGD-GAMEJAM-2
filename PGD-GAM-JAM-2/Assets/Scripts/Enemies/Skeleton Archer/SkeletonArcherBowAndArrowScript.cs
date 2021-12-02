using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonArcherBowAndArrowScript : MonoBehaviour
{
    private GameObject arrow;
    // Start is called before the first frame update

    //Arrow forces
    public float shootForce, upwardsForce;

    //Gun variables
    public float timeBetweenAttacks, arrowCount, reloadTime, spread;
    public int magazineSize, bulletPerTap;
    public bool allowButtonHold;

    int bulletsLeft, bulletsShot;

    //Bools
    bool isShooting, readyToShoot, reloading;
    public Camera fpsCam;
    public Transform attackPoint;


    void Awake()
    {
        bulletsLeft = magazineSize;
        readyToShoot = true;
    }

    // Update is called once per frame
    void Update()
    {
        MyInput();
    }

    void MyInput()
    {
        //Check if mousehold down is allowed
        if (allowButtonHold) isShooting = Input.GetKey(KeyCode.Mouse0);
        else isShooting = Input.GetKeyDown(KeyCode.Mouse0);

        //Shooting
        if(readyToShoot && isShooting && !reloading && bulletsLeft > 0)
        {

            bulletsShot = 0;

            Shoot();

        }
    }

    void Shoot()
    {
        readyToShoot = false;

        bulletsLeft--;
        bulletsShot++;
    }
}
