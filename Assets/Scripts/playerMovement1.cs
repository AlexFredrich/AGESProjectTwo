using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement1 : MonoBehaviour {

    private Animator anim;
    private float walking;
    private bool crouching;
    private CapsuleCollider capsule;
    private float startCapsuleHeight;
    private Vector3 capsuleCenter;
    private float halfCrouch = 0.5f;
    private Rigidbody rigidbody;
    private float turn;

    [SerializeField]
    Camera playerCamera;
    private float originalCameraHeight;
    private float crouchCameraHeight;



    [SerializeField]
    private float walkingSpeed, turnSpeed;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();

        
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
       startCapsuleHeight = capsule.height;
        capsuleCenter = capsule.center;
        originalCameraHeight = playerCamera.transform.position.y;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        
        Crouching();
	}

    private void Crouching()
    {
        crouching = Input.GetButton("crouchP1");
        anim.SetBool("Crouching", crouching);
        anim.SetBool("isWalking", walking == 0 ? false : true);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (crouching)
        {
            if (stateInfo.IsName("Idle2Crouch_Neutral2Crouch2Idle") || stateInfo.IsName("136_13"))
            {
                float colliderHeight = anim.GetFloat("colliderHeight");
                capsule.height = startCapsuleHeight * colliderHeight;
                crouchCameraHeight = originalCameraHeight / 1.05f;
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, Mathf.Lerp(originalCameraHeight, crouchCameraHeight, .5f), playerCamera.transform.position.z);
                float centery = capsule.height / 2;

                capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
            }
            
        }
        else
        {
            capsule.height = startCapsuleHeight;
            float centery = capsule.height / 2;
            playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, originalCameraHeight, playerCamera.transform.position.z);
            capsule.center = new Vector3(capsule.center.x, centery, capsule.center.z);
        }
        //ScaleCapsuleForCrouching(crouching);
        PreventStandingInLowHeadRoom();
        
    }

    private void Movement()
    {
        walking = Input.GetAxis("VerticalP1");
        anim.SetFloat("Walking", walking);

        Vector3 movement = transform.forward * walking * walkingSpeed * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);
        
    }

    //private void Turn()
    //{
    //    turn = Input.GetAxis("HorizontalP1");
    //    float turnValue = turn * turnSpeed * Time.deltaTime;
    //    Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
    //    rigidbody.MoveRotation(rigidbody.rotation * turnRotation);
    //}

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
