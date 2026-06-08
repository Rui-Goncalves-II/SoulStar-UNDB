// PerguntasQuiz.cs
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz/Perguntas")]
public class PerguntasQuiz : ScriptableObject
{
    public Pergunta[] perguntas;
    public int acertosNecessarios = 2;
}

[System.Serializable]
public class Pergunta
{
    public string enunciado;
    public string[] opcoes;
    public int indiceCorreto;
}