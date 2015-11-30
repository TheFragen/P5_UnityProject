using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterControllerJoystick : MonoBehaviour {
    //input
    public Vector2 moveVec;
    //    Rigidbody rBody;
    NavMeshAgent agent;
    //a little delay on the input
    public float inputDelay = 0.1f;

    //the speed settings
    public float forwardVel = 1;
    public float rotateVel = 100;
    Quaternion rotation;
    Vector3 direction;
    Vector3 targetDirection = Vector3.zero;
    public bool CameraOrientation = false;
    public bool locked = true;
    VirtualJoystick2 joystick;
    public GameObject CustomJoystick;
    public GameObject goJoystick;
    public Sprite colorCodedJoystick;
    public Sprite blackJoystick;
    bool setImageOnce = false;

   // NavMeshAgent navMeshAgent;

    // Use this for initialization
    void Start()
    {
        joystick = gameObject.GetComponent<VirtualJoystick2>();
        //     rBody = GetComponent<Rigidbody>();
        agent = GetComponent<NavMeshAgent>();
    }
    void Getinput()
    {
        if(locked == true)  moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
            
        if(locked == false) moveVec = new Vector2(joystick.movement.x, -joystick.movement.y);
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(this.transform.position, this.transform.position + this.transform.forward, Color.blue);
        /* if(goJoystick != null)
         {
             goJoystick.GetComponent<RectTransform>().position = new Vector3(70, 70, 0);
         }
         */
        if (locked == false)
        {
            joystick.enabled = true;
            Destroy(GameObject.Find("CustomJoystick(Clone)"));
        }
        else
        {
            if(GameObject.Find("CustomJoystick(Clone)") == null)
            {
            //    goJoystick = Instantiate(CustomJoystick, CustomJoystick.GetComponent<RectTransform>().position, Quaternion.identity) as GameObject;
                
            //    goJoystick.GetComponent<RectTransform>().position = new Vector3(70, 70, 1);
            //    goJoystick.GetComponent<RectTransform>().localScale = new Vector3(.8f, .8f, .8f);
             //   goJoystick.transform.parent = GameObject.Find("Canvas").transform;


                //foreach (Transform child in goJoystick.transform)
             //   {
                /*    float _width = child.GetComponent<RectTransform>().rect.width;
                    float _height = child.GetComponent<RectTransform>().rect.height;
                    child.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width/4, Screen.width/4);*/
                 //   child.parent = GameObject.FindGameObjectWithTag("Joystick").transform;
             //   }
          //      goJoystick.transform.localScale = new Vector3(Screen.width/12, Screen.width / 12, 0);

            }
            joystick.enabled = false;
        }

        if (CameraOrientation)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                if (component.gameObject.name.Contains("Tile"))
                {
                    component.enabled = false;
                }
            }

            // if (!setImageOnce)
            //  {
            setImageOnce = true;
                goJoystick.GetComponentsInChildren<Transform>()[1].GetComponent<Image>().sprite = blackJoystick;
         //   }

        } else
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Enable rendering:
            foreach (Renderer component in rendererComponents)
            {
                if (component.gameObject.name.Contains("Tile"))
                {
                    component.enabled = true;
                }
            }
            //    if (setImageOnce)
            //     {
            setImageOnce = false;
               // goJoystick.GetComponentsInChildren<Transform>()[1].GetComponent<Image>().sprite = colorCodedJoystick;
         //   }
        }

        Run();
        Getinput();
        //print("moveVec is: " + moveVec);
    }
    void FixedUpdate()
    {
       
   //     Rotating();
        
    }
    void Run()
    {

        //forward and backwards movement
        if (Mathf.Abs(moveVec.y) > 0 || Mathf.Abs(moveVec.x) > 0)
        {
            if(CameraOrientation == false)
            {
                //calculate the direction vector for global coordinates
             /*   direction = (transform.TransformDirection(-transform.forward) * moveVec.y * forwardVel) +
                (transform.TransformDirection(transform.right) * moveVec.x * forwardVel);
*/
                //calculate the local 
                //direction = (-transform.forward * moveVec.y) * forwardVel +
                //(transform.right * moveVec.x) * forwardVel;

                Vector3 ydirection = GameObject.FindGameObjectWithTag("Ground").transform.forward * moveVec.y * forwardVel;


                Vector3 Xdirection = GameObject.FindGameObjectWithTag("Ground").transform.right * moveVec.x * forwardVel;
                direction = ydirection + Xdirection;

            }
            if (CameraOrientation == true) { 
            //calculate the direction vector based on the camera position
                Vector3 ydirection = GameObject.Find("Reference Cube").transform.forward * moveVec.y * forwardVel;
                Vector3 Xdirection = GameObject.Find("Reference Cube").transform.right * moveVec.x * forwardVel;
                direction = ydirection + Xdirection;
            }
            Vector3 movement = direction.normalized;
            movement *= 86;
            agent.destination = this.transform.position + movement * Time.deltaTime;
        //    rBody.velocity = direction;

             Debug.Log("direction" + direction.normalized);

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
        agent.destination = this.transform.position + Vector3.zero;
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
   //     Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
  //      rBody.MoveRotation(newRotation);
    }

    public void setCameraOrientation()
    {
        CameraOrientation = !CameraOrientation;
    }
}
