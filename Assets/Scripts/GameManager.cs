using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _playerLife = 5;
    [SerializeField] GameObject _deadPanel;
    [SerializeField] LifeBar _lifeBar;

    public void LoseLife()
    {
        _playerLife--;
        _lifeBar.ReduceLife();

        if (_playerLife <= 0)
        {
            Time.timeScale = 0;
            SceneManager.LoadScene(4);
            /*
            _deadPanel.SetActive(true);
            Time.timeScale = 0;
            Application.targetFrameRate = 60;*/
        }
    }

    public void WinLife()
    {
        _playerLife++;

        _lifeBar.IncreaseLife();
    }
}