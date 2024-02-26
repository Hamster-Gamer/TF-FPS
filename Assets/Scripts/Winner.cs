using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour
{
    void OnTriggerEnter(Collider collider)
    {
        // Daño al Jugador.
        if (collider.CompareTag("Player"))
        {
            SceneManager.LoadScene(5);
        }
    }
}
