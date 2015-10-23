using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class cycleControls : MonoBehaviour {
    public Transform player;
    public List<Transform> tileBased;
    public List<Transform> joystick;
    public List<Transform> pointAndClick;

    public bool enableTilebasedMovement = false;
    public bool enableJoystick;
    public bool enablePointAndClick;


    // Use this for initialization
    void Start () {

    }
	
	// Update is called once per frame
	void Update () {
        if (enableTilebasedMovement) {
            enableJoystick = false;
            enablePointAndClick = false;

            foreach (Transform child in tileBased)
            {
                child.gameObject.SetActive(true);
            }

            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<tileMovement>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;

        } else if (enableJoystick) {
            enablePointAndClick = false;
            enableTilebasedMovement = false;
            player.GetComponent<CharacterControllerJoystick>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = false;

        } else if (enablePointAndClick)  {
            enableJoystick = false;
            enableTilebasedMovement = false;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<PointAndClick>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;

            foreach (Transform child in pointAndClick)
            {
                child.gameObject.SetActive(true);
            }
        } else
        {
            resetAll();
        }
    }

    void resetAll()
    {
        foreach (Transform child in pointAndClick)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in tileBased)
        {
            child.gameObject.SetActive(false);
        }

        player.GetComponent<CharacterControllerJoystick>().enabled = false;
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.GetComponent<PointAndClick>().enabled = false;
        player.GetComponent<tileMovement>().enabled = false;
        if(GameObject.Find("CustomJoystick(Clone)") != null)
        {
            Destroy(GameObject.Find("CustomJoystick(Clone)"));
        }
    }
}
