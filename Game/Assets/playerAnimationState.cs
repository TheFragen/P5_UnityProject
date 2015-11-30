using UnityEngine;
using System.Collections;

public class playerAnimationState : MonoBehaviour {
    public bool isMoving;
    public float threshold;
    NavMeshAgent agent;
    Rigidbody rb;
    Animator anim;


	// Use this for initialization
	void Start () {
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponent<Rigidbody>();
        anim = this.transform.GetChild(0).GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {

        if(agent.hasPath)
        {
            isMoving = true;
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Speed", agent.desiredVelocity.magnitude);
        } else if(Vector3.Distance(rb.velocity, Vector3.zero) > threshold)
        {
            isMoving = true;
            anim.SetBool("IsWalking", true);
            anim.SetFloat("Speed", Vector3.Magnitude(rb.velocity - Vector3.zero));
        }
        else
        {
            isMoving = false;
            anim.SetBool("IsWalking", false);
        }

	}
}
