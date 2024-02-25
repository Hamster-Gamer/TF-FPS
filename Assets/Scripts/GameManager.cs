using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] int _playerLife = 5;

    public void LoseLife()
    {
        _playerLife--;

        if (_playerLife <= 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }

    public void WinLife()
    {
        _playerLife++;
    }
}