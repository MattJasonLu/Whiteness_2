using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public bool isPaused = false;

    private void Start()
    {
        _instance = this;
    }

    public void OnPause()
    {
        isPaused = true;
        Time.timeScale = 0;
    }

    public void OnResume()
    {
        isPaused = false;
        Time.timeScale = 1;
    }

    public void ReloadScene()
    {
        PlayerPrefs.DeleteAll();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
