using UnityEngine;
using System.Collections;

public class vuforiaOrientation : MonoBehaviour {
    public Transform referenceCube;

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 _tmp = referenceCube.localEulerAngles;
        _tmp.y = this.transform.localEulerAngles.y;
        referenceCube.localEulerAngles = _tmp;
        Debug.Log("Set Reference Cube to: " + _tmp);
    }
}
