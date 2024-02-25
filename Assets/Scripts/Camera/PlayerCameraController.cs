using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
    [SerializeField] Transform _player;
    [SerializeField] float _mouseSensitivity = 75f;

    float _xRotation = 0;

    void Start()
    {
        // Bloqueo de Cursor.
        Cursor.lockState = CursorLockMode.Locked;   
    }

    void Update()
    {
        // Obtención de Rotaciones X Y.
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        // Cálculo de Rotación X con Limitaciones.
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        // Aplicar Rotación X.
        transform.localRotation = Quaternion.Euler(_xRotation, 0, 0);

        // Rotación del Jugador.
        _player.Rotate(Vector3.up * mouseX);
    }
}