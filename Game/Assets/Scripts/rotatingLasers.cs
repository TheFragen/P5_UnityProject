using UnityEngine;
using System.Collections;

public class rotatingLasers : MonoBehaviour {
    public float speed = 5f;
    public enum dir {Clockwise, CounterClockwise};
    public dir Direction;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 _tmp = this.transform.eulerAngles;
        if(Direction == dir.Clockwise)
        {
            _tmp.y += speed;
        } else if (Direction == dir.CounterClockwise){
            _tmp.y -= speed;
        }

        this.transform.eulerAngles = _tmp;
    }
}
