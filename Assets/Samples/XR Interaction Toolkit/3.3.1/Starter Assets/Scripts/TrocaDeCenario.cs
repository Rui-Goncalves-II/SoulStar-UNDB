using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaDeCenario : MonoBehaviour
{
    public string nomeDoProximoCenario;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(nomeDoProximoCenario);
        }
    }
}