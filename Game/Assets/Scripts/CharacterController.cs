using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;

public class CharacterController : MonoBehaviour {
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
   // NavMeshAgent navMeshAgent;


    // Use this for initialization
    void Start()
    {

        Quaternion rotation = transform.rotation;
        rBody = GetComponent<Rigidbody>();
      //  navMeshAgent = this.GetComponent<NavMeshAgent>();

      
    }
    void Getinput()
    {
        moveVec = new Vector2(CrossPlatformInputManager.GetAxis("Horizontal"), CrossPlatformInputManager.GetAxis("Vertical"));
    }

    // Update is called once per frame
    void Update()
    {
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

            //calculate the direction vector for global coordinates
            // direction = (transform.TransformDirection(-transform.forward) * moveVec.y * forwardVel) + (transform.TransformDirection (transform.right) * moveVec.x * forwardVel);

            //calculate the direction vector based on the camera position
            Vector3 ydirection = Camera.main.transform.forward * moveVec.y * forwardVel;
            Vector3 Xdirection = Camera.main.transform.right * moveVec.x * forwardVel;
            
            direction = ydirection + Xdirection;
            //navMeshAgent.destination = direction * 10 + this.transform.position;
            rBody.velocity = direction;

            Debug.Log("direction" + direction);

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
          Vector3 targetDirection = new Vector3(direction.x, 0f, direction.z);

        // Create a rotation based on this new vector assuming that up is the global y axis.
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection, Vector3.up);

        // Create a rotation that is an increment closer to the target rotation from the player's rotation.
        Quaternion newRotation = Quaternion.Lerp(rBody.rotation, targetRotation, turnSmoothing * Time.deltaTime);

        // Change the players rotation to this new rotation.
        rBody.MoveRotation(newRotation);
    }
}
