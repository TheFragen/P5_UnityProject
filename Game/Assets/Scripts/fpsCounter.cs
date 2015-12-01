using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class fpsCounter : MonoBehaviour {

    public float updateInterval = 0.5F;

    private float accum = 0; // FPS accumulated over the interval
    private int frames = 0; // Frames drawn over the interval
    private float timeleft; // Left time for current interval

    void Start()
    {
        timeleft = updateInterval;
    }

    void Update()
    {
        timeleft -= Time.deltaTime;
        accum += Time.timeScale / Time.deltaTime;
        ++frames;
        
        if (timeleft <= 0.0)
        {
            float fps = accum / frames;
            string format = System.String.Format("{0:F2} FPS", fps);
            this.GetComponent<Text>().text = format;

            if (fps >= 1 && fps <= 10)
            {
                this.GetComponent<Text>().color = Color.red;
            }
            else if (fps >= 11 && fps <= 23)
            {
                this.GetComponent<Text>().color = Color.yellow;
            }
            else
            {
                this.GetComponent<Text>().color = Color.green;
            }

            timeleft = updateInterval;
            accum = 0.0F;
            frames = 0;
        }
    }
}