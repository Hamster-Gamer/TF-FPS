using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] int _enemyLife = 5;

    public void ReduceLife()
    {
        _enemyLife -= 1;

        if (_enemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }
}