using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public string nameOfPlayer;
    
    void Start() {
    }

    public void PlayButton()
    {
        GameManager.Instance.canvas.SetActive(true);
        SceneManager.LoadScene("Level1");
        GameManager.Instance.currentSceneIndex = 1;
        GameManager.Instance.gameStart = true;
    }

    public void OptionsButton() 
    {
        SceneManager.LoadScene("Options");
    }

    public void nameSetter(string n) 
    {
        nameOfPlayer = n;
        PersistentData.Instance.SetName(nameOfPlayer);
    }

    public void TutorialButton()
    {
        SceneManager.LoadScene("Tutorial");
    }

    public void goBack()
    {
        SceneManager.LoadScene("Menu");
    }

    public void DifficultyScene()
    {
        SceneManager.LoadScene("Difficulty");
    }

}
