using UnityEngine;
using System.Collections;

public class fogOfWar_Script : MonoBehaviour {
    public Transform plane;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        var ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 2, Screen.height / 2, Camera.main.nearClipPlane));

        RaycastHit hit;
        if (Physics.Raycast(ray, out hit)) {
            plane.GetComponent<Renderer>().material.SetVector("_Point", hit.point);
            float dist = Vector3.Distance(this.transform.position, hit.point);
            plane.GetComponent<Renderer>().material.SetFloat("_DistanceNear", dist / 2);
        } else {
            plane.GetComponent<Renderer>().material.SetFloat("_DistanceNear", 0);
        }

           
    }
}
