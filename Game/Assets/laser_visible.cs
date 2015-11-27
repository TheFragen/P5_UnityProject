using UnityEngine;
using System.Collections;

public class laser_visible : MonoBehaviour
{
    private GameObject Laser;
    private float gameTime;
    private bool on;
    // Use this for initialization
    void Start()
    {
        gameTime = 0;
        on = false;
        Laser = GameObject.Find("Lasers");
    }

    // Update is called once per frame
    void Update()
    {
        print(Time.time);
        if (Time.time - gameTime > 5)
        {
            print(Time.time - gameTime + "difference " );
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
