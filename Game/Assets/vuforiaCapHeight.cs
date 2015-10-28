using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class vuforiaCapHeight : MonoBehaviour {
    private CameraDevice.FocusMode autoFocus = CameraDevice.FocusMode.FOCUS_MODE_NORMAL;
    private CameraDevice.FocusMode nearFocus = CameraDevice.FocusMode.FOCUS_MODE_MACRO;

    private bool isNear = false;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        StateManager stateManager = TrackerManager.Instance.GetStateManager();
        IEnumerable<TrackableBehaviour> activeTrackables = stateManager.GetActiveTrackableBehaviours();
     //   Debug.Log(Input.acceleration.y);

        if (false)
        {
            if (!isNear)
            {
                CameraDevice.Instance.SetFocusMode(nearFocus);
                isNear = true;
                Debug.Log("Above");
            }
            
        } else
        {
            if (isNear)
            {
                CameraDevice.Instance.SetFocusMode(autoFocus);
                isNear = false;
                Debug.Log("Lower");
            }
        }
        
    }

    void OnTrackingFound()
    {
        Debug.Log("Test");
    }

}
