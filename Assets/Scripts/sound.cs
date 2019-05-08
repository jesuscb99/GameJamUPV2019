using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    public AudioClip MusicClipStart;
    public AudioClip MusicClipGoing;

    public AudioSource MusicSource;
    
    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = MusicClipStart;
        MusicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(!MusicSource.isPlaying)
        {
            MusicSource.clip = MusicClipGoing;
            MusicSource.Play();
            MusicSource.loop = true;
        }
    }
}
