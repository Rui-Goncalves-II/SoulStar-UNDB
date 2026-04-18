using UnityEngine;

// Define a estrutura de uma pergunta individual
[System.Serializable]
public class Pergunta
{
    public string enunciado;
    public string[] opcoes;       // sempre 3 opções
    public int indiceCorreto;     // 0, 1 ou 2
}

// Transforma o script em um "gerador de arquivos" de dados
[CreateAssetMenu(fileName = "Perguntas", menuName = "SoulStar/Perguntas Quiz")]
public class PerguntasQuiz : ScriptableObject
{
    public Pergunta[] perguntas;
    public int acertosNecessarios = 1;
}