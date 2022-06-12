using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGrab : MonoBehaviour
{
    [SerializeField] private OVRInput.Controller controller;
    [SerializeField] private OVRInput.Button actionButton;

    [SerializeField] private GameObject onGrabObj = null;
    [SerializeField] private RingController onGrabScript = null;

    [SerializeField] private GameObject _handGrabContainerObj = null;
    public GameObject HandGrabContainerObj
    {
        get { return _handGrabContainerObj; }
        set { _handGrabContainerObj = value; }
    }

    private void OnTriggerEnter(Collider other)
    {
        //get the reference of grabbale object
        if (other.gameObject.CompareTag("Grabbable"))
        {
            onGrabObj = other.transform.parent.gameObject;
            onGrabScript = onGrabObj.GetComponent<RingController>();
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Grabbable"))
        {
            if (onGrabObj != null && other.transform.parent.gameObject == onGrabObj)
            {
                onGrabObj = null;
                onGrabScript = null;
            }
        }
    }

    private void Update()
    {
        //grab
        if (OVRInput.GetDown(actionButton, controller))
        {
            //when a grabbale object in range
            if (onGrabObj != null)
            {
                onGrabObj.transform.parent = _handGrabContainerObj.transform;
                onGrabScript.OnGrabbingScript = this;
            }
        }

        //release
        if (OVRInput.GetUp(actionButton, controller))
        {
            if (onGrabObj != null)
            {
                onGrabObj.transform.parent = null;
                onGrabScript.OnGrabbingScript = null;
            }
            else
            {
                foreach(Transform child in _handGrabContainerObj.transform)
                {
                    child.parent = null;
                }
            }
        }
    }

    public void ReleaseHolding()
    {
        if (onGrabObj != null)
        {
            onGrabObj.transform.parent = null;
            onGrabObj = null;
            onGrabScript = null;
        }
    }
}
