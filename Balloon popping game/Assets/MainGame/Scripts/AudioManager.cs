using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    public AudioClip[] audioClips;
    private AudioSource audioSource;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayClip(string clipName)
    {
        AudioClip clipToPlay = null;

        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == clipName)
            {
                clipToPlay = clip;
                break;
            }
        }

        if (clipToPlay != null)
        {
            audioSource.PlayOneShot(clipToPlay);
        }
        else
        {
            Debug.LogWarning("Clip not found: " + clipName);
        }
    }
}
