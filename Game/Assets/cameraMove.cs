using UnityEngine;
using System.Collections;
using System;

public class cameraMove : MonoBehaviour
{
    public float speed = 0f;
    public Transform referenceCube;

    public enum RotationAxes { MouseXAndY = 0, MouseX = 1, MouseY = 2 }
    public RotationAxes axes = RotationAxes.MouseXAndY;
    public float sensitivityX = 15F;
    public float sensitivityY = 15F;

    public float minimumX = -360F;
    public float maximumX = 360F;

    public float minimumY = -60F;
    public float maximumY = 60F;

    float rotationY = 0F;

    long lastDebounceTime = 0;
    long debounceDelay = 150;

    public float fixedRotation = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 _tmp = referenceCube.localEulerAngles;
        _tmp.y = this.transform.localEulerAngles.y;
        referenceCube.localEulerAngles = _tmp;

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            transform.position += transform.right * Time.deltaTime * speed;
            //     transform.Translate(new Vector3(speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            transform.position -= transform.right * Time.deltaTime * speed;
            //     transform.Translate(new Vector3(-speed * Time.deltaTime, 0, 0));
        }
        if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
        {
            transform.Translate(-referenceCube.transform.forward * Time.deltaTime * speed, Space.World);
            //     transform.Translate(new Vector3(0, -speed * Time.deltaTime, 0));
        }
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W))
        {
            transform.position += referenceCube.transform.forward * Time.deltaTime * speed;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            if (((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) - lastDebounceTime) > debounceDelay)
            {
                Debug.Log("Nigger");
                lastDebounceTime = (DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond);
                GameObject.Find("Player").GetComponent<PointAndClick>().setClickButton(true);
            }
        }


        if (Input.GetMouseButton(0))
        {
            if (axes == RotationAxes.MouseXAndY)
            {
                float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
            }
            else
            {
                rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
                rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
                transform.localEulerAngles = new Vector3(-rotationY, transform.localEulerAngles.y, 0);
            }
        }
    }
}
