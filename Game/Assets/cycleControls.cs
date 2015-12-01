using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
public class cycleControls : MonoBehaviour {
    public Transform player;
    public List<Transform> tileBased;
    public List<Transform> joystick;
    public List<Transform> pointAndClick;
    public GameObject pointAndClickButton;
    public GameObject tilebasedButton;
    public GameObject fingerClickButton;
    public GameObject joystickButton;

    public bool enableTilebasedMovement { get; set; }
    public bool enableJoystick { get; set; }
    public bool enablePointAndClick { get; set; }
    public bool enableFingerClick { get; set; }
    Color32 disableColor = new Color32(114, 16, 16, 255);
    Color32 enabledColor = new Color32(19, 144, 19, 255);
    public Transform showUpGoddammitJoystick;

    // Use this for initialization
    void Start () {
        resetAll();
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
            tilebasedButton.GetComponent<Image>().color = enabledColor;
        } else if (enableJoystick) {
            enablePointAndClick = false;
            enableTilebasedMovement = false;
            enableFingerClick = false;

            showUpGoddammitJoystick.gameObject.SetActive(true);
            foreach (Transform child in showUpGoddammitJoystick)
            {
                child.gameObject.SetActive(true);
            }
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<CharacterControllerJoystick>().enabled = true;
   //         player.GetComponent<Rigidbody>().isKinematic = false;
            joystickButton.GetComponent<Image>().color = enabledColor;
            

        } else if (enablePointAndClick) {
            enableJoystick = false;
            enableTilebasedMovement = false;
            enableFingerClick = false;
            player.GetComponent<NavMeshAgent>().enabled = true;
            player.GetComponent<PointAndClick>().enabled = true;
            player.GetComponent<Rigidbody>().isKinematic = true;
            GameObject.Find("Canvas/redicule").GetComponent<Image>().enabled = true;
            pointAndClickButton.GetComponent<Image>().color = enabledColor;

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
            fingerClickButton.GetComponent<Image>().color = enabledColor;

        }
        else {
        //    resetAll();
        }
    }

    public void resetAll()
    {
        Debug.Log("Reset");
        enableFingerClick = false;
        enableJoystick = false;
        enableTilebasedMovement = false;
        enablePointAndClick = false;

        showUpGoddammitJoystick.gameObject.SetActive(false);
        foreach (Transform child in showUpGoddammitJoystick)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in pointAndClick)
        {
            child.gameObject.SetActive(false);
        }

        foreach (Transform child in tileBased)
        {
            child.gameObject.SetActive(false);
        }

        player.GetComponent<CharacterControllerJoystick>().enabled = false;
        player.GetComponent<NavMeshAgent>().speed = 15;
        player.GetComponent<NavMeshAgent>().enabled = false;
        player.GetComponent<PointAndClick>().enabled = false;
        player.GetComponent<tileMovement>().enabled = false;
        player.GetComponent<fingerClick>().enabled = false;
        GameObject.Find("Canvas/redicule").GetComponent<Image>().enabled = false;

        if (GameObject.Find("CustomJoystick(Clone)") != null)
        {
            Destroy(GameObject.Find("CustomJoystick(Clone)"));
        }

        
        Renderer[] rendererComponents = player.GetComponentsInChildren<Renderer>(true);

        // Disable rendering:
        foreach (Renderer component in rendererComponents)
        {
            if (component.gameObject.name.Contains("Tile"))
            {
                component.enabled = false;
            }
        }


        pointAndClickButton.GetComponent<Image>().color = disableColor;
        fingerClickButton.GetComponent<Image>().color = disableColor;
        tilebasedButton.GetComponent<Image>().color = disableColor;
        joystickButton.GetComponent<Image>().color = disableColor;

    }

    public string getCurrentControlScheme()
    {
        if (enableTilebasedMovement) return "Tile Based";
        if (enableJoystick) return "Joystick";
        if (enablePointAndClick) return "Point and Click";
        if (enableFingerClick) return "Finger Click";
        return "None";
    }
}
