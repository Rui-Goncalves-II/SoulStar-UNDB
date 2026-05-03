using UnityEngine;

public class TestadorQuiz : MonoBehaviour {
    public PerguntasQuiz perguntasDoLobby; // Aqui você vai arrastar seu arquivo de perguntas

    void Update() {
        // Quando você apertar a tecla Espaço, o Quiz começa!
        if (Input.GetKeyDown(KeyCode.Space)) {
            QuizManager.Instance.StartQuiz(perguntasDoLobby);
        }
    }
}