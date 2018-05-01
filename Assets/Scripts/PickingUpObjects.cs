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
		if(Input.GetButton("Interact"))
        {
            if(Physics.Raycast(TransformCamera.position, TransformCamera.forward, out hit, distance, RayMask))
            {
                if(hit.transform.tag == "Item")
                {
                    SetNewTransform(hit.transform);
                }
            }
        }

        if(Input.GetButtonUp("Interact"))
        {
            RemoveTransform();
        }

        if(currentTransform)
        {
            MoveTransformAround();
        }
	}

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

    public void RemoveTransform()
    {
        if (!currentTransform)
            return;

        currentTransform.GetComponent<Rigidbody>().isKinematic = false;

        currentTransform = null;
    }
}
