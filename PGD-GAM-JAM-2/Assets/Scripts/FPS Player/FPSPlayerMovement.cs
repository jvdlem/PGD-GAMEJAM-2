using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPSPlayerMovement : MonoBehaviour
{
    [SerializeField] public Camera mainCamera;
    public CharacterController controller;
    public Transform groundCheck;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float groundDistance = 0.4f;

    public LayerMask groundMask;

    Vector3 Velocity;
    public bool isGrounded = false;


    //crouching stuff (pls send help)
    private KeyCode crouchKey = KeyCode.LeftControl;
    private bool canCrouch = true;
    private bool ShouldCrouch => canCrouch && Input.GetKey(crouchKey) && !duringCrouchAnimation && isGrounded;
    float crouchHeight = 1;
    float standHeight = 2;
    bool isCrouching, duringCrouchAnimation;
    float crouchTime = 0.5f;
    Vector3 standCenterPoint = new Vector3(0, 1, 0);
    Vector3 crouchCenterPoint = new Vector3(0, 0.5f, 0);


    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && Velocity.y < 0)
        {
            Velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        Velocity.y += gravity * Time.deltaTime;
        controller.Move(Velocity * Time.deltaTime);

        if (ShouldCrouch)
        {
            CrouchStand();

        }
        Debug.Log(ShouldCrouch);
        Debug.Log(isGrounded);
    }
    private IEnumerator CrouchStand()
    {
        if (isCrouching && Physics.Raycast(mainCamera.transform.position, Vector3.up, 1f))
            yield break;

        duringCrouchAnimation = true;

        float timeElapsed = 0;
        float targetHeight = isCrouching ? standHeight : crouchHeight;
        float currentHeight = controller.height;
        float cameraCurrentHeight = mainCamera.transform.position.y;
        float cameraTargetHeight = !isCrouching ? 1 : -1;
        Vector3 targetCenter = isCrouching ? standCenterPoint : crouchCenterPoint;
        Vector3 currentCenter = controller.center;

        while (timeElapsed < crouchTime)
        {
            controller.height = Mathf.Lerp(currentHeight, targetHeight, timeElapsed / crouchTime);
            controller.center = Vector3.Lerp(currentCenter, targetCenter, timeElapsed / crouchTime);
            mainCamera.transform.position = new Vector3(mainCamera.transform.position.x, Mathf.Lerp(cameraCurrentHeight, cameraCurrentHeight - cameraTargetHeight, timeElapsed / crouchTime), mainCamera.transform.position.z);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        controller.height = targetHeight;
        controller.center = targetCenter;

        isCrouching = !isCrouching;
        duringCrouchAnimation = false;
    }
}
