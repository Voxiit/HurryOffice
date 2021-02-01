using UnityEngine;
using System;

public class Grabber : MonoBehaviour
{
    //Attributes
    //Objects and references
    [SerializeField]
    private Camera _playerCamera;

    private GameObject _currGrabdObj;

    private Transform _currGrabObjPrevParent;

    //Values
    [SerializeField]
    private float _grabDistance = 3f;

    [SerializeField]
    private float _throwForce = 1000f;

    private bool _objectIsGrab = false;

    [SerializeField]
    private float _objGrabDist = 1f;

    //Action
    public static Action PlayerWin;


    //Functions
    void Update()
    {
        //Update distance
        if(_objectIsGrab)
        {
            UpdateDistance();
        }

        //Check if we press left click for grabbing
        if(Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (!_objectIsGrab)
            {
                Grab();
            }
            else
            {
                Release();
            }
        }

        //Check f we want to throw it
        else if(Input.GetKeyDown(KeyCode.Mouse1) && _objectIsGrab)
        {
            Throw();
        }
    }

    //We want the object to always be at the same distance
    private void UpdateDistance()
    {
        _currGrabdObj.transform.position = _playerCamera.transform.position + _playerCamera.transform.forward * _objGrabDist;
    }

    private void Grab()
    {
        //Throw a ray, check if ther's an object with "Grab object" of "Winning object" as a tag
        RaycastHit hit;

        if (Physics.Raycast(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward), out hit, _grabDistance))
        {
            //Debug.DrawRay(_playerCamera.transform.position, _playerCamera.transform.TransformDirection(Vector3.forward) * hit.distance, Color.red);
            //Debug.Log("Did Hit : " + hit.transform.gameObject.name + " whith tag : " + hit.transform.gameObject.tag);
            //Get the object and tag
            GameObject hitObject = hit.transform.gameObject;
            string hitObjectTag = hitObject.tag;


            //Check if they have the correct tag
            if (hitObjectTag== "Victory Object")
            {
                Debug.Log("YOU WIN");
                if(PlayerWin !=null)
                {
                    PlayerWin();
                }
                return;
            }

            if(hitObjectTag == "Grabable Object")
            {
                //Set the value for current grab object
                _currGrabObjPrevParent = hitObject.transform.parent.gameObject.transform;
                _currGrabdObj = hitObject;
                _objectIsGrab = true;

                //Change it's parent
                _currGrabdObj.transform.parent = _playerCamera.transform;

                //Change it's kinematic
                _currGrabdObj.GetComponent<Rigidbody>().isKinematic = true;
            }
        }

    }

    private void Release()
    {
        //Change grab obj kinematic
        _currGrabdObj.GetComponent<Rigidbody>().isKinematic = false;
        _currGrabdObj.GetComponent<Rigidbody>().useGravity = true;

        ResetCurrentGrabObject();
    }

    private void Throw()
    {
        //Change grab obj kinematic
        _currGrabdObj.GetComponent<Rigidbody>().isKinematic = false;
        _currGrabdObj.GetComponent<Rigidbody>().useGravity = true;

        //Let's throw the object !
        _currGrabdObj.GetComponent<Rigidbody>().AddForce(_playerCamera.transform.forward * _throwForce);

        ResetCurrentGrabObject();
    }

    //Reset the value for the current grab object
    private void ResetCurrentGrabObject()
    {
        //Set the value for current grab object
        _currGrabdObj.transform.parent = _currGrabObjPrevParent.transform;
        _objectIsGrab = false;
        _currGrabObjPrevParent = null;
        _currGrabdObj = null;
    }
}
