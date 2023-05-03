using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Options : MonoBehaviour
{
    [SerializeField] string sceneName;
    public Slider slider;
    public TextMeshProUGUI percentage;
    public AudioMixer audioMixer;
    private float newVolume;
    // Start is called before the first frame update
    void Start()
    {
        if(!PlayerPrefs.HasKey("volume"))
        {
            PlayerPrefs.SetFloat("volume", 0);
            Load();
        }
        else 
        {
            Load();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void goBack() 
    {
        SceneManager.LoadScene(sceneName);
    }

    public void SetVolume(float volume) 
    {
        audioMixer.SetFloat("volume", volume);
        float newVolume = (volume + 80f) / 80f * 100f;
        percentage.text = newVolume.ToString("00") + "%";
        Save();
    }


    public void SetQuality(int qualityIndex) 
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    private void Load()
    {
        float volume = PlayerPrefs.GetFloat("volume");
        slider.value = volume;
        bool result = audioMixer.GetFloat("volume", out volume);
        // Calculate the volume percentage and update the UI
        if (result)
        {
            newVolume = (volume + 80f) / 80f * 100f;
            if (newVolume == 0f)
            {
                newVolume = 100f;
            }
            percentage.text = newVolume.ToString("00") + "%";
        }
    }
    private void Save()
    {
        PlayerPrefs.SetFloat("volume", slider.value);
    }

}
