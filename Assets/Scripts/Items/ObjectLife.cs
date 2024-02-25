using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().WinLife();
            Destroy(gameObject);
        }
    }
}