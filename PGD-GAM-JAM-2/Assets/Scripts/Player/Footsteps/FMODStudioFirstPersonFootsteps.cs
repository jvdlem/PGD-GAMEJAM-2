using UnityEngine;

public class FMODStudioFirstPersonFootsteps : MonoBehaviour
{
    //Set FMOD settings in the Inspector
    [Header("FMOD Settings")]
    [SerializeField] FMODUnity.EventReference FootstepsEventPath; //FMOD event
    [SerializeField] private string MaterialParameterName;  //FMOD material Parameter                    
    [SerializeField] private string SpeedParameterName;                         
    [Header("Playback Settings")]
    [SerializeField] private float StepDistance = 2.0f;  //How far must the player walk for the sfx to play                     
    [SerializeField] private float RayDistance = 1.2f;   //Length of the ray to detect ground                   
    public string[] MaterialTypes; //List of diffrent Materials                                            
    [HideInInspector] public int DefulatMaterialValue; //Default material if not set on floor                    


    //These variables are used to control when the player executes a footstep.
    private float StepRandom;  // Set random number to add variety to footsteps
    private Vector3 PrevPos;   // Previous position of player
    private float DistanceTravelled;   // This will hold a value that how represnt how far the player has travlled since they last took a step.
    //These variables are used when checking the Material type the player is on top of.
    private RaycastHit hit;    // Raycast hit
    private int F_MaterialValue;  // Sets value to determine material type
    //These booleans will hold values that tell if the player is touching the ground currently and if they were touching it during the last frame.
    private bool PlayerTouchingGround; //Check if player is touching ground  
    private bool PreviosulyTouchingGround; //Check if player was touching ground                                    



    void Start()
    {
        StepRandom = Random.Range(0f, 0.5f);
        PrevPos = transform.position;
    }


    void Update()
    {
        Debug.DrawRay(transform.position, Vector3.down * RayDistance, Color.blue); //Debug for Raycast

        GroundedCheck(); //Check if grounded                                   
        //Update 'PreviosulyTouchingGround' to the value of whatever 'PlayerTouchingGround' variable is.
        PreviosulyTouchingGround = PlayerTouchingGround;
        
        //This section determines when and how the PlayFootstep method should be told to trigger, thus playing the footstep event in FMOD.
        DistanceTravelled += (transform.position - PrevPos).magnitude;  //Every frame, the co-ordinates that the player is currently at is reduced by the value of the co-ordinates they were at during the previous frame. 
        if (DistanceTravelled >= StepDistance + StepRandom)
        {
            MaterialCheck(); // Checks what meterial the player is walking on
            PlayFootstep();   // Plays footstep sound
            StepRandom = Random.Range(0f, 0.5f); //Reset StepRandom
            DistanceTravelled = 0f; //Set DistanceTravelled to 0 because we just moved
        }
        PrevPos = transform.position; //Calculate where player was during the last frame, by setting 'PrevPos' to whaterver the current position of the player is during the end of the current frame.
    }


    void MaterialCheck()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance))
        {
            if (hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>())// hit variable checks what Material the ground has the player is currently walking on.
            {
                F_MaterialValue = hit.collider.gameObject.GetComponent<FMODStudioMaterialSetter>().MaterialValue; //Set material variable to whatever the material is that the ground is made out of. That way FMOD knows what sound to play
            }
            else  //If the ground doesn't have a set material, use the sound for default material
                F_MaterialValue = DefulatMaterialValue;                                                              
        }
        else //Incase the raycast can't find a collider but the player is still walking on something. Use default sound effect
            F_MaterialValue = DefulatMaterialValue;
    }

    void GroundedCheck()
    {
        Physics.Raycast(transform.position, Vector3.down, out hit, RayDistance);   
        if (hit.collider)                                                          
            PlayerTouchingGround = true;                                            
        else                                                                        
            PlayerTouchingGround = false;                                           
    }


    void PlayFootstep()
    {
        if (PlayerTouchingGround)//Check if player is touching the ground
        {
            FMOD.Studio.EventInstance Footstep = FMODUnity.RuntimeManager.CreateInstance(FootstepsEventPath); //Create FMOD event instance
            FMODUnity.RuntimeManager.AttachInstanceToGameObject(Footstep, transform, GetComponent<Rigidbody>()); //Attach the instance to player position
            Footstep.setParameterByName(MaterialParameterName, F_MaterialValue); //Set material parameter
            Footstep.start();
            Footstep.release();
        }
    }
}

