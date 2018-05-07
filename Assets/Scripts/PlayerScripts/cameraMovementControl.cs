using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovementControl : MonoBehaviour {

    Vector2 Look;
    Vector2 smoothv;
    [SerializeField]
    float sensivity = 5.0f;
    [SerializeField]
    float smoothing = 2.0f;

    private GameObject character;

    [SerializeField]
    string playerCameraHorizontal;
    [SerializeField]
    string playerCameraVertical;

	// Use this for initialization
	void Start ()
    {
        character = transform.parent.gameObject;
	}
	
	// Update is called once per frame
	void Update ()
    {
        //Allowing the player to move the camera up and down, left and right, with the controller
        var md = new Vector2(Input.GetAxisRaw(playerCameraHorizontal), Input.GetAxisRaw(playerCameraVertical));

        md = Vector2.Scale(md, new Vector2(sensivity * smoothing, sensivity * smoothing));
        smoothv.x = Mathf.Lerp(smoothv.x, md.x, 1f / smoothing);
        smoothv.y = Mathf.Lerp(smoothv.y, md.y, 1f / smoothing);
        Look += smoothv;
        //Limiting the camera up and down so that the player doesn't go to far in either way
        Look.y = Mathf.Clamp(Look.y, -30f, 25f);

        transform.localRotation = Quaternion.AngleAxis(-Look.y, Vector3.right);
        character.transform.localRotation = Quaternion.AngleAxis(Look.x, character.transform.up);
		
	}
}
