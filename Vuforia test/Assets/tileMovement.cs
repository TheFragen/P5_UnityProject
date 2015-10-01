using UnityEngine;
using System.Collections;

public class tileMovement : MonoBehaviour {
    public float speed = 5f;
    private Vector3 velocity = Vector3.zero;
    bool goingUp = false;
    bool goingDown = false;
    bool goingLeft = false;
    bool goingRight = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (goingUp)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + (Vector3.forward), ref velocity, 1f);
        }
        if (goingDown)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + (-Vector3.forward), ref velocity, 1f);
        }
        if (goingLeft)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + (-Vector3.right), ref velocity, 1f);
        }
        if (goingRight)
        {
            transform.position = Vector3.SmoothDamp(transform.position, transform.position + (Vector3.right), ref velocity, 1f);
        }
    }

    public void moveUp()
    {
        goingUp = true;
        StartCoroutine(stopMovement(1F,1));
    }

    public void moveDown()
    {
        goingDown = true;
        StartCoroutine(stopMovement(1F, 2));
    }

    public void moveLeft()
    {
        goingLeft = true;
        StartCoroutine(stopMovement(1F, 3));
    }

    public void moveRight()
    {
        goingRight = true;
        StartCoroutine(stopMovement(1F, 4));
    }

    IEnumerator stopMovement(float waitTime, int direction)
    {
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
        }
        velocity = Vector3.zero;

    }


}
