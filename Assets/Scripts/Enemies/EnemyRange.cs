using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRange : MonoBehaviour
{
    [SerializeField] Animator _animatorEnemy;
    [SerializeField] Enemy _scriptEnemy;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            // Activar y Desactivar Animaciones.
            _animatorEnemy.SetBool("Walk", false);
            _animatorEnemy.SetBool("Run", false);
            _animatorEnemy.SetBool("NewAttack", true);

            _scriptEnemy._isAttack = true;
            // Desactiva Collider Enemigo Evitando que se Active Repetidamente.
            GetComponent<BoxCollider>().enabled = false;
        }
    }
}