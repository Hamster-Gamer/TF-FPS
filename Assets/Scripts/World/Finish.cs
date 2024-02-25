using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Finish : MonoBehaviour
{
    void OnCollisionEnter(Collision Colision)
    {
        if (Colision.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Winner");
        }
    }
}