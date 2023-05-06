using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistentData : MonoBehaviour
{
    [SerializeField] private int playerScore;
    [SerializeField] private string playerName;
    [SerializeField] private string playerMode;
    [SerializeField] public List<HighScore> highScores;

    public static PersistentData Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        playerScore = 0;
        playerName = "";
        playerMode = "";
        highScores = new List<HighScore>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetMode(string s) 
    {
        playerMode = s;
    }
    public void SetName(string s)
    {
        playerName = s;
    }

    public void SetScore(int s)
    {
        playerScore = s;
    }

    public string GetMode()
    {
        return playerMode;
    }
    public string GetName()
    {
        return playerName;
    }

    public int GetScore()
    {
        return playerScore;
    }

    public void AddHighScore(string name, int score)
    {
        highScores.Add(new HighScore(name, score));
        highScores.Sort();
        if (highScores.Count > 5)
        {
            highScores.RemoveAt(5);
        }
    }

    public List<HighScore> GetHighScores()
    {
        return highScores;
    }

    public struct HighScore : System.IComparable<HighScore>
    {
        public string name;
        public int score;

        public HighScore(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public int CompareTo(HighScore other)
        {
            return other.score - this.score;
        }
    }
}
