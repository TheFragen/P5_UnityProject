using UnityEngine;
using System.Collections;

public class settingsButton : MonoBehaviour {
    private bool enableSettings = false;
    private bool fireOnce = true;
    private GameObject controlCycler;
    private GameObject orientation;

    // Use this for initialization
    void Start () {
        controlCycler = GameObject.Find("controlCycler");
        orientation = GameObject.Find("orientation");
    }
	
	// Update is called once per frame
	void Update () {
        if (enableSettings)
        {
            controlCycler.SetActive(true);
            orientation.SetActive(true);
            if(fireOnce) GameObject.Find("Control Cycler").GetComponent<cycleControls>().resetAll(); fireOnce = false;
        }
        else if (controlCycler.activeSelf && !enableSettings)
        {
            GameObject.Find("Control Cycler").GetComponent<cycleControls>().resetAll();
            controlCycler.SetActive(false);
            orientation.SetActive(false);
            fireOnce = true;
        }
	}

    public void changeSettings()
    {
        enableSettings = !enableSettings;
    }
}
