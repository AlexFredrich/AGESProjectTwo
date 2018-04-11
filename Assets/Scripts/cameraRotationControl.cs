using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraRotationControl : MonoBehaviour {

    [SerializeField]
    private float speedH = 2.0f;
    [SerializeField]
    private float speedV = 2.0f;

    [SerializeField]
    private float horizontalUpperRestrictions = 20, horizontalLowerRestrictions = -20;
    [SerializeField]
    private float verticalUpperRestrictions = 20, verticalLowerRestrictions = -20;


    private float yaw = 0.0f;
    private float pitch = 0.0f;

	// Update is called once per frame
	void Update ()
    {
        if (horizontalUpperRestrictions >= yaw && yaw >= horizontalLowerRestrictions)
        {
            yaw += speedH * Input.GetAxis("cameraHorizontal");
        }
        if (verticalUpperRestrictions >= pitch && pitch >= verticalLowerRestrictions)
        {
            pitch -= speedV * Input.GetAxis("cameraVertical");
        }
        transform.eulerAngles = new Vector3(pitch, yaw, 0.0f);
	}
}
