﻿using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControllerJoystick : MonoBehaviour {
    //input
    public Vector2 moveVec;
    Rigidbody rBody;
    //a little delay on the input
    public float inputDelay = 0.1f;

    //the speed settings
    public float forwardVel = 1;
    public float rotateVel = 100;
    Quaternion rotation;
    Vector3 direction;
    Vector3 targetDirection = Vector3.zero;
    public bool CameraOrientation = true;
    public bool locked = true;
    VirtualJoystick2 joystick;
    public GameObject CustomJoystick;

   // NavMeshAgent navMeshAgent;


    // Use this for initialization
    void Start()
    {
        joystick = gameObject.GetComponent<VirtualJoystick2>();

        Quaternion rotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
    }
    void Getinput()
    {
        if(locked == true)  moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            
        if(locked == false) moveVec = new Vector2(joystick.movement.x, -joystick.movement.y);
    }

    // Update is called once per frame
    void Update()
    {
        if(locked == false)
        {
            joystick.enabled = true;
            Destroy(GameObject.Find("CustomJoystick(Clone)"));
        }
        else
        {
            if(GameObject.Find("CustomJoystick(Clone)") == null)
            {
                Instantiate(CustomJoystick, new Vector3(0, 0, 0), Quaternion.identity);
            }
            joystick.enabled = false;
        }
        Getinput();
        //print("moveVec is: " + moveVec);
    }
    void FixedUpdate()
    {
        Run();
        Rotating();
        
    }
    void Run()
    {

        //forward and backwards movement
        if (Mathf.Abs(moveVec.y) > 0 || Mathf.Abs(moveVec.x) > 0)
        {
            if(CameraOrientation == false)
            {
                //calculate the direction vector for global coordinates
                direction = (transform.TransformDirection(-transform.forward) * moveVec.y * forwardVel) +
                (transform.TransformDirection(transform.right) * moveVec.x * forwardVel);

                //calculate the local 
                //direction = (-transform.forward * moveVec.y) * forwardVel +
                //(transform.right * moveVec.x) * forwardVel;

            }
            if (CameraOrientation == true) { 
            //calculate the direction vector based on the camera position
            Vector3 ydirection = Camera.main.transform.forward * moveVec.y * forwardVel;
            Vector3 Xdirection = Camera.main.transform.right * moveVec.x * forwardVel;
                direction = ydirection + Xdirection;
            }
            
            //navMeshAgent.destination = direction * 10 + this.transform.position;
            rBody.velocity = direction;

            //Debug.Log("direction" + direction);

        }
        else
        { 
            StartCoroutine(Delay());
        }
    }

    //makes a smooth delay
    IEnumerator Delay()
    {
        if (!(Mathf.Abs(moveVec.y) > 0 || Mathf.Abs(moveVec.x) > 0))
        {
            yield return new WaitForSeconds(0.1f);
        }
         rBody.velocity = Vector3.zero;
    }

    void Rotating()
    {
          float turnSmoothing = 15f;
          
        // Create a new vector of the horizontal and vertical inputs.
        if (CameraOrientation == true)  targetDirection = new Vector3(direction.x, 0f, direction.z);
        if(CameraOrientation == false) targetDirection = new Vector3(moveVec.x, 0f, moveVec.y);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rBody.MoveRotation(newRotation);
    }
}
