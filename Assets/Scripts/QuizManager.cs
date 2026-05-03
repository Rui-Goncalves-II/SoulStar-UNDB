// QuizManager.cs
using UnityEngine;
using UnityEngine.Events;
using TMPro;

public class QuizManager : MonoBehaviour {
    public static QuizManager Instance;

    [Header("UI")]
    public GameObject painelQuiz;
    public TextMeshProUGUI txtPergunta;
    public UnityEngine.UI.Button[] botoesOpcao;  // array de 3 botões

    [Header("Eventos")]
    public UnityEvent onQuizAprovado;   // Rui/Marcos conectam a transição aqui
    public UnityEvent onQuizReprovado;

    private PerguntasQuiz dadosAtual;
    private int indexAtual = 0;
    private int acertos = 0;

    void Awake() {
        // Isso garante que só exista um QuizManager no jogo todo (Singleton)
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void StartQuiz(PerguntasQuiz dados) {
        dadosAtual = dados;
        indexAtual = 0;
        acertos = 0;
        painelQuiz.SetActive(true);
        MostrarPergunta();
    }

    void MostrarPergunta() {
        if (indexAtual >= dadosAtual.perguntas.Length) {
            FinalizarQuiz(); return;
        }
        Pergunta p = dadosAtual.perguntas[indexAtual];
        txtPergunta.text = p.enunciado;
        for (int i = 0; i < botoesOpcao.Length; i++) {
            int idx = i; // captura para o closure
            botoesOpcao[i].GetComponentInChildren<TextMeshProUGUI>().text = p.opcoes[i];
            botoesOpcao[i].onClick.RemoveAllListeners();
            botoesOpcao[i].onClick.AddListener(() => ResponderPergunta(idx));
        }
    }

    void ResponderPergunta(int opcaoEscolhida) {
        bool correto = opcaoEscolhida == dadosAtual.perguntas[indexAtual].indiceCorreto;
        if (correto) acertos++;
        StartCoroutine(FeedbackEAvancar(correto, botoesOpcao[opcaoEscolhida]));
    }

    System.Collections.IEnumerator FeedbackEAvancar(bool correto, UnityEngine.UI.Button btn) {
        // Feedback visual: verde para acerto ou vermelho para erro por 1.5s
        var img = btn.GetComponent<UnityEngine.UI.Image>();
        img.color = correto ? new Color(0.2f,0.8f,0.3f) : new Color(0.9f,0.2f,0.2f);
        yield return new WaitForSeconds(1.5f);
        img.color = Color.white;
        indexAtual++;
        MostrarPergunta();
    }

    void FinalizarQuiz() {
        painelQuiz.SetActive(false);
        // Aqui o código tenta avisar o GameManager sobre os pontos ganhos
        // GameManager.Instance?.AdicionarPontos(acertos, dadosAtual.perguntas.Length);

        if (acertos >= dadosAtual.acertosNecessarios)
            onQuizAprovado.Invoke(); // Dispara o evento de sucesso configurado no Inspector
        else
            onQuizReprovado.Invoke(); // Dispara o evento de falha para tentar novamente
    }
}