using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement1 : MonoBehaviour
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
    private float turn;
    private float strafing;

    //Serialize fields
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

    private void Crouching()
    {
        //Checks for crouch input and if the player is walking or not
        crouching = Input.GetButton("crouchP1");
        anim.SetBool("Crouching", crouching);
        anim.SetBool("isWalking", walking == 0 ? false : true);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (crouching)
        {
            //If the player is in either crouch state, half the capsule collider and move the camera down
            if (stateInfo.IsName("Idle2Crouch_Neutral2Crouch2Idle") || stateInfo.IsName("136_13"))
            {
                float colliderHeight = anim.GetFloat("colliderHeight");
                capsule.height = startCapsuleHeight * colliderHeight;
                playerCamera.transform.position = crouchCameraHeight.position;
                float centery = capsule.height / 2f;
                capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
            }
        }
        //If the exit crouch, return everything to the original size and position
        else
        {
            capsule.height = startCapsuleHeight;
            float centery = capsule.height / 2;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, originalCameraHeight.position.y, playerCamera.transform.position.z);
            capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
        }
    
        PreventStandingInLowHeadRoom();
    }

    private void Movement()
    {
        //Getting inputs and placing them in the animator if needed
        walking = Input.GetAxis("VerticalP1");
        anim.SetFloat("Walking", walking);
        strafing = Input.GetAxis("HorizontalP1");
        //Moving left and right
        Vector3 Strafing = transform.right * strafing * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + Strafing);
        anim.SetFloat("Strafing", strafing);
        //Moving back and forth
        Vector3 movement = transform.forward * walking * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
        
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
