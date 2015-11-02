using UnityEngine;
using System.Collections;
using Vuforia;
using System.Collections.Generic;

public class vuforiaCapHeight : MonoBehaviour
{
    public float capHeight = 40;
    public float capAngle = 75;
    private CameraDevice.FocusMode autoFocus = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
    private CameraDevice.FocusMode nearFocus = CameraDevice.FocusMode.FOCUS_MODE_MACRO;

    private bool isNear = false;
    GameObject ARCamera;
    GameObject imageTarget;
    GameObject capWarning;

    // Use this for initialization
    void Start()
    {
        ARCamera = GameObject.Find("ARCamera");
        imageTarget = GameObject.Find("ImageTarget");
        capWarning = GameObject.Find("capWarning");
    }

    // Update is called once per frame
    void Update()
    {
        if (ARCamera.transform.position.y > capHeight && imageTarget.GetComponent<Vuforia.DefaultTrackableEventHandler>().trackerFound && ARCamera.transform.localEulerAngles.x > capAngle)
        {
            if (!isNear)
            {
                isNear = true;
                CameraDevice.Instance.SetFocusMode(nearFocus);
                capWarning.SetActive(true);
            }
        } else
        {
            if(isNear) CameraDevice.Instance.SetFocusMode(autoFocus);
            isNear = false;
            capWarning.SetActive(false);
        }

    }

    void OnTrackingFound()
    {
        Debug.Log("Test");
    }

}
