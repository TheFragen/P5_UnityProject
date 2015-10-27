using UnityEngine;
using System.Collections;

public class cameraClick : MonoBehaviour
{
    public bool debug = false;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (!debug)
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                GameObject.Find("Player").GetComponent<fingerClick>().setNewPosition(Input.GetTouch(0).position);
            }
        } else {
            if (Input.GetMouseButton(0))
            {
                GameObject.Find("Player").GetComponent<fingerClick>().setNewPosition(Input.mousePosition);
            }
        }
    }
}
