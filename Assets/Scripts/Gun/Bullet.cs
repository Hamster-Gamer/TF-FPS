using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float _bulletTimeLife = 1f;

    void OnCollisionEnter(Collision collision)
    {
        // Daño al Enemigo.
        if (collision.gameObject.CompareTag("Enemy"))
        {
            collision.gameObject.GetComponent<Enemy>()?.ReduceLife();
        }

        // Tiempo de Vida de Bala.
        Destroy(gameObject, _bulletTimeLife);
    }
}