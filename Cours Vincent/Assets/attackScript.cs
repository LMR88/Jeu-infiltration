using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attackScript : MonoBehaviour
{
    public GameObject gameOverUi;
    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("collision");
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.SetActive(false);
            gameOverUi.SetActive(true);
        }
    }
}
