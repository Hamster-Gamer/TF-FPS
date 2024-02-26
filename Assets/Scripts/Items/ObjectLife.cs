using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectLife : MonoBehaviour
{
    [SerializeField] AudioSource _audioSource;

    void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            FindObjectOfType<GameManager>().WinLife();

            _audioSource.Play();

            gameObject.SetActive(false);
            Destroy(gameObject, 1f);
        }
    }
}