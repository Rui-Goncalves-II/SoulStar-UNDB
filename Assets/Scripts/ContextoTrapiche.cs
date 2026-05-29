using UnityEngine;

public class PontoDeInteresse : MonoBehaviour
{
    public GameObject painelQuiz;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelQuiz.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            painelQuiz.SetActive(false);
        }
    }
}