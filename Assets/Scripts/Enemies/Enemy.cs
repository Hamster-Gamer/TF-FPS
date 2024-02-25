using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int _enemyLife = 5;

    [Header("Drops")]
    [SerializeField] bool _generateObject = false;
    [Range(0f, 100f)]
    [SerializeField] float _dropProbability = 25f;
    [SerializeField] float _delayBeforeDrop = 2.5f;

    [Header("Items")]
    [SerializeField] GameObject _itemLife;

    public void ReduceLife()
    {
        _enemyLife -= 1;

        if (_enemyLife <= 0)
        {
            gameObject.SetActive(false);
            Invoke("RandomDrops", _delayBeforeDrop);
        }
    }

    void RandomDrops()
    {
        if (_generateObject == true && Random.value * 100 < _dropProbability)
        {
            Instantiate(_itemLife, transform.position, Quaternion.identity);
        }

        Destroy(gameObject);
    }
}