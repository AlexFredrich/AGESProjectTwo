using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour {

    [SerializeField]
    private Transform player;
    [SerializeField]
    private Transform playerCam;
    [SerializeField]
    private float throwForce = 2;
    [SerializeField]
    private Transform guide;
    bool hasPlayer = false;
    bool beingCarried = false;
    private bool touched = false;

	
	// Update is called once per frame
	void Update ()
    {
        float dist = Vector3.Distance(gameObject.transform.position, player.position);
        if (dist <= 1.5f)
        {
            hasPlayer = true;
        }
        else
        {
            hasPlayer = false;
        }
        if (hasPlayer && Input.GetButton("Interact"))
        {
            GetComponent<Rigidbody>().isKinematic = true;
            transform.position = guide.position;
            transform.rotation = guide.rotation;
            transform.parent = playerCam;
            
            beingCarried = true;
        }
        if (beingCarried)
        {
            if (touched)
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                touched = false;
            }
            if (Input.GetButtonUp("Interact"))
            {
                GetComponent<Rigidbody>().isKinematic = false;
                transform.parent = null;
                beingCarried = false;
                
                
            }

        }
    }

    void OnTriggerEnter()
    {
        if (beingCarried)
        {
            touched = true;
        }
    }
}
