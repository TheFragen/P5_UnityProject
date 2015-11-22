using UnityEngine;
using System.Collections;

public class fogOfWay_player : MonoBehaviour {
    public Transform plane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        plane.GetComponent<Renderer>().material.SetVector("_PlayerPoint", this.transform.position);
        plane.GetComponent<Renderer>().material.SetFloat("_DistancePlayer", 5);
    }
}
