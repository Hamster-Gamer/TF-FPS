using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    [SerializeField] float _shootingTimeLife = 1f;
    [SerializeField] float _impactTimeLife = 0.25f;

    [SerializeField] GameObject _damagePanel;

    void Start()
    {
        // Tiempo de Vida de Bala.
        Destroy(gameObject, _shootingTimeLife);
    }

    void OnTriggerEnter(Collider collider)
    {
        // Daño al Jugador.
        if (collider.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().LoseLife();

            StartCoroutine(ShowDamageEffect());
        }

        if (collider.CompareTag("Plataform"))
        {
            Destroy(gameObject, 0.1f);
        }
    }

    IEnumerator ShowDamageEffect()
    {
        _damagePanel.SetActive(true);
        yield return new WaitForSeconds(_impactTimeLife);
        _damagePanel.SetActive(false);

        Destroy(gameObject);
    }
}