using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public int score = 0;
    public float sizeIncreaseInterval = 1f;
    public float gameTime = 120f;
    public float timer = 0f;
    private int currentSceneIndex;
    [SerializeField] TextMeshProUGUI time;
    public GameObject canvas;

    public static GameManager Instance;
    // Start is called before the first frame update
    private void Awake()
    {
        // Check if this is the first instance of the game object
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            // Destroy this instance if there are multiple instances
            Destroy(canvas);
            Destroy(gameObject);
        }
        else
        {
            // Don't destroy this instance when a new scene is loaded
            DontDestroyOnLoad(canvas);
            DontDestroyOnLoad(gameObject);
        }
    }
    void Start()
    {
        currentSceneIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // Update the timer
        timer += Time.deltaTime;
        DisplayTimer();

        // Check if the game time has elapsed
        if (timer >= gameTime)
        {
            // Load the next scene
            if(currentSceneIndex == 0) {
                SceneManager.LoadScene(1);
                currentSceneIndex++;
            }
            else if(currentSceneIndex == 1) {
                SceneManager.LoadScene(2);
            }
            timer = 0f;
        }
    }

    public void DisplayTimer() {
        float timeLeft = gameTime - timer;
        time.text = "Time left: " + timeLeft;
    }


}
