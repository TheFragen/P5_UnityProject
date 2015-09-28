using UnityEngine;
using System.Collections;

public class tilePlacement : MonoBehaviour {
    Vector3 startPosition;


	// Use this for initialization
	void Start () {
        startPosition = this.transform.localPosition;
    }
	
	// Update is called once per frame
	void Update () {
        transform.position = startPosition/1.5f + this.transform.parent.position;
        transform.rotation = Quaternion.identity;
	}
}
