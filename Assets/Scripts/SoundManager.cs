using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Function: SoundManager
// Name: Myungsub Eum
// Number: 101168160
// Last Uopdate: 20201207
// Description: sound can play, stop the music and SFX

// each sound set up
[System.Serializable]
public class Sound
{
    // sound name 
    public string name;
    // sound clip
    public AudioClip clip;
    // sound source
    private AudioSource source;

    // volumn
    public float Volumn;
    public bool loop;

    // audio source setup
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
        source.loop = loop;
        source.volume = Volumn;
    }

    // volumn
    public void SetVolumn()
    {
        source.volume = Volumn;
    }
    // play the sound
    public void Play()
    {
        source.Play();
    }
    // stop the sound
    public void Stop()
    {
        source.Stop();
    }
    // sound loop
    public void SetLoop()
    {
        source.loop = true;
    }
    // sound loop cancel
    public void SetLoopCancel()
    {
        source.loop = false;
    }
}


public class SoundManager : MonoBehaviour
{
    // singleton design pattern
    static public SoundManager instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            DontDestroyOnLoad(this.gameObject);
            instance = this;
        }
    }

    [SerializeField]
    public Sound[] sounds;

    void Start()
    {
        // sound init and make a sound array
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject soundObject = new GameObject("Sound name : " + i + " = " + sounds[i].name);
            sounds[i].SetSource(soundObject.AddComponent<AudioSource>());
            soundObject.transform.SetParent(this.transform);
        }
    }

    // play the sound depend on name
    public void Play(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Play();
                return;
            }
        }
    }
    // stop the sound depend on name
    public void Stop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Stop();
                return;
            }
        }
    }
    // loop the sound depend on name
    public void SetLoop(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoop();
                return;
            }
        }
    }
    // stop loop the sound depend on name
    public void SetLoopCancel(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].SetLoopCancel();
                return;
            }
        }
    }
    // volumn control
    public void SetVolumn(string _name, float _Volumn)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (_name == sounds[i].name)
            {
                sounds[i].Volumn = _Volumn;
                sounds[i].SetVolumn();
                return;
            }
        }
    }
}
