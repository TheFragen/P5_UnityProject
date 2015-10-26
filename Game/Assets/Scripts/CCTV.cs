using UnityEngine;
using System.Collections;
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]

public class CCTV : MonoBehaviour {

    public float fovAngle = 0f;
	public bool inSight = false;
	public Vector3 lastSighting;
	public float angle = 0f;
    float angleFix = -18f;

	private Animator anim;
	private GameObject player;
	private Vector3 previousSighting;



	void Awake(){
		player = GameObject.FindGameObjectWithTag("Player");

		this.GetComponent<LineRenderer>().SetColors(Color.red,Color.red);
		this.GetComponent<LineRenderer>().SetVertexCount(4);
		this.GetComponent<LineRenderer>().SetWidth(0.1f,0.1f);

        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<SphereCollider>().isTrigger = true;
    }
	void OnTriggerStay(Collider other){
		if(other.gameObject == player){

			Vector3 direction = other.transform.position - transform.position;
			angle = Vector3.Angle(direction,transform.forward);
			Debug.DrawRay(new Vector3(transform.position.x, transform.position.y,transform.position.z),direction);

			if(angle > ((180 - fovAngle) / 2) - angleFix && angle < fovAngle - angleFix){
				RaycastHit hit;
				if(Physics.Raycast(transform.position, direction.normalized, out hit))
				{
					inSight = true;
					lastSighting = player.transform.position;
					Debug.Log ("In sight");
				}
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
			inSight = false;
		angle = 0f;
		Debug.Log ("Cleared");
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Quaternion left = Quaternion.AngleAxis (((180 - fovAngle) / 2) - angleFix, transform.up);
		Quaternion right = Quaternion.AngleAxis (fovAngle - angleFix, transform.up);

		this.GetComponent<LineRenderer>().SetPosition(0,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(1,(left * this.transform.forward * 8)+transform.position);
		this.GetComponent<LineRenderer>().SetPosition(2,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(3,(right * this.transform.forward * 8)+transform.position);
	}
}
