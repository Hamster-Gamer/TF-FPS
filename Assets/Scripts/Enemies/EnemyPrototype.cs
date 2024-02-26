using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollowUp : MonoBehaviour
{
    [SerializeField] int _enemyLife = 5;
    
    Vector3 _positionInitial;
    [SerializeField] Rigidbody _RB;

    [SerializeField] Transform _player;
    NavMeshAgent _enemy;

    public void Start()
    {
        _positionInitial = transform.position;

        _enemy = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        EnemyMovement();
    }

    public void ReduceLife()
    {
        _enemyLife -= 1;

        if (_enemyLife <= 0)
        {
            Destroy(gameObject);
        }
    }

    void EnemyMovement()
    {
        _enemy.destination = _player.position;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().LoseLife();
        }
    }

    public void ResetEnemy()
    {
        transform.position = _positionInitial;
        _RB.velocity = Vector3.zero;
    }
}