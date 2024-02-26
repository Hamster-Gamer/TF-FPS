using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _shootingTimeLife = 1f;
    [SerializeField] float _impactTimeLife = 0.25f;

    void Start()
    {
        // Tiempo de Vida de Bala.
        Destroy(gameObject, _shootingTimeLife);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Daño al Enemigo.
        if (collider.gameObject.CompareTag("Enemy"))
        {
            collider.gameObject.GetComponent<Enemy>()?.ReduceLife();
            Destroy(gameObject, _impactTimeLife);
        }
    }
}