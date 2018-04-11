using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveObject : MonoBehaviour, IActivatable
{
    [SerializeField]
    private GameObject item;
    [SerializeField]
    private GameObject temp;
    [SerializeField]
    private Transform guide;

    [SerializeField]
    private string nameText;

    public string NameText
    {
        get
        {
            return nameText;
        }
    }
    

	// Use this for initialization
	void Start ()
    {
        item.GetComponent<Rigidbody>().useGravity = true;
	}
	
	// Update is called once per frame
	public void DoActivate ()
    {
		if(Input.GetButton("Interact"))
        {
            item.GetComponent<Rigidbody>().useGravity = false;
            item.GetComponent<Rigidbody>().isKinematic = true;
            item.transform.position = guide.transform.position;
            item.transform.rotation = guide.transform.rotation;
            item.transform.parent = temp.transform;
        }
        else if (Input.GetButtonUp("Interact"))
        {
            item.GetComponent<Rigidbody>().useGravity = true;
            item.GetComponent<Rigidbody>().isKinematic = false;
            item.transform.parent = null;
            item.transform.position = guide.transform.position;
        }


	}
}
