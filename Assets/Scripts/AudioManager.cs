using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds)
        {
            s.audioSource = gameObject.AddComponent<AudioSource>();
            s.audioSource.clip = s.clip;
            s.audioSource.loop = s.loop;
            s.audioSource.volume = s.volume;
            s.audioSource.pitch = s.pitch;
        }
    }

    private void Start()
    {
        Play("Ambient");
        Play("Car");
        if(GameData.GetCarData(GameData.SelectedCarId).Id == 3)
        {
            Play("Police");
        }
      
    }
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null) {
            Debug.LogWarning(name + " not found");
            return;
        }
        s.audioSource.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, s => s.name == name);
        if (s == null)
        {
            Debug.LogWarning(name + " not found");
            return;
        }
        s.audioSource.Stop();
    }
}
