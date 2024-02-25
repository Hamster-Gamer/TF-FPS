using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sway : MonoBehaviour
{
    Quaternion _startPosition;
    [SerializeField] float _swayAmount = 5f;

    void Start()
    {
        // Almacenamiento de Rotación Local.
        _startPosition = transform.localRotation;
    }

    void Update()
    {
        Swinging();
    }

    void Swinging()
    {
        // Almacenamiento de las Entradas del Mouse.
        float mouseX = Input.GetAxis("Mouse X");
        float mouseY = Input.GetAxis("Mouse Y");

        // Obtención de Rotaciones X, Y.
        Quaternion xAngle = Quaternion.AngleAxis(mouseX * -1.25f, Vector3.up);
        Quaternion yAngle = Quaternion.AngleAxis(mouseY * 1.25f, Vector3.left);

        // Suma de Rotaciones X, Y.
        Quaternion targetRotation = _startPosition * xAngle * yAngle;

        // Suavizado para las Rotaciones.
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation, Time.deltaTime * _swayAmount);
    }
}