using UnityEngine;
using System.Collections;

public class AMSounds : MonoBehaviour {

    public static AMSounds Static;
    void Awake()
    {
        Static = this;
    }

    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Play(string SoundName)
    {
        foreach(AudioSource au in gameObject.GetComponentsInChildren<AudioSource>())
        {
            if(au.gameObject.name == SoundName)
            {
                StopAllAudio();
                au.Play();
            }
        }
    }

    //Stop All Audio
    public void StopAllAudio()
    {   
        AudioSource[] listAudio = gameObject.GetComponentsInChildren<AudioSource>();
        foreach (AudioSource au in listAudio)
        {
            if (au.isPlaying)
            {
                au.Stop();
            }
        }
    }
}
