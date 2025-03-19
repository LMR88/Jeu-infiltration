using UnityEngine;
using UnityEngine.SceneManagement;

public class NiveauManager : MonoBehaviour
{
    public string prochainNiveau;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(prochainNiveau);
        }
    }
}

