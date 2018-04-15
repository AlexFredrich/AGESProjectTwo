using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement1 : MonoBehaviour {

    private Animator anim;
    private float walking;
    private float turning;
    private bool crouching;
    private CapsuleCollider capsule;
    private float capsuleHeight;
    private Vector3 capsuleCenter;
    private float halfCrouch = 0.5f;
    private Rigidbody rigidbody;

    [SerializeField]
    private float turningSpeed;

	// Use this for initialization
	void Start ()
    {
        anim = GetComponent<Animator>();
        walking = 0.0f;
        turning = 0.0f;
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
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
        crouching = Input.GetButton("crouch");
        anim.SetBool("Crouching", crouching);
        if (crouching)
        {
            //if (crouching) return;
            capsule.height = capsule.height / 2f;
            capsule.center = capsule.center / 2f;
            
        }
        else
        {

        }
        //ScaleCapsuleForCrouching(crouching);
        PreventStandingInLowHeadRoom();
        
    }

    private void Movement()
    {
        walking = Input.GetAxis("Vertical");
        anim.SetFloat("Walking", walking);
        turning = Input.GetAxis("Horizontal") * turningSpeed;
        turning *= Time.deltaTime;
        transform.Translate(turning, 0, walking);
    }

    private void PreventStandingInLowHeadRoom()
    {
        if (!crouching)
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * halfCrouch, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * halfCrouch;
            if (Physics.SphereCast(crouchRay, capsule.radius * halfCrouch, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
            }
        }
    }

    private void ScaleCapsuleForCrouching(bool crouch)
    {
        if (crouch)
        {
            //if (crouching) return;
            capsule.height = capsule.height / 2f;
            capsule.center = capsule.center / 2f;
            crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * halfCrouch, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * halfCrouch;
            if (Physics.SphereCast(crouchRay, capsule.radius * halfCrouch, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
                return;
            }
            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;
            crouching = false;
        }
    }

}
