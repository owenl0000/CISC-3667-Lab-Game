using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Score : MonoBehaviour
{
    [SerializeField] int score;
    const int DEFAULT_POINTS = 1;
    [SerializeField] TextMeshProUGUI scoreTxt;
    [SerializeField] TextMeshProUGUI levelTxt;
    [SerializeField] TextMeshProUGUI nameTxt;
    [SerializeField] TextMeshProUGUI modeTxt;
    [SerializeField] int level;
    [SerializeField] int mode;

    // Start is called before the first frame update
    void Start()
    {
        score = PersistentData.Instance.GetScore();
        level = SceneManager.GetActiveScene().buildIndex;
        mode = GameManager.Instance.difficultyLevel;

        //display score
        DisplayScore();
        DisplayLevel();
        DisplayName();
        DisplayMode();
    }

    // Update is called once per frame
    void Update()
    {
        score = PersistentData.Instance.GetScore();
        name = PersistentData.Instance.GetName();
        level = SceneManager.GetActiveScene().buildIndex;
        mode = GameManager.Instance.difficultyLevel;
        DisplayName();
        DisplayLevel();
        DisplayScore();
        DisplayMode();
    }

    public void UpdateScore(int addend)
    {
        score += addend;
        //display score
        DisplayScore();
        PersistentData.Instance.SetScore(score);

    }

    public void UpdateScore()
    {
        UpdateScore(DEFAULT_POINTS);
    }

    public void DisplayScore()
    {
        scoreTxt.text = "Score: " + score;
    }

    public void DisplayLevel()
    {
        int levelToDisplay = level;
        levelTxt.text = "Level " + levelToDisplay;
    }

    public void DisplayName()
    {
        nameTxt.text = "Hi, " + PersistentData.Instance.GetName();
    }

    public void DisplayMode()
    {
        if(mode == 1) {
            modeTxt.text = "Normal Mode";
            PersistentData.Instance.SetMode("Normal");
        }
        else if(mode == 2) {
            modeTxt.text = "Hard Mode";
            PersistentData.Instance.SetMode("Hard");
        }
        else if(mode == 3) {
            modeTxt.text = "Expert Mode";
            PersistentData.Instance.SetMode("Expert");
        }
    }

}
