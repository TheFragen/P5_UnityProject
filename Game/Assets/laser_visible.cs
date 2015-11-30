using UnityEngine;
using System.Collections;

public class laser_visible : MonoBehaviour
{
    private Transform player;
    private Transform[] laserChild;
    private GameObject Laser;
    private float gameTime;
    private bool on;
    // Use this for initialization
    void Start()
    {
        player = GameObject.Find("Player/Robart").transform;
        gameTime = 0;
        on = false;
        Laser = GameObject.Find("Lasers");
        laserChild = this.GetComponentsInChildren<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
            if (Time.time - gameTime > 5)
        {
            if (on == false)
            {
                gameTime = Time.time;
                foreach (Transform child in Laser.transform)
                {
                    child.gameObject.SetActive(false);
                }
                on = true;
            }
            else
            {
                gameTime = Time.time;
                foreach (Transform child in Laser.transform)
                {
                    child.gameObject.SetActive(true);
                }
                on = false;
            }
        }
    }
}
