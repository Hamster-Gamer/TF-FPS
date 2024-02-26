using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;

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

    [Header("Conduct General")]
    [SerializeField] int _routine;
    [SerializeField] float _chronometer;
    [SerializeField] Animator _animator;
    [SerializeField] Quaternion _angle;
    [SerializeField] float _angleDegress;

    [Header("Conduct Attack")]
    [SerializeField] GameObject _targetPlayer;
    [SerializeField] bool _isAttack;

    void Update()
    {
        Conduct();
    }

    void Conduct()
    {
        // Jugador Fuera del Rango de Distancia.
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > 7.5f)
        {
            // Animación de Caminar si Estaba Corriendo.
            if (_animator.GetBool("Run"))
            {
                _animator.SetBool("Run", false);
                _animator.SetBool("Walk", true);
            }

            // Comportamiento Aleatorio.
            _chronometer += Time.deltaTime;
            if (_chronometer >= 4)
            {
                _routine = Random.Range(0, 2);
                _chronometer = 0;
            }

            // Comportamiento a la Rutina Actual.
            if (_routine == 0)
            {
                // Detiene la Animación de Caminar.
                _animator.SetBool("Walk", false);
            }

            else if (_routine == 1)
            {
                // Ángulo Aleatorio para Girar.
                _angleDegress = Random.Range(0, 360);
                _angle = Quaternion.Euler(0, _angleDegress, 0);
                _routine++;
            }

            else if (_routine == 2)
            {
                // Gira el Ángulo y Avanza.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, _angle, 0.5f);
                transform.Translate(Vector3.forward * 1 * Time.deltaTime);
                _animator.SetBool("Walk", true);
            }
        }

        // Jugador Dentro del Rango de Distancia.
        else
        {
            if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > 5 && !_isAttack)
            {
                // Cambiar a Animación de Correr.
                _animator.SetBool("Walk", false);
                _animator.SetBool("Run", true);
                transform.Translate(Vector3.forward * 2 * Time.deltaTime);
                _animator.SetBool("NewAttack", false);
                
                // Lógica de Seguimiento al Jugador.
                var lookPosition = _targetPlayer.transform.position - transform.position;
                lookPosition.y = 0;
                var rotation = Quaternion.LookRotation(lookPosition);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, 2);
            }

            else
            {
                // Ataca al Jugador.
                _animator.SetBool("Walk", false);
                _animator.SetBool("Run", false);
                _animator.SetBool("NewAttack", true);
                _isAttack = true;
            }
        }
    }

    public void FinishAttack()
    {
        // Reinicio de Estado de Ataque.
        _animator.SetBool("NewAttack", false);
        _isAttack = false;
    }

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