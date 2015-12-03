using UnityEngine;
using System.Collections;

public class vuforiaOrientation : MonoBehaviour {
    public Transform referenceCube;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if(Application.loadedLevel > 0)
        {
            if (referenceCube == null) referenceCube = GameObject.Find("Reference Cube").transform;
            Vector3 _tmp = referenceCube.localEulerAngles;
            _tmp.y = this.transform.localEulerAngles.y;
            referenceCube.localEulerAngles = _tmp;
        }   
    }

    void OnLevelWasLoaded()
    {
        referenceCube = null;
    }


}
