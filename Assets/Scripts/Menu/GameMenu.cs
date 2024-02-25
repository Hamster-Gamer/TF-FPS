using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMenu : MonoBehaviour
{
    [SerializeField] GameObject _pausePanel;
    bool _isGamePaused = false;
    
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            _isGamePaused = !_isGamePaused;
            PauseGame();
        }
    }

    void PauseGame()
    {
        if (_isGamePaused)
        {
            Time.timeScale = 0;
            _pausePanel.SetActive(true);
            Application.targetFrameRate = 60;
        }

        else
        {
            Time.timeScale = 1;
            _pausePanel.SetActive(false);
            Application.targetFrameRate = -1;
        }
    }
}