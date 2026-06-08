using UnityEngine;

public class GatilhoProximidade : MonoBehaviour
{
    // Variável que vai receber o seu quadro de informações
    public GameObject painelInfo;

    void OnTriggerEnter(Collider outro)
    {
        // Verifica se quem entrou no raio verde foi o jogador
        if (outro.CompareTag("Player"))
        {
            painelInfo.SetActive(true);
        }
    }

    void OnTriggerExit(Collider outro)
    {
        // Verifica se quem saiu do raio verde foi o jogador
        if (outro.CompareTag("Player"))
        {
            painelInfo.SetActive(false);
        }
    }
}