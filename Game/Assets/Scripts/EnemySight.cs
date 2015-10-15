using UnityEngine;
using System.Collections;

public class EnemySight : MonoBehaviour {

    public float fovAngle = 110f;
    public bool playerInSight;
	public Vector3 personalLastSighting = new Vector3(1000.0f, 1000.0f, 1000.0f);

	public Vector3 resestSight = new Vector3(1000.0f, 1000.0f, 1000.0f);
    private SphereCollider col;
    private GameObject player;
    private Vector3 previousSighting;

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag("Player");

    }
	
	// Update is called once per frame
	void Update () {

		// Draw field of view with line renderer
		/*Quaternion left = Quaternion.AngleAxis ((fovAngle-180) / 2, transform.up);
		Quaternion right = Quaternion.AngleAxis (180-fovAngle, transform.up);
		
		this.GetComponent<LineRenderer>().SetPosition(0,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(1,(left * this.transform.forward * 4)+transform.position);
		this.GetComponent<LineRenderer>().SetPosition(2,transform.position);
		this.GetComponent<LineRenderer>().SetPosition(3,(right * this.transform.forward * 4)+transform.position);
		*/
	}
	
	void OnTriggerStay (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInSight = false;

            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            if (angle < fovAngle * 0.5f)
            {
                RaycastHit hit;
                
                if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius*2))
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    if (hit.collider.gameObject == player)
                    {
                        playerInSight = true;
                    
                        personalLastSighting = player.transform.position;

                        Debug.Log("player sighted");
                    }
                }
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

}
