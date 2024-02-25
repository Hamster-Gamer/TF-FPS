using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [Header("Fire")]
    [SerializeField] Transform _firePoint;
    [SerializeField] GameObject _bulletPrefab;

    // Variables - Fuerza de Disparo.
    [SerializeField] float _shotForce = 50f;
    
    // Variables - Tiempo de Disparo.
    [SerializeField] float _shotCoolDown = 0.5f;
    float _shotRateTime = 0;

    // Variables - Tiempo de Vida de Balas.
    [SerializeField] float _bulletLifeTime = 5f;

    // Variables - Apuntar.
    Camera _mainCamera;
    bool _isAim = false;
    float _startFOV;
    [Header("Aim")]
    [SerializeField] Transform _startingPoint;
    [SerializeField] Transform _aimPoint;
    [SerializeField] float _aimFOV = 40f;

    void Start()
    {
        _mainCamera = Camera.main;
        _startFOV = _mainCamera.fieldOfView;
    }

    void Update()
    {
        Fire();
        Aim();
    }

    void Fire()
    {
        if (Input.GetMouseButton(0))
        {
            if(Time.time > _shotRateTime)
            {
                // Instanciar Bala.
                GameObject newBullet = Instantiate(_bulletPrefab, _firePoint.position, Quaternion.identity);

                // Fuerza de Bala.
                Rigidbody rb = newBullet.GetComponent<Rigidbody>();
                rb?.AddForce(-transform.forward * _shotForce, ForceMode.Impulse);

                // Tiempo de Cooldown.
                _shotRateTime = Time.time + _shotCoolDown;

                // Destrucción de Bala.
                Destroy(newBullet, _bulletLifeTime);
            }
        }
    }

    void Aim()
    {
        if (Input.GetMouseButton(1))
        {
            _isAim = true;
        }

        if (_isAim)
        {
            // Cambio de Posición de Arma.
            transform.position = Vector3.Lerp(transform.position, _aimPoint.position, Time.deltaTime * 7.5f);

            // FOV Gradual.
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _aimFOV, Time.deltaTime * 7.5f);

            if (!Input.GetMouseButton(1))
            {
                _isAim = false;

                // Cambio de Posición de Arma.
                transform.position = _startingPoint.position;
                // FOV Gradual.
                _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _startFOV, Time.deltaTime * 7.5f);
            }
        }

        else
        {
            // Cambio de Posición de Arma.
            transform.position = _startingPoint.position;
            // FOV Gradual.
            _mainCamera.fieldOfView = Mathf.Lerp(_mainCamera.fieldOfView, _startFOV, Time.deltaTime * 7.5f);
        }
    }
}