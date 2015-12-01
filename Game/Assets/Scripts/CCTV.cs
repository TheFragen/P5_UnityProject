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
    public float lineRenderAngleFix = -18f;
    public float baseVectorAngleFix = 0f;

    private Animator anim;
    private GameObject player;
	private Vector3 previousSighting;

    public Material foundMat;
    public Material normalMat;


    void Awake(){
		player = GameObject.Find("Player/Robart");

		this.GetComponent<LineRenderer>().SetColors(Color.red,Color.red);
		this.GetComponent<LineRenderer>().SetVertexCount(4);
		this.GetComponent<LineRenderer>().SetWidth(0.1f,0.1f);

        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<SphereCollider>().isTrigger = true;
    }
	void OnTriggerStay(Collider other){
		if(other.gameObject == player){
            inSight = false;
            
            //Let's fix this angle
            Vector3 direction = other.transform.position - new Vector3(transform.parent.position.x, 1.97f, transform.parent.position.z);
            Vector3 initialVector = new Vector3(this.transform.forward.x, 1.97f, this.transform.forward.z);
            Vector3 right = Vector3.Cross(initialVector, Vector3.up);
            Vector3 fixedVector = Quaternion.AngleAxis(baseVectorAngleFix, right) * initialVector;

            angle = Vector3.Angle(direction, fixedVector);
            
			if(angle < (fovAngle/2) - 5)
            {
                Debug.DrawRay(new Vector3(transform.parent.position.x, 1.97f, transform.parent.position.z), direction + Vector3.up, Color.blue);
                Debug.DrawRay(new Vector3(transform.parent.position.x, 1.97f, transform.parent.position.z), fixedVector, Color.green);

                //Makes sure there is nothing in the way of the camera (such as POTPLANT)
                RaycastHit hit;
				if(Physics.Raycast(new Vector3(transform.parent.position.x, 1.97f, transform.parent.position.z), direction + Vector3.up, out hit))
				{
                    if (hit.collider.gameObject == player)
                    {
                        inSight = true;
                        lastSighting = player.transform.position;
                        Debug.Log("In sight");
                        this.transform.parent.Find("soundSystem").GetComponent<soundSystem>().setSound();
                        this.GetComponent<LineRenderer>().material = foundMat;
                    }
                }
			}
		}
	}

	void OnTriggerExit (Collider other)
	{
		if(other.gameObject == player)
        {
            inSight = false;
            this.transform.parent.Find("soundSystem").GetComponent<soundSystem>().setReasonToPlay();
            this.GetComponent<LineRenderer>().material = normalMat;
        }
			
		angle = 0f;
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		Quaternion left = Quaternion.AngleAxis (((180 - fovAngle) / 2) - lineRenderAngleFix, transform.up);
		Quaternion right = Quaternion.AngleAxis (fovAngle - lineRenderAngleFix, transform.up);

		this.GetComponent<LineRenderer>().SetPosition(0,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(1,(left * this.transform.forward * 8)+transform.position);
		this.GetComponent<LineRenderer>().SetPosition(2,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(3,(right * this.transform.forward * 8)+transform.position);
	}
}
