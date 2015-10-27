using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class cycleControls : MonoBehaviour {
    public Transform player;
    public List<Transform> tileBased;
    public List<Transform> joystick;
    public List<Transform> pointAndClick;

    public bool enableTilebasedMovement { get; set; }
    public bool enableJoystick { get; set; }
    public bool enablePointAndClick { get; set; }
    public bool enableFingerClick { get; set; }


    // Use this for initialization
    void Start () {

    }

    // Update is called once per frame
    void Update() {
        if (enableTilebasedMovement) {
            enableJoystick = false;
            enablePointAndClick = false;
            enableFingerClick = false;

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
            enableFingerClick = false;
            player.GetComponent<CharacterControllerJoystick>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = false;

        } else if (enablePointAndClick) {
            enableJoystick = false;
            enableTilebasedMovement = false;
            enableFingerClick = false;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<PointAndClick>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("Canvas/redicule").GetComponent<Image>().enabled = true;
            
            foreach (Transform child in pointAndClick)
            {
                child.gameObject.SetActive(true);
            }
        } else if (enableFingerClick) {
            enableJoystick = false;
            enableTilebasedMovement = false;
            enablePointAndClick = false;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;
            player.GetComponent<fingerClick>().enabled = true;

        }
        else {
            resetAll();
        }
    }

    public void resetAll()
    {
        enableJoystick = false;
        enableTilebasedMovement = false;
        enablePointAndClick = false;
        enableFingerClick = false;

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
        player.GetComponent<fingerClick>().enabled = false;
        GameObject.Find("Canvas/redicule").GetComponent<Image>().enabled = false;

        if (GameObject.Find("CustomJoystick(Clone)") != null)
        {
            Destroy(GameObject.Find("CustomJoystick(Clone)"));
        }
    }
}
