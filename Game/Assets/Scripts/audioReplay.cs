using UnityEngine;
using System.Collections;

public class audioReplay : MonoBehaviour {
    public bool play;
    private bool isPlaying;
    public float replayWaitTime = 0f;
    AudioSource audio;

	// Use this for initialization
	void Start () {
        audio = GetComponent<AudioSource>();
        
	}
	
	// Update is called once per frame
	void Update () {
        if (play)
        {
            if (!isPlaying)
            {
                audio.Play();
                isPlaying = true;
            }
        } else {
            audio.Stop();
            StopCoroutine("replaySound");
        }

        if (!audio.isPlaying && play)
        {
            play = false;
            isPlaying = false;
            StartCoroutine(replaySound(replayWaitTime));
        }
	}

    IEnumerator replaySound(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        play = true;
    } 

    public void setPlay()
    {
        if (play == true)
        {
            play = false;
        } else
        {
            play = true;
        }
    }
}
