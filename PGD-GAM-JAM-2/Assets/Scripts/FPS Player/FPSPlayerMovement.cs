using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{
    [SerializeField] ExitMenuScript exitmenuscript;
    [SerializeField] public Camera mainCamera;
    public CharacterController controller;
    public Transform groundCheck;

    public float speed = 12f;
    public float walkSpeed = 12f;
    private float crouchSpeed = 6f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;
    float x;
    float z;

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

    private void Start()
    {
        gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (exitmenuscript != null)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && exitmenuscript.menuOn)
            {
                exitmenuscript.ToggleExitMenuScreenOff();
            }
            else if (Input.GetKeyDown(KeyCode.Escape) && !exitmenuscript.menuOn)
            {
                exitmenuscript.ToggleExitMenuScreenOn();
            }
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        /**
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        **/

        if (Input.GetKey(KeyCode.W)) z = 1;
        else if (Input.GetKey(KeyCode.S)) z = -1;
        else z = 0;

        if (Input.GetKey(KeyCode.D)) x = 1;
        else if (Input.GetKey(KeyCode.A)) x = -1;
        else x = 0;

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        if (canCrouch) HandleCrouch();
        if (isCrouching) speed = crouchSpeed;
        else speed = walkSpeed;

    }

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
        float targetHeight = isCrouching ? standHeight : crouchHeigt;
        float currentHeight = controller.height;
        Vector3 targetCenter = isCrouching ? standingCenter : crouchingCenter;
        Vector3 currentCenter = controller.center;

        while (timeElapsed < timeToCrouch)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / timeToCrouch);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / timeToCrouch);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;

        duringCrouchAnimation = false;
    }

    public void ToggleFPSPlayer()
    {
        gameObject.SetActive(false);
    }
}
