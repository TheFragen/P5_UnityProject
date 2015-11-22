using UnityEngine;
using System.Collections;
using System;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(AudioSource))]

public class soundSystem : MonoBehaviour {
    SphereCollider soundWave;
    AudioSource audioSource;
    public bool createSound = false;
    private bool fireOnce = true;
    private bool soundHasPlayed = false;
    private long timeSincePlay;
    private long currentTime;
    private float loudnessScalar = 0.1f;
    private float sphereRadiusScalar;
    private bool reasonToPlay = false;
    public Vector3 positionToSend;

    [Tooltip("Defines how far away the enemy can be to be alerted by sound. Distance = loundness * 10.")]
    public float loudness = 1;

	// Use this for initialization
	void Awake () {
        soundWave = GetComponent<SphereCollider>();
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
        //soundWave.radius = 0.01f;
        sphereRadiusScalar = soundWave.radius;
        this.gameObject.tag = "SoundEmitter";
    }
	
	// Update is called once per frame
	void Update () {
        currentTime = DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond;

        if (createSound) {
            if(!soundHasPlayed) {
                if(audioSource.clip != null) audioSource.Play();
                soundHasPlayed = true;
                timeSincePlay = currentTime;
            }
            if(soundWave.radius < loudnessScalar * loudness) {
                soundWave.radius += sphereRadiusScalar;
            }
        } else {
            soundWave.radius = sphereRadiusScalar;
            fireOnce = true;
        }

        if(timeSincePlay + (1000 * loudness) < currentTime && !reasonToPlay) {
            if (audioSource.clip != null) audioSource.Stop();
            createSound = false;
            soundHasPlayed = false;
        }
    }

    void OnTriggerExit(Collider other) {
            
    }

    void OnTriggerStay(Collider other) {
        if (other.gameObject.tag == "Enemy") {
            Vector3 direction = other.transform.position - transform.position;

            RaycastHit hit;
            if (Physics.Raycast(transform.position, direction.normalized, out hit, soundWave.radius * 100 * 2)) {

                //      if(Vector3.Distance(this.transform.position, hit.transform.position) < loudnessScalar * loudness && createSound) {
                    Debug.DrawLine(this.transform.position, hit.point, Color.red);
                    if (fireOnce) {
                        fireOnce = false;
                        other.GetComponent<EnemyMovementNavAgent>().setSoundAlerted(positionToSend);
                    }
          //      }
            }
        }
    }

    public void setSound()
    {
        if(!createSound)
        {
            createSound = true;
            reasonToPlay = true;
        }
    }

    public void setReasonToPlay()
    {
        reasonToPlay = false;
        timeSincePlay = currentTime + 1000;
    }
}
