using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{
    //contains all the variables used for the non-vr player
    #region variables
    [SerializeField] ExitMenuScript exitmenuscript;
    [SerializeField] public Camera mainCamera;
    public CharacterController controller;
    public Transform groundCheck;
    //movement variables
    public float speed = 12f, walkSpeed = 12f, crouchSpeed = 6f, gravity = -9.81f, groundDistance = 0.4f, x, z;

    public LayerMask groundMask;

    Vector3 Velocity;
    public bool isGrounded = false;
    private bool canCrouch = true;

    //Crouch parameters
    private float crouchHeigt = 0.5f;
    private float standHeight = 2f;
    private float timeToCrouch = 0.25f;
    private Vector3 crouchingCenter = new Vector3(0, 0.5f, 0);
    private Vector3 standingCenter = new Vector3(0, 0, 0);
    private bool isCrouching;
    private bool duringCrouchAnimation;
    //Crouch misc
    private KeyCode crouchKey = KeyCode.LeftControl;
    private bool shouldCrouch => Input.GetKeyDown(crouchKey) && !duringCrouchAnimation && controller.isGrounded;
    #endregion

    private void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (exitmenuscript != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && exitmenuscript.menuOn) exitmenuscript.ToggleExitMenuScreenOff();
            else if (Input.GetKeyDown(KeyCode.Escape) && !exitmenuscript.menuOn) exitmenuscript.ToggleExitMenuScreenOn();
        }

        //contains movement of the non-vr player
        #region movement
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }




        //old code
        /**
        if (Input.GetKey(KeyCode.W)) z = 1;
        else if (Input.GetKey(KeyCode.S)) z = -1;
        else z = 0;

        if (Input.GetKey(KeyCode.D)) x = 1;
        else if (Input.GetKey(KeyCode.A)) x = -1;
        else x = 0;
        **/



        if (canCrouch) HandleCrouch();
        if (isCrouching) speed = crouchSpeed;
        else speed = walkSpeed;
        #endregion

    }

    private void FixedUpdate()
    {
        if (exitmenuscript.menuOn == false)
        {
            x = Input.GetAxis("Horizontal");
            z = Input.GetAxis("Vertical");
            Vector3 move = transform.right * x + transform.forward * z;

            controller.Move(move * speed * Time.deltaTime);

            Velocity.y += gravity * Time.deltaTime;
            controller.Move(Velocity * Time.deltaTime);
        }
    }

    //contains crouching of the non-vr player
    #region crouching
    private void HandleCrouch()
    {
        if (shouldCrouch) StartCoroutine(CrouchStand());

    }
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(mainCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeigt; //If crouching targetHeigh is standheight else crouchheigt
        float currentHeight = controller.height; //Set height
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter; //if crouching standingCenter else crouchingcenter
        Vector3 currentCenter = controller.center; //Set center of player object

        while (timeElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch); //Change height to target height
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch); //Change center to target center
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }
    #endregion

    public void ToggleFPSPlayer()
    {
        gameObject.SetActive(false);
    }
}
