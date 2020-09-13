using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume = 0.5f;

    [Range(0.5f, 1.5f)]
    public float pitch = 1f;

    private AudioSource source;
    public void SetSource(AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play()
    {
        source.pitch = pitch;
        source.volume = volume;
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;

    [SerializeField]
    public Sound[] sounds;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        } else {
            
            instance = this;
        }

        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
        for (int i = 0; i < sounds.Length; i++)
        {
            GameObject go = new GameObject("Sound_" + i + "_" + sounds[i].name);
            sounds[i].SetSource(go.AddComponent<AudioSource>());
        }

        PlaySound("shop");
    }

    public void PlaySound(string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name) {
                sounds[i].Play();
                return;
            }
        }

        Debug.LogWarning("No sound with that name");
    }

    public void PlayBattle()
    {

    }

    public void PlayMainTheme()
    {

    }
}
