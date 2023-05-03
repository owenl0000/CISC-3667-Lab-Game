using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class End : MonoBehaviour
{
    public string sceneName;

    public void MenuButton()
    {
        SceneManager.LoadScene(sceneName);
        GameManager.Instance.currentSceneIndex = 0;
        PersistentData.Instance.SetScore(0);
    }
}
