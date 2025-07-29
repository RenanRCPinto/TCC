using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    [SerializeField] private GameObject asteroide;
    [SerializeField] private GameObject moeda;
    [SerializeField] private Vector3 posDir;
    [SerializeField] private Vector3 posEsq;
    [SerializeField] private Vector3 posMoeda;
    [SerializeField] private float posMax = 8f;
    [SerializeField] private float timer1 = 1f;
    [SerializeField] private float timer2 = 1f;
    [SerializeField] private AudioClip somMoeda;
    [SerializeField] private AudioClip somAsteroide;
    [SerializeField] private AudioClip somMorte;
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private int pontuacao = 0;
    private float tempoJogo = 0f;
    [SerializeField] private Texture2D iconeCoracao;
    [SerializeField] private int vida = 3;
    [SerializeField] private int nivelDificuldade = 1;

    [SerializeField] private float intervaloNivel = 300f; // 5 minutos por nível
    [SerializeField] private int maxNiveis = 10;

    [SerializeField] private float limiteFacil = 5f;
    [SerializeField] private float limiteDificil = 1f;

    [SerializeField] private float espacoSeguroAtual;
    bool venceu = false;

    // Start is called before the first frame update
    void Start()
    {
        nivelDificuldade = ConfigGlobal.dificuldadeInicial;

        AtualizarEspacoSeguro();
    }

    // Update is called once per frame
    void Update()
    {
        tempoJogo += Time.deltaTime;
        int novoNivel = Mathf.Min(maxNiveis, ConfigGlobal.dificuldadeInicial + Mathf.FloorToInt(tempoJogo / intervaloNivel));
        if (novoNivel != nivelDificuldade)
        {
            nivelDificuldade = novoNivel;
            AtualizarEspacoSeguro();
        }

        timer1 -= Time.deltaTime;
        if (timer1 <= 0)
        {
            timer1 = 1f;
            float zonaPerigoMin = espacoSeguroAtual;

            posDir.x = Random.Range(zonaPerigoMin, posMax);
            posEsq.x = Random.Range(-posMax, -zonaPerigoMin);

            Instantiate(asteroide, posDir, Quaternion.identity);
            Instantiate(asteroide, posEsq, Quaternion.identity);

        }
        timer2 -= Time.deltaTime * 0.5f;
        if (timer2 <= 0)
        {
            timer2 = 1f;
            float limite = GameObject.FindObjectOfType<GameController>().GetLimiteMoeda();
            posMoeda.x = Random.Range(-limite, limite); ;
            Instantiate(moeda, posMoeda, Quaternion.identity);
        }

        if (tempoJogo >= 900f && !venceu)
        {
            venceu = true;
            DadosFinais.pontuacao = pontuacao;
            DadosFinais.tempo = tempoJogo;
            DadosFinais.nivel = nivelDificuldade;
            SceneManager.LoadScene("Vitoria");
        }

    }

    private void AtualizarEspacoSeguro()
    {
        float t = (nivelDificuldade - 1) / (float)(maxNiveis - 1);
        espacoSeguroAtual = Mathf.Lerp(limiteFacil, limiteDificil, t);
    }

    public void TocarSom(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    public void AdicionarPontuacao(int valor)
    {
        pontuacao += valor;
    }

    public void AtualizarVidas(int novaVida)
    {
        vida = Mathf.Clamp(novaVida, 0, 3);
    }

    public float GetLimiteMoeda()
    {
        float proporcao = (float)(nivelDificuldade - 1) / (maxNiveis - 1);
        float limiteMin = 0f;
        float limiteMax = 2f;

        return Mathf.Lerp(limiteMax, limiteMin, proporcao); // vai de 2 até 0
    }

    void OnGUI()
    {
        GUIStyle estilo = new GUIStyle();
        estilo.fontSize = 100;
        estilo.normal.textColor = Color.white;

        GUI.Label(new Rect(20, 20, 300, 50), "Pontuação: " + pontuacao, estilo);

        GUIStyle estiloNivel = new GUIStyle();
        estiloNivel.fontSize = 100;
        estiloNivel.normal.textColor = Color.white;
        estiloNivel.alignment = TextAnchor.UpperCenter;

        GUI.Label(new Rect(Screen.width / 2 - 75, 120, 150, 50), "Nível: " + nivelDificuldade,estiloNivel);

        GUIStyle estiloCentro = new GUIStyle();
        estiloCentro.fontSize = 100;
        estiloCentro.alignment = TextAnchor.UpperCenter;
        estiloCentro.normal.textColor = Color.white;

        string tempoFormatado = Mathf.FloorToInt(tempoJogo).ToString();
        GUI.Label(new Rect(Screen.width / 2 - 75, 20, 150, 50), "Tempo: " + tempoFormatado, estiloCentro);

        for (int i = 0; i < vida; i++)
        {
            GUI.DrawTexture(new Rect(Screen.width - 540 + 170 * i, 20, 150, 150), iconeCoracao);
        }
    }

    public float GetFatorDificuldade()
    {
        return 1f + (tempoJogo / 30f); // aumenta progressivamente
    }

    public AudioClip GetSomMoeda() => somMoeda;
    public AudioClip GetSomAsteroide() => somAsteroide;
    public AudioClip GetSomMorte() => somMorte;
    public int GetPontuacao() => pontuacao;
    public float GetTempo() => tempoJogo;

    public int GetNivel() => nivelDificuldade;

}
