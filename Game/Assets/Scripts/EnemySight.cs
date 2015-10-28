using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class EnemySight : MonoBehaviour {

    public float fovAngle = 110f;
    public bool playerInSight;
	public Vector3 personalLastSighting = new Vector3(1000.0f, 1000.0f, 1000.0f);

	public Vector3 resetSight = new Vector3(1000.0f, 1000.0f, 1000.0f);
    private SphereCollider col;
    private GameObject player;
    private Vector3 previousSighting;

	private GameObject canvas;
	
	private GameObject restartButton;
	private GameObject text;
	private GameObject window;

	private bool restart = false;
	private bool caught = false;

	// Use this for initialization
	void Start () {
	
	}

    void Awake()
    {
        col = GetComponent<SphereCollider>();
        player = GameObject.FindWithTag("Player");
		canvas = GameObject.Find ("Canvas");

    }
	
	// Update is called once per frame
	void Update () {

		if (restart) 
		{
			Debug.Log ("We must go back");
			Application.LoadLevel (Application.loadedLevel);
			restart = false;
		}

	}
	
	void OnTriggerStay (Collider other)
    {
        
        if (other.gameObject == player)
        {
            playerInSight = false;
            Vector3 direction = other.transform.position - transform.position;
            float angle = Vector3.Angle(direction, transform.forward);

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, col.radius * 2))
            {
                if (angle < fovAngle * 0.5f)
                {
                    Debug.DrawLine(transform.position, hit.point, Color.red);
                    if (hit.collider.gameObject == player)
                    {
                        if (Physics.Linecast(this.transform.position, hit.point, out hit))
                        {

                        }

                        playerInSight = true;   
                        personalLastSighting = player.transform.position;
					
						if (hit.distance < 1.0f)
						{
							playerCaught();
						}
                    }

                //Check if player is really close to enemy, and mark that as sight
                } else if (hit.distance < 1.5f && hit.collider.gameObject == player) 
				{
                    playerInSight = true;
                    personalLastSighting = player.transform.position;
                }

            } 
        }
    }

	void playerCaught ()
	{
		if (caught == false) {
			caught = true;
			//Debug.Log ("Så fik jeg dig");

			window = Instantiate (Resources.Load ("WindowPanel")) as GameObject;
			window.transform.SetParent (canvas.transform, false);
		
			text = Instantiate (Resources.Load ("VictoryText")) as GameObject;
			text.transform.SetParent (canvas.transform, false);
			text.GetComponent<Text>().text = "You got caught";
		
			restartButton = Instantiate (Resources.Load ("RestartButton")) as GameObject;
			restartButton.transform.SetParent (canvas.transform, false);
			restartButton.GetComponent<Button> ().onClick.AddListener (() => {
			setRestart (true);});
		}


	}

	public void setRestart (bool restart)
	{
		this.restart = restart;
	}

    void OnTriggerExit(Collider other)
    {
        if (other.gameObject == player)
            playerInSight = false;
    }

}
