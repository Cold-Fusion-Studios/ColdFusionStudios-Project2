using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_Player : MonoBehaviour
{   //Make this a list?
    public AudioClip Song1;
    public AudioClip Song2;
    public AudioClip Song3;
    public AudioSource MusicSource;
    // Start is called before the first frame update
    void Start()
    {
        MusicSource.clip = Song1;
        MusicSource.Play();
    }
    
    public void changeSong(int num)
    { if (num == 2)
        {
            MusicSource.Stop();
            MusicSource.clip = Song2;
        }
    if (num == 3)
        {
            MusicSource.Stop();
            MusicSource.clip = Song3;
        }
            MusicSource.Play();


    }
}
