using UnityEngine;
using System.Collections;

public class tileMovement : MonoBehaviour
{
    public float speed = 5f;
    public float distance = 1f;
    public bool isMoving = false;
    bool goingUp = false;
    bool goingDown = false;
    bool goingLeft = false;
    bool goingRight = false;
    bool goingNorthWest = false;
    bool goingNorthEast = false;
    bool goingSouthWest = false;
    bool goingSouthEast = false;

    NavMeshAgent navMeshAgent;
    private IEnumerator coroutine;

    // Use this for initialization
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            /*    var movement = new Vector3();
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

                movement = transform.TransformDirection(movement);
                movement *= speed;

                character.Move(movement * Time.deltaTime);*/
            Vector3 movement = new Vector3();
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
            movement *= speed;
            navMeshAgent.destination = this.transform.position + movement * Time.deltaTime;
            if(navMeshAgent.path.corners.Length > 2)
            {
                completeReset();
            }
        }
        

        
    }

    public void moveUp()
    {
        completeReset();
        goingUp = true;
        coroutine = stopMovement(distance, 1);
        StartCoroutine(coroutine);
      
    }

    public void moveDown()
    {
        completeReset();
        goingDown = true;
        coroutine = stopMovement(distance, 2);
        StartCoroutine(coroutine);
    }

    public void moveLeft()
    {
        completeReset();
        goingLeft = true;
        coroutine = stopMovement(distance, 3);
        StartCoroutine(coroutine);
    }

    public void moveRight()
    {
        completeReset();
        goingRight = true;
        coroutine = stopMovement(distance, 4);
        StartCoroutine(coroutine);
    }

    public void moveNorthWest()
    {
        completeReset();
        goingNorthWest = true;
        coroutine = stopMovement(distance, 5);
        StartCoroutine(coroutine);
    }

    public void moveNorthEast()
    {
        completeReset();
        goingNorthEast = true;
        coroutine = stopMovement(distance, 6);
        StartCoroutine(coroutine);
    }

    public void moveSouthWest()
    {
        completeReset();
        goingSouthWest = true;
        coroutine = stopMovement(distance, 7);
        StartCoroutine(coroutine);
    }

    public void moveSouthEast()
    {
        completeReset();
        goingSouthEast = true;
        coroutine = stopMovement(distance, 8);
        StartCoroutine(coroutine);
    }

    IEnumerator stopMovement(float waitTime, int direction)
    {
        isMoving = true;
        yield return new WaitForSeconds(waitTime);
        switch (direction)
        {
            case 1:
                goingUp = false;
                break;
            case 2:
                goingDown = false;
                break;
            case 3:
                goingLeft = false;
                break;
            case 4:
                goingRight = false;
                break;
            case 5:
                goingNorthWest = false;
                break;
            case 6:
                goingNorthEast = false;
                break;
            case 7:
                goingSouthWest = false;
                break;
            case 8:
                goingSouthEast = false;
                break;
        }
        isMoving = false;

    }

    public void completeReset()
    {
        if(coroutine != null)
        {
            StopCoroutine(coroutine);
        }

        goingUp = false;
        goingDown = false;
        goingLeft = false;
        goingRight = false;
        goingNorthWest = false;
        goingNorthEast = false;
        goingSouthWest = false;
        goingSouthEast = false;

        navMeshAgent.ResetPath();
     //   navMeshAgent.SetDestination(this.transform.position);
        isMoving = false;
    }


}
