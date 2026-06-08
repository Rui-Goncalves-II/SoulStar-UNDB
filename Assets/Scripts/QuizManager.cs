// QuizManager.cs
using UnityEngine;
using UnityEngine.Events;
using TMPro;
using System.Collections;

public class QuizManager : MonoBehaviour
{
    public static QuizManager Instance;

    [Header("UI")]
    public GameObject painelQuiz;
    public TextMeshProUGUI txtPergunta;
    public UnityEngine.UI.Button[] botoesOpcao;

    [Header("Dados")]
    public PerguntasQuiz dadosAtual;

    [Header("Eventos")]
    public UnityEvent onQuizAprovado;
    public UnityEvent onQuizReprovado;

    private int indexAtual = 0;
    private int acertos = 0;
    private bool aguardandoResposta = true;

    void Awake()
    {
        if (Instance != null) { Destroy(gameObject); return; }
        Instance = this;
    }

    void Start()
    {
        IniciarQuiz();
    }

    void IniciarQuiz()
    {
        indexAtual = 0;
        acertos = 0;
        painelQuiz.SetActive(true);
        MostrarPergunta();
    }

    void MostrarPergunta()
    {
        if (indexAtual >= dadosAtual.perguntas.Length)
        {
            FinalizarQuiz();
            return;
        }

        aguardandoResposta = true;

        foreach (var btn in botoesOpcao)
        {
            btn.GetComponent<UnityEngine.UI.Image>().color = Color.white;
        }

        Pergunta p = dadosAtual.perguntas[indexAtual];
        txtPergunta.text = p.enunciado;

        for (int i = 0; i < botoesOpcao.Length; i++)
        {
            int idx = i;

            TextMeshProUGUI tmp = botoesOpcao[i].GetComponentInChildren<TextMeshProUGUI>();

            if (tmp == null)
            {
                Debug.LogError("TMP NAO ENCONTRADO no botao " + i);
            }
            else
            {
                Debug.Log("TMP encontrado no botao " + i + " | texto atual: " + tmp.text);
                tmp.text = p.opcoes[i];
                tmp.color = Color.black;
            }

            botoesOpcao[i].onClick.RemoveAllListeners();
            botoesOpcao[i].onClick.AddListener(() => ResponderPergunta(idx));
        }
    }

    void ResponderPergunta(int opcaoEscolhida)
    {
        if (!aguardandoResposta) return;
        aguardandoResposta = false;

        bool correto = opcaoEscolhida == dadosAtual.perguntas[indexAtual].indiceCorreto;
        if (correto) acertos++;

        StartCoroutine(FeedbackEAvancar(correto, opcaoEscolhida));
    }

    IEnumerator FeedbackEAvancar(bool correto, int opcaoEscolhida)
    {
        int indiceCorreto = dadosAtual.perguntas[indexAtual].indiceCorreto;

        if (correto)
        {
            botoesOpcao[opcaoEscolhida].GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.3f);
        }
        else
        {
            botoesOpcao[opcaoEscolhida].GetComponent<UnityEngine.UI.Image>().color = new Color(0.9f, 0.2f, 0.2f);
            botoesOpcao[indiceCorreto].GetComponent<UnityEngine.UI.Image>().color = new Color(0.2f, 0.8f, 0.3f);
        }

        yield return new WaitForSeconds(1.5f);

        indexAtual++;
        MostrarPergunta();
    }

    void FinalizarQuiz()
    {
        painelQuiz.SetActive(false);

        if (acertos >= dadosAtual.acertosNecessarios)
            onQuizAprovado.Invoke();
        else
            onQuizReprovado.Invoke();
    }
}