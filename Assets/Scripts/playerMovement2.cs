using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement2 : MonoBehaviour
{

    private Animator anim;
    private float walking;
    private bool crouching;
    private CapsuleCollider capsule;
    private float startCapsuleHeight;
    private Vector3 capsuleCenter;
    private float halfCrouch = 0.5f;
    private Rigidbody rigidbody;

    [SerializeField]
    private float walkingSpeed;

    [SerializeField]
    Camera playerCamera;
    private float originalCameraHeight;
    private float crouchCameraHeight;

    // Use this for initialization
    void Start ()
    {
        anim = GetComponent<Animator>();
        walking = 0.0f;

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
    private void Movement()
    {
        walking = Input.GetAxis("VerticalP2");
        anim.SetFloat("Walking", walking);

        Vector3 movement = transform.forward * walking * walkingSpeed * Time.deltaTime;
        rigidbody.MovePosition(rigidbody.position + movement);

    }

    private void Crouching()
    {
        crouching = Input.GetButton("crouchP2");
        anim.SetBool("Crouching", crouching);
        anim.SetBool("isWalking", walking == 0 ? false : true);

        AnimatorStateInfo stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (crouching)
        {
            if (stateInfo.IsName("Idle2Crouch_Neutral2Crouch2Idle") || stateInfo.IsName("136_14"))
            {
                float colliderHeight = anim.GetFloat("colliderHeight");
                capsule.height = startCapsuleHeight * colliderHeight;
                float centery = capsule.height / 2;
                crouchCameraHeight = originalCameraHeight / 2;
                playerCamera.transform.position = new Vector3(playerCamera.transform.position.x, crouchCameraHeight, playerCamera.transform.position.z);
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
