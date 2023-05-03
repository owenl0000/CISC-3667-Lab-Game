using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Scoreboard : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] nameTexts;
    [SerializeField] private TextMeshProUGUI[] scoreTexts;
    private List<PersistentData.HighScore> highScores;

    private void Start()
    {
        // Get the high scores from PersistentData and display them
        List<PersistentData.HighScore> highScores = PersistentData.Instance.highScores;

        for (int i = 0; i < highScores.Count; i++)
        {
            nameTexts[i].text = highScores[i].name;
            scoreTexts[i].text = highScores[i].score.ToString();
        }
    }

        private class HighScore
    {
        public string name;
        public int score;

        public HighScore(string n, int s)
        {
            name = n;
            score = s;
        }
    }
}
