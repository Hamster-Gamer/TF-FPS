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

    void OnCollisionEnter(Collision collision)
    {
        // Daño al Enemigo.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>()?.ReduceLife();
            Destroy(gameObject, _impactTimeLife);
        }
    }
}