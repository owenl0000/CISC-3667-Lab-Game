using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public float sizeIncreaseInterval = 1f;
    public float gameTime = 120f;
    public float timer = 0f;
    public int currentSceneIndex;
    [SerializeField] TextMeshProUGUI time;
    public GameObject canvas;
    public bool gameStart = false;

    public static GameManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        // Check if this is the first instance of the game object
        if (FindObjectsOfType(GetType()).Length > 1 && Instance != null)
        {
            // Destroy this instance if there are multiple instances
            Destroy(canvas);
            Destroy(gameObject);
        }
        else
        {
            // Don't destroy this instance when a new scene is loaded
            Instance = this;
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        HideCanvas();
        currentSceneIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if(gameStart != false) {
            // Update the timer
            timer += Time.deltaTime;
            DisplayTimer();

            // Check if the game time has elapsed
            if (timer >= gameTime)
            {
                timer = 0f;
                currentSceneIndex++;
                // Load the next scene
                if(currentSceneIndex == 1) {
                    SceneManager.LoadScene(2);
                }
                else if(currentSceneIndex == 2) {
                    SceneManager.LoadScene(3);
                }
                else {
                    EndGame();
                    gameStart = false;
                }
                
            }
        }
        else {
            HideCanvas();
        }
    }

    public void DisplayTimer() {
        float timeLeft = Mathf.Max(gameTime - timer, 0); // set timeLeft to 0 if it goes below 0
        int minutes = Mathf.FloorToInt(timeLeft / 60f);
        int seconds = Mathf.FloorToInt(timeLeft % 60f);
        float milliseconds = (timeLeft - Mathf.Floor(timeLeft)) * 100f;
        string timeString = string.Format("{0:00}:{1:00}.{2:00}", minutes, seconds, milliseconds);
        time.text = "Time left: " + timeString;
    }

    public void EndGame()
    {
        HideCanvas();
        PersistentData.Instance.AddHighScore(PersistentData.Instance.GetName(), PersistentData.Instance.GetScore());
        timer = 0f;
        SceneManager.LoadScene("End");
    }

    public void HideCanvas() {
        currentSceneIndex = 0;
        canvas.SetActive(false);
        timer = 0f;
        gameStart = false;
    }

}
