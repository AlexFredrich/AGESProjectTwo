using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickingUpObjects : MonoBehaviour
{

    [SerializeField]
    private Transform TransformCamera;
    [SerializeField]
    private LayerMask RayMask;
    [SerializeField]
    private float distance = 5;
    [SerializeField]
    private Transform handTransform;

    private RaycastHit hit;
    private Transform currentTransform;
    private float length;

    // Update is called once per frame
    void Update ()
    {
        //If the player presses a button, it will move the object to the hand transform and be able to move the object around
		if(Input.GetButton("PickUp"))
        {
            //An object will only be able to be picked up if the raycast hits an object with the tag item
            if(Physics.Raycast(TransformCamera.position, TransformCamera.forward, out hit, distance, RayMask))
            {
                if(hit.transform.tag == "Item")
                {
                    SetNewTransform(hit.transform);
                }
            }
        }
        //Pressing another key will get the player to drop the item right where in front of them
        if(Input.GetButton("Drop"))
        {
            RemoveTransform();
        }

        if(currentTransform)
        {
            MoveTransformAround();
        }
	}
    //Moving the object
    public void SetNewTransform(Transform newTransform)
    {
        if (currentTransform)
            return;
        currentTransform = newTransform;
        length = Vector3.Distance(handTransform.position, newTransform.position);

        currentTransform.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void MoveTransformAround()
    {
        currentTransform.position = handTransform.position + handTransform.forward * length;
    }
    //If released, it removes the transform and sets the new position
    public void RemoveTransform()
    {
        if (!currentTransform)
            return;

        currentTransform.GetComponent<Rigidbody>().isKinematic = false;

        currentTransform = null;
    }
}
