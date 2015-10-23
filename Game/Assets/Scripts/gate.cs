using UnityEngine;
using System.Collections;
[RequireComponent(typeof(NavMeshObstacle))]
[RequireComponent(typeof(BoxCollider))]

public class gate : MonoBehaviour {
    public bool isActivated = false;
    public Vector3 activationLocation;
    public Vector3 activationRotation;
    Vector3 initialLocation;
    Vector3 initialRotation;
    Vector3 currentAngle;
    bool on = false;
    NavMeshObstacle obstacle;

    // Use this for initialization
    void Start () {
      //  obstacle = GetComponent<NavMeshObstacle>();
        initialLocation = this.transform.localPosition;
        initialRotation = this.transform.localEulerAngles;
    }
	
	// Update is called once per frame
	void Update () {
        if (isActivated) {
            if(activationLocation == Vector3.zero && activationRotation == Vector3.zero)
            {
      //          this.gameObject.GetComponent<NavMeshObstacle>().enabled = false;
                this.gameObject.GetComponent<Renderer>().enabled = false;
                this.gameObject.GetComponent<BoxCollider>().enabled = false;
            }
            else if (activationLocation == Vector3.zero)
            {
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, activationRotation.x, Time.deltaTime * 2),
                    Mathf.LerpAngle(currentAngle.y, activationRotation.y, Time.deltaTime * 2),
                    Mathf.LerpAngle(currentAngle.z, activationRotation.z, Time.deltaTime * 2)
                );
                transform.eulerAngles = currentAngle;
            } else if (activationRotation == Vector3.zero)
            {
                this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, activationLocation, 0.1f);
            }
        } else {
            this.transform.localPosition = Vector3.Lerp(this.transform.localPosition, initialLocation, 0.1f);
            currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, initialRotation.x, Time.deltaTime * 2),
                    Mathf.LerpAngle(currentAngle.y, initialRotation.y, Time.deltaTime * 2),
                    Mathf.LerpAngle(currentAngle.z, initialRotation.z, Time.deltaTime * 2)
                );
            transform.eulerAngles = currentAngle;
      //      this.gameObject.GetComponent<NavMeshObstacle>().enabled = true;
            this.gameObject.GetComponent<Renderer>().enabled = true;
            this.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
	}

    public void setIsActivated(bool isActivated) {
        this.isActivated = isActivated;
    }
}
