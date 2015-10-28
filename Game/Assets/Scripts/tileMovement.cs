using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class tileMovement : MonoBehaviour
{
    public float speed = 5f;
    public bool isMoving = false;
    bool goingUp = false;
    bool goingDown = false;
    bool goingLeft = false;
    bool goingRight = false;
    bool goingNorthWest = false;
    bool goingNorthEast = false;
    bool goingSouthWest = false;
    bool goingSouthEast = false;
    public bool useLocalOrientation = true;

    NavMeshAgent navMeshAgent;
    CharacterController character;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {   
        if (isMoving) {
            Vector3 movement = new Vector3();

            //This is really ugly code, but works
            if (useLocalOrientation)
            {
                if (goingUp)
                {
                    movement = new Vector3(0, 0, 1);
                }
                if (goingDown)
                {
                    movement = new Vector3(0, 0, -1);
                }
                if (goingLeft)
                {
                    movement = new Vector3(-1, 0, 0);
                }
                if (goingRight)
                {
                    movement = new Vector3(1, 0, 0);
                }
                if (goingNorthWest)
                {
                    movement = new Vector3(-0.5f, 0, 0.5f);
                }
                if (goingNorthEast)
                {
                    movement = new Vector3(0.5f, 0, 0.5f);
                }
                if (goingSouthWest)
                {
                    movement = new Vector3(-0.5f, 0, -0.5f);
                }
                if (goingSouthEast)
                {
                    movement = new Vector3(0.5f, 0, -0.5f);
                }
            } else
            {
                if (goingUp)
                {
                    movement = GameObject.Find("Reference Cube").transform.forward;
                }
                if (goingDown)
                {
                    movement = -GameObject.Find("Reference Cube").transform.forward;
                }
                if (goingLeft)
                {
                    movement = -GameObject.Find("Reference Cube").transform.right;
                }
                if (goingRight)
                {
                    movement = GameObject.Find("Reference Cube").transform.right;
                }
                if (goingNorthWest)
                {
                    movement = (GameObject.Find("Reference Cube").transform.forward - GameObject.Find("Reference Cube").transform.right).normalized;
                }
                if (goingNorthEast)
                {
                    movement = (GameObject.Find("Reference Cube").transform.forward + GameObject.Find("Reference Cube").transform.right).normalized;
                }
                if (goingSouthWest)
                {
                    movement = (-GameObject.Find("Reference Cube").transform.forward - GameObject.Find("Reference Cube").transform.right).normalized;
                }
                if (goingSouthEast)
                {
                    movement = (-GameObject.Find("Reference Cube").transform.forward + GameObject.Find("Reference Cube").transform.right).normalized;
                }
            }

            movement *= speed;
            navMeshAgent.destination = this.transform.position + movement * Time.deltaTime;
        }

        if (useLocalOrientation)
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                if (component.gameObject.name == "Capsule") continue;
                component.enabled = true;
            }
        }  else
        {
            Renderer[] rendererComponents = GetComponentsInChildren<Renderer>(true);

            // Disable rendering:
            foreach (Renderer component in rendererComponents)
            {
                if (component.gameObject.name == "Capsule") continue;
                component.enabled = false;
            }
        }
    }

    void movePlayer(Vector3 movement) {
        movement *= speed;
        navMeshAgent.destination = this.transform.position + movement * Time.deltaTime;
    }

    public void moveUp(int val)
    {
        if (val == 1) {
            goingUp = true;
            isMoving = true;
        } else {
            completeReset();
        }
    }

    public void moveDown(int val)
    {
        
        if (val == 1) {
            isMoving = true;
            goingDown = true;
        } else {
            completeReset();
        }
    }

    public void moveLeft(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingLeft = true;
        } else {
            completeReset();
        }
    }

    public void moveRight(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingRight = true;
        } else {
            completeReset();
        }
    }

    public void moveNorthWest(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingNorthWest = true;
        } else {
            completeReset();
        }
    }

    public void moveNorthEast(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingNorthEast = true;
        } else {
            completeReset();
        }
    }

    public void moveSouthWest(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingSouthWest = true;
        } else {
            completeReset();
        }
    }

    public void moveSouthEast(int val)
    {
        if (val == 1) {
            isMoving = true;
            goingSouthEast = true;
        } else {
            completeReset();
        }
    }

    public void completeReset()
    {
        goingUp = false;
        goingDown = false;
        goingLeft = false;
        goingRight = false;
        goingNorthWest = false;
        goingNorthEast = false;
        goingSouthWest = false;
        goingSouthEast = false;

        navMeshAgent.ResetPath();
        isMoving = false;
    }

    public void setLocalOrientation()
    {
        if (useLocalOrientation == true)
        {
            useLocalOrientation = false;
            GameObject.Find("Canvas/orientation/Text").GetComponent<Text>().text = "Local Orientation";
        } else
        {
            useLocalOrientation = true;
            GameObject.Find("Canvas/orientation/Text").GetComponent<Text>().text = "Camera Orientation";
        }
    }
}
