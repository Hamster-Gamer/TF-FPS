using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeBar : MonoBehaviour
{
    [SerializeField] Image _lifeBar;
    [SerializeField] float _currentLife;
    [SerializeField] float _maxLife = 10;

    void Start()
    {
        _currentLife = _maxLife;
    }

    public void ReduceLife()
    {
        _currentLife--;
        UpdateLifeBar();
    }

    public void IncreaseLife()
    {
        _currentLife++;
        UpdateLifeBar();
    }

    public void UpdateLifeBar()
    {
        float fillAllmount = _currentLife / _maxLife;
        _lifeBar.fillAmount = fillAllmount;
    }
}