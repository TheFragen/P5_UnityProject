using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class timer : MonoBehaviour {

	public static float playedTime;

	Text text;

	// Use this for initialization
	void Start () 
	{

		text = GetComponent<Text> ();

		playedTime = 0;
	
	}
	
	// Update is called once per frame
	void FixedUpdate () 
	{
		playedTime += Time.deltaTime;

		text.text = "Time: " + Mathf.RoundToInt (playedTime);
	
	}
}
