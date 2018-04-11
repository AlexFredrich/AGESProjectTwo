using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    float movingTurnSpeed = 360, stationaryTurnSpeed = 180;
    [SerializeField]
    float moveSpeedMultiplier = 1f, animSpeedMultiplier = 1f;
    [SerializeField]
    float groundCheckDistance = 0.1f;

    [SerializeField]
    private string horizontalInput, verticalInput, crouchInput;

    private Rigidbody rigidbody;
    private Animator animator;
    private const float halfCrouch = 0.5f;
    private float turnAmount;
    private float forwardAmount;
    private Vector3 groundNormal;
    private float capsuleHeight;
    private Vector3 capsuleCenter;
    private CapsuleCollider capsule;
    private bool crouching;
    private Vector3 movement;


	// Use this for initialization
	void Start ()
    {
        animator = GetComponent<Animator>();
        rigidbody = GetComponent<Rigidbody>();
        capsule = GetComponent<CapsuleCollider>();
        capsuleHeight = capsule.height;
        capsuleCenter = capsule.center;

        rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezePositionX;

	}
	
	// Update is called once per frame
	void FixedUpdate ()
    {
        float h = Input.GetAxis(horizontalInput);
        float v = Input.GetAxis(verticalInput);
        bool crouch = Input.GetButtonDown(crouchInput);

        movement = v * Vector3.forward + h * Vector3.right;

        Move(movement, crouch);
	}

    private void Move(Vector3 move, bool crouch)
    {
        if (move.magnitude > 1f)
            move.Normalize();

        move = transform.InverseTransformDirection(move);
        CheckGroundStatus();
        move = Vector3.ProjectOnPlane(move, groundNormal);
        turnAmount = Mathf.Atan2(move.x, move.z);
        forwardAmount = move.z;

        ApplyExtraTurnRotation();


        ScaleCapsuleForCrouching(crouch);
        PreventStandingInLowHeadRoom();

        UpdateAnimator(move);
    }

    private void PreventStandingInLowHeadRoom()
    {
        if(!crouching)
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * halfCrouch, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * halfCrouch;
            if(Physics.SphereCast(crouchRay, capsule.radius * halfCrouch, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
            }
        }
    }

    private void ScaleCapsuleForCrouching(bool crouch)
    {
        if (crouch)
        {
            if (crouching) return;
            capsule.height = capsule.height / 2f;
            capsule.center = capsule.center / 2f;
            crouching = true;
        }
        else
        {
            Ray crouchRay = new Ray(rigidbody.position + Vector3.up * capsule.radius * halfCrouch, Vector3.up);
            float crouchRayLength = capsuleHeight - capsule.radius * halfCrouch;
            if(Physics.SphereCast(crouchRay, capsule.radius * halfCrouch, crouchRayLength, Physics.AllLayers, QueryTriggerInteraction.Ignore))
            {
                crouching = true;
                return;
            }
            capsule.height = capsuleHeight;
            capsule.center = capsuleCenter;
            crouching = false;
        }
    }

    private void UpdateAnimator(Vector3 move)
    {
        animator.SetFloat("Forward", forwardAmount, 0.1f, Time.deltaTime);
        animator.SetFloat("Turn", turnAmount, 0.1f, Time.deltaTime);
        animator.SetBool("Crouch", crouching);
        

        if(move.magnitude > 0)
        {
            animator.speed = animSpeedMultiplier;
        }
    }

    private void ApplyExtraTurnRotation()
    {
        float turnSpeed = Mathf.Lerp(stationaryTurnSpeed, movingTurnSpeed, forwardAmount);
        transform.Rotate(0, turnAmount * turnSpeed * Time.deltaTime, 0);
    }

    public void OnAnimatorMove()
    {
        if(Time.deltaTime > 0)
        {
            Vector3 v = (animator.deltaPosition * moveSpeedMultiplier) / Time.deltaTime;

            v.y = rigidbody.velocity.y;
            rigidbody.velocity = v;
        }
    }

    private void CheckGroundStatus()
    {
        RaycastHit hitInfo;

        if(Physics.Raycast(transform.position + (Vector3.up * 0.1f), Vector3.down, out hitInfo, groundCheckDistance))
        {
            groundNormal = hitInfo.normal;
            animator.applyRootMotion = true;
        }
        else
        {
            
            groundNormal = Vector3.up;
            animator.applyRootMotion = false;
        }
    }
}
