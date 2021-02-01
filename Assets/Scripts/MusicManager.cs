using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    //Attributes
    [SerializeField]
    private List<AudioClip> _tracks;

    //Values
    [SerializeField]
    private bool _play = true;

    //Singleton for bug issue
    private static MusicManager instance = null;

    //Function
    private void Awake()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);

        }

    }

    private void Start()
    {
        // if the singleton hasn't been initialized yet
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }

        instance = this;

        CoutdownManager.StartMusic += PlayRandomMusic;
        GameManager.StopMusic += StopMusic;
    }

    private void PlayRandomMusic()
    {
        int rand = Random.Range(0, _tracks.Count);
        AudioClip music = _tracks[rand];
        GetComponent<AudioSource>().clip = music;
        GetComponent<AudioSource>().Play();
    }

    private void PlayPauseMusic()
    {
        if(_play)
        {
            GetComponent<AudioSource>().Pause();
            _play = false;
        }

        else
        {
            GetComponent<AudioSource>().Play();
            _play = true;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.M))
        { 
            PlayPauseMusic();
        }
    }

    private void StopMusic()
    {
        GetComponent<AudioSource>().Stop();

}
}
