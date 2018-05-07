using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
{
    //Private fields
    private Animator anim;
    private float walking;
    private bool crouching;
    private CapsuleCollider capsule;
    private float startCapsuleHeight;
    private Vector3 capsuleCenter;
    private float halfCrouch = 0.5f;
    private Rigidbody rigidbody;
    private float strafing;

    //Serialize Fields
    [SerializeField]
    Camera playerCamera;
    [SerializeField]
    private Transform originalCameraHeight;
    [SerializeField]
    private Transform crouchCameraHeight;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        walking = 0.0f;
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        startCapsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Crouching();
    }


    private void Movement()
    {
        //Getting all the inputs and putting them in teh animator if necessary
        walking = Input.GetAxis("VerticalP2");
        anim.SetFloat("Walking", walking);
        strafing = Input.GetAxis("HorizontalP2");

        //Moving side to side
        Vector3 Strafing = transform.right * strafing * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + Strafing);
        anim.SetFloat("Strafing", strafing);

        //Moving forward and backwards
        Vector3 movement = transform.forward * walking * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
        

    }

    private void Crouching()
    {
        //Checking for the crouch input, it needs to be held down. Also checks if the player is moving
        crouching = Input.GetButton("crouchP2");
        anim.SetBool("Crouching", crouching);
        anim.SetBool("isWalking", walking == 0 ? false : true);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (crouching)
        {
            //If the player is in either crouch state, half the capsule trigger and move the camera to match the crouch
            if (stateInfo.IsName("Idle2Crouch_Neutral2Crouch2Idle") || stateInfo.IsName("136_14"))
            {
                float colliderHeight = anim.GetFloat("colliderHeight");
                capsule.height = startCapsuleHeight * colliderHeight;
                playerCamera.transform.position = crouchCameraHeight.position;
                float centery = capsule.height / 2.2f;
                capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
            }
        }
        //If they aren't crouching, return everything to normal
        else
        {
            capsule.height = startCapsuleHeight;
            float centery = capsule.height / 2;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, originalCameraHeight.position.y, playerCamera.transform.position.z);
            capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
        }
        PreventStandingInLowHeadRoom();
    }

    private void PreventStandingInLowHeadRoom()
    {
        if (!crouching)
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * halfCrouch, Vector3.up);
            float crouchRayLength = startCapsuleHeight - capsule.radius * halfCrouch;
            if (Physics.SphereCast(crouchRay, capsule.radius * halfCrouch, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
            }
        }
    }
}
