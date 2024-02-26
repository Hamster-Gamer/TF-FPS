using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _playerLife = 5;
    [SerializeField] GameObject _deadPanel;

    public void LoseLife()
    {
        _playerLife--;

        if (_playerLife <= 0)
        {
            _deadPanel.SetActive(true);
            Time.timeScale = 0;
            Application.targetFrameRate = 60;
        }
    }

    public void WinLife()
    {
        _playerLife++;
    }
}