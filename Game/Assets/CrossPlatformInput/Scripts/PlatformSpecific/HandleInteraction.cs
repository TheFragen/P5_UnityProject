using UnityEngine;
using System.Collections;

public class HandleInteraction : MonoBehaviour {
#pragma warning disable CS0618 // Type or member is obsolete

    private GameObject thisHandlePivot;
    public GameObject objectToAffect;
    private bool handleTurn = false;
    public bool debugSwitch;
    private bool reverse = true;
    private Quaternion start;
    private Quaternion stop;
    private float time = 0.0f;
    private Renderer visible;
    public bool negative;
    private float eulerEnd;


    void Start () {
        thisHandlePivot = this.transform.parent.gameObject;
        eulerEnd = (negative) ? -3.14f : 3.14f;
        start = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        stop = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
    //    lastKnown = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        start.SetEulerAngles(0.0f, 0.0f, 0.0f);
        stop.SetEulerAngles(0.0f, 0.0f, eulerEnd);
    }
	
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player") {
            if (true) {
                handleTurn = true;
                reverse = !reverse;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (debugSwitch)
        {
            debugSwitch = !debugSwitch;
            handleTurn = true;
            reverse = !reverse;
        }

        if (handleTurn && reverse == false ) {
            if (thisHandlePivot.transform.rotation == stop)
            {
                start.SetEulerAngles(0.0f, 0.0f, 0.0f);
                stop.SetEulerAngles(0.0f, 0.0f, eulerEnd);
            }
            else
            {
                start= thisHandlePivot.transform.rotation;
                stop.SetEulerAngles(0.0f, 0.0f, eulerEnd);
            }

            thisHandlePivot.transform.rotation = Quaternion.Lerp(start, stop, time);
            time += 0.01f;
            if (thisHandlePivot.transform.rotation == stop)
            {
                time = 0;
                handleTurn = false;
                objectToAffect.gameObject.SetActive(false);
                foreach (Transform child in objectToAffect.transform)
                {
                    child.gameObject.SetActive(false);
                }
            
            }
        }
        else if (handleTurn && reverse == true)
       {
            if (thisHandlePivot.transform.rotation == stop)
            {
                start.SetEulerAngles(0.0f, 0.0f, eulerEnd);
                stop.SetEulerAngles(0.0f, 0.0f, 0f);
            }
            else
            {
                start = thisHandlePivot.transform.rotation;  
                stop.SetEulerAngles(0.0f, 0.0f, 0.0f);
            }
            thisHandlePivot.transform.rotation = Quaternion.Lerp(start, stop, time);
            time += 0.01f;
            if(thisHandlePivot.transform.rotation == stop)
            {
                time = 0;
                handleTurn = false;
                objectToAffect.gameObject.SetActive(true);
                foreach (Transform child in objectToAffect.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
