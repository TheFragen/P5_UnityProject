using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour {
    public Vector3 initial;
	// Use this for initialization
	void Start () {
        initial = this.transform.position;
    }
	
	// Update is called once per frame
	void Update () {
        this.transform.position = initial;
        this.transform.rotation = Quaternion.identity;
    }
}
