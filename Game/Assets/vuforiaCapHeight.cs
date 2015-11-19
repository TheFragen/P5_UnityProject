using UnityEngine;
using Vuforia;

public class vuforiaCapHeight : MonoBehaviour
{
    public float capHeight = 40;
    public float capAngle = 75;
    public bool isDebug = false;
    private CameraDevice.FocusMode autoFocus = CameraDevice.FocusMode.FOCUS_MODE_CONTINUOUSAUTO;
    private CameraDevice.FocusMode nearFocus = CameraDevice.FocusMode.FOCUS_MODE_MACRO;

    private bool isNear = false;

    GameObject ARCamera;
    GameObject imageTarget;
    GameObject capWarning;

    // Use this for initialization
    void Start()
    {
        if (isDebug)
        {
            ARCamera = GameObject.Find("Camera");
            imageTarget = GameObject.Find("Debug World");
        }
        else
        {
            ARCamera = GameObject.Find("ARCamera");
            imageTarget = GameObject.Find("ImageTarget");
        }

        capWarning = GameObject.Find("capWarning");
    }

    // Update is called once per frame
    void Update()
    {
        bool condition = false;
        if (isDebug)
        {
            condition = ARCamera.transform.position.y >= capHeight && ARCamera.transform.localEulerAngles.x >= capAngle;
        } else {
            condition = ARCamera.transform.position.y > capHeight && imageTarget.GetComponent<DefaultTrackableEventHandler>().trackerFound && ARCamera.transform.localEulerAngles.x > capAngle;
        }

        if (condition)
        {
            if (!isNear)
            {
                if (!isDebug) CameraDevice.Instance.SetFocusMode(nearFocus);
                isNear = true;
                capWarning.SetActive(true);
            }
        } else
        {
            if (!isDebug && isNear) CameraDevice.Instance.SetFocusMode(autoFocus);
            isNear = false;
            capWarning.SetActive(false);
        }
    }

}
