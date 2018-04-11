using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivateLookedAtObject : MonoBehaviour {

    [SerializeField]
    private float maxActivateDistance = 3.0f;

    [SerializeField]
    private Text lookedAtObjectText;

    [SerializeField]
    private Transform guide;

    private IActivatable objectLookedAt;

	
	// Update is called once per frame
	void FixedUpdate ()
    {
        Debug.DrawRay(guide.position, guide.forward * maxActivateDistance);

        UpdateObjectLookedAt();
        UpdateLookedAtObjectText();
        ActivateLookedAtObjects();

    }

    private void ActivateLookedAtObjects()
    {
        if(objectLookedAt != null)
        {
            if(Input.GetButtonDown("Interact"))
            {
                objectLookedAt.DoActivate();
            }
        }
    }

    private void UpdateLookedAtObjectText()
    {
        if(objectLookedAt != null)
        {
            lookedAtObjectText.text = objectLookedAt.NameText;
        }
        else
        {
            lookedAtObjectText.text = string.Empty;
        }
    }

    private void UpdateObjectLookedAt()
    {
        RaycastHit hit;
        objectLookedAt = null;

        if(Physics.Raycast(guide.position, guide.forward, out hit, maxActivateDistance))
        {
            objectLookedAt = hit.transform.GetComponent<IActivatable>();
        }
    }
}
