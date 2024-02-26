using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.LookDev;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [Header("Stats")]
    [SerializeField] int _life = 5;
    [SerializeField] float _speedWalk = 1f;
    [SerializeField] float _speedRotation = 2f;
    [SerializeField] float _speedRun = 2f;    

    [Header("AI Navigation")]
    [SerializeField] NavMeshAgent _agent;
    [SerializeField] float _distanceAttack = 7.5f;
    [SerializeField] float _visionRadius = 10f;

    [Header("Fire")]
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletPrefab;
    [SerializeField] float _shotForce = 50f;

    [Header("Drops")]
    [SerializeField] bool _generateObject = false;
    [Range(0f, 100f)]
    [SerializeField] float _dropProbability = 25f;
    [SerializeField] float _delayBeforeDrop = 2.5f;

    [Header("Items")]
    [SerializeField] GameObject _itemLife;

    [Header("Conduct General")]
    [SerializeField] int _routine;
    float _chronometer;
    [SerializeField] Animator _animator;
    Quaternion _angle;
    float _angleDegress;

    [Header("Conduct Attack")]
    [SerializeField] GameObject _targetPlayer;
    [SerializeField] EnemyRange _colliderRangeAttack;
    [HideInInspector] public bool _isAttack;

    void Update()
    {
        Conduct();
    }

    void Conduct()
    {
        // Jugador Fuera de Rango.
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > _visionRadius)
        {
            // Animación de Caminar si Estaba Corriendo.
            if (_animator.GetBool("Run"))
            {
                _agent.enabled = false;

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

            if (_routine == 0)
            {
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
                transform.Translate(Vector3.forward * _speedWalk * Time.deltaTime);
                _animator.SetBool("Walk", true);
            }
        }

        else
        {
            // Calcula la Dirección para Mirar al Jugador.
            Vector3 lookPosition = _targetPlayer.transform.position - transform.position;
            lookPosition.y = 0;
            Quaternion rotation = Quaternion.LookRotation(lookPosition);

            _agent.enabled = true;
            _agent.SetDestination(_targetPlayer.transform.position);

            if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > _distanceAttack && !_isAttack)
            {
                // Cambiar a Animación de Correr.
                _animator.SetBool("Walk", false);
                _animator.SetBool("Run", true);
                transform.Translate(Vector3.forward * _speedRun * Time.deltaTime);
                _animator.SetBool("Attack", false);
                
                // Rotación Gradual hacia el Objetivo.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speedRotation);
            }

            else
            {
                // Rotación Gradual hacia el Objetivo.
                transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, _speedRotation);

                _animator.SetBool("Walk", false);
                _animator.SetBool("Run", false);
            }
        }

        if (_isAttack)
        {
            _agent.enabled = false;
        }
    }

    public void FireAttack()
    {
        // Instanciar Bala.
        GameObject newBullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

        // Fuerza de Bala.
        Rigidbody rb = newBullet.GetComponent<Rigidbody>();
        rb?.AddForce(transform.forward * _shotForce, ForceMode.Impulse);
    }

    public void FinishAnimationAttack()
    {
        // Reinicio de Estado de Ataque.
        if (Vector3.Distance(transform.position, _targetPlayer.transform.position) > _distanceAttack + 0.2f)
        {
            _animator.SetBool("Attack", false);
        }
        
        _isAttack = false;

        // Habilita Collider del Rango del Enemigo para Atacar.
        _colliderRangeAttack.GetComponent<BoxCollider>().enabled = true;
    }

    public void ReduceLife()
    {
        _life -= 1;

        if (_life <= 0)
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