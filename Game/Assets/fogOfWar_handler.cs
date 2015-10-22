using UnityEngine;
using System.Collections;

public class fogOfWar_handler : MonoBehaviour {
    public int number = 1;
    public Transform fogOfWarPlane;


	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position);
        Ray rayToPlayerPos = Camera.main.ScreenPointToRay(screenPos);
        int layermask = (int)(1 << 8);
        RaycastHit hit;
        if (Physics.Raycast(rayToPlayerPos, out hit, 1000, layermask)) {
            fogOfWarPlane.GetComponent<Renderer>().material.SetVector("_Player" + number.ToString() + "_Pos", hit.point);
        }
    }
}
