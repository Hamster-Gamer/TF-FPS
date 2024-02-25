using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    [SerializeField] float _speedMovement = 5f;
    [SerializeField] float _smoothStopping = 0.1f;

    [SerializeField] float _jumpForce = 2f;
    bool _floorDetected = false;

    void Update()
    {
        HandleJump();
    }

    void FixedUpdate()
    {
        // Movimiento básico.
        float moveHorizontal = Input.GetAxisRaw("Horizontal");
        float moveVertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = (transform.forward * moveVertical + transform.right * moveHorizontal).normalized;
        
        // Aplica fuerza solo si hay entrada de movimiento.
        if (direction.magnitude > 0)
        {
            _rb.AddForce(direction * _speedMovement, ForceMode.Force);
        }

        // Aplica una fracción de la fuerza actual para detener el movimiento gradualmente.
        else
        {
            Vector3 oppositeForce = -_rb.velocity * (_speedMovement * _smoothStopping);
            _rb.AddForce(oppositeForce, ForceMode.Force);
        }
    }

    void HandleJump()
    {
        // Verificación de Salto.
        if (Input.GetButtonDown("Jump") && _floorDetected)
        {
            // Restablece la velocidad vertical antes de aplicar la fuerza de salto.
            _rb.velocity = new Vector3(_rb.velocity.x, 0f, _rb.velocity.z);

            // Aplicación del salto.
            _rb.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
        }

        // Detección de Suelo.
        if (Physics.Raycast(transform.position, Vector3.down, 1.05f))
        {
            _floorDetected = true;
        }

        else
        {
            _floorDetected = false;
        }
    }
}