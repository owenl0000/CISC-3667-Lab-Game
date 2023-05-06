using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Difficulty : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.difficultyLevel = 0;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void normalMode()
    {
        GameManager.Instance.difficultyLevel = 1;
    }
    
    public void hardMode()
    {
        GameManager.Instance.difficultyLevel = 2;
    }

    public void expertMode()
    {
        GameManager.Instance.difficultyLevel = 3;
    }
}
