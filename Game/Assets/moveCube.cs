using UnityEngine;
using System.Collections;

public class moveCube : MonoBehaviour {
    public Vector3 reference;
    public Vector3 localForward;

	// Use this for initialization
	void Start () {
        reference = this.transform.position;
      //  localForward = Quaternion.Euler(this.transform.rotation.x, this.transform.rotation.y, this.transform.rotation.z) * localForward;
    }
	
	// Update is called once per frame
	void Update () {
        localForward = this.transform.forward;
        Debug.DrawLine(this.transform.position, transform.forward, Color.red);
	}
}
