using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SaveHighScores : MonoBehaviour
{
    public const int NUM_HIGH_SCORES = 5;
    public const string NAME_KEY = "HSName";
    public const string SCORE_KEY = "HScore";
    public const string MODE_KEY = "HSMode";

    [SerializeField] string playerName;
    [SerializeField] string playerMode;
    [SerializeField] int playerScore;

    [SerializeField] TextMeshProUGUI[] nameTexts;
    [SerializeField] TextMeshProUGUI[] scoreTexts;
    [SerializeField] TextMeshProUGUI[] modeTexts;

    // Start is called before the first frame update
    void Start()
    {
        playerName = PersistentData.Instance.GetName();
        playerScore = PersistentData.Instance.GetScore();
        playerMode = PersistentData.Instance.GetMode();

        SaveScore();
        DisplayHighScores();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SaveScore()
    {
        for (int i = 1; i <= NUM_HIGH_SCORES; i++)
        {
            string currentNameKey = NAME_KEY + i;
            string currentScoreKey = SCORE_KEY + i;
            string currentModeKey = MODE_KEY + i;

            if (PlayerPrefs.HasKey(currentScoreKey))
            {
                int currentScore = PlayerPrefs.GetInt(currentScoreKey);
                if (playerScore > currentScore)
                {
                    int tempScore = currentScore;
                    string tempName = PlayerPrefs.GetString(currentNameKey);
                    string tempMode = PlayerPrefs.GetString(currentModeKey);

                    PlayerPrefs.SetString(currentNameKey, playerName);
                    PlayerPrefs.SetInt(currentScoreKey, playerScore);
                    PlayerPrefs.SetString(currentModeKey, playerMode);

                    playerName = tempName;
                    playerScore = tempScore;
                    playerMode = tempMode;
                }
            }
            else

            {
                PlayerPrefs.SetString(currentNameKey, playerName);
                PlayerPrefs.SetInt(currentScoreKey, playerScore);
                PlayerPrefs.SetString(currentModeKey, playerMode);
                return;
            }              
        }
    }

    public void DisplayHighScores()
    {
        for (int i = 0; i < NUM_HIGH_SCORES; i++)
        {
            nameTexts[i].text = PlayerPrefs.GetString(NAME_KEY + (i+1));
            modeTexts[i].text = PlayerPrefs.GetString(MODE_KEY + (i+1));
            if(PlayerPrefs.GetInt(SCORE_KEY + (i+1)) != 0) {
                scoreTexts[i].text = PlayerPrefs.GetInt(SCORE_KEY + (i+1)).ToString();
            }
        }
    }
}