using UnityEngine;
using System.Collections;

public class HandleInteraction : MonoBehaviour {
    private GameObject Switch;
    private GameObject Laser;
    private bool handleTurn = false;
    private bool reverse = true;
    private bool going = false;
    private Quaternion start;
    private Quaternion stop;
    private float time = 0.0f;
    private Renderer visible;
    private Quaternion lastKnown;
    // Use this for initialization
    void Start () {
	Switch = GameObject.Find("HandlePivot");
        Laser = GameObject.Find("Lasers");
        start = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        stop = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        lastKnown = new Quaternion(0.0f, 0.0f, 0.0f, 1.0f);
        start.SetEulerAngles(0.0f, 0.0f, 0.0f);
        stop.SetEulerAngles(0.0f, 0.0f, 3.14f);
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
        if (handleTurn && reverse == false ) {
            if (Switch.transform.rotation == stop)
            {
                start.SetEulerAngles(0.0f, 0.0f, 0.0f);
                stop.SetEulerAngles(0.0f, 0.0f, 3.14f);
            }
            else
            {
                start=Switch.transform.rotation;
                stop.SetEulerAngles(0.0f, 0.0f, 3.14f);
            }

            Switch.transform.rotation = Quaternion.Lerp(start, stop, time);
            time += 0.01f;
            if (Switch.transform.rotation == stop)
            {
                time = 0;
                handleTurn = false;
                foreach(Transform child in Laser.transform)
                {
                    child.gameObject.SetActive(false);
                }
            
            }
        }
        else if (handleTurn && reverse == true)
       {
            if (Switch.transform.rotation == stop)
            {
                start.SetEulerAngles(0.0f, 0.0f, 3.14f);
                stop.SetEulerAngles(0.0f, 0.0f, 0f);
            }
            else
            {
                start = Switch.transform.rotation;  
                stop.SetEulerAngles(0.0f, 0.0f, 0.0f);
            }
            Switch.transform.rotation = Quaternion.Lerp(start, stop, time);
            time += 0.01f;
            if(Switch.transform.rotation == stop)
            {
                time = 0;
                handleTurn = false;
                foreach (Transform child in Laser.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
        }
    }
}
