using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    
    [SerializeField] private float velocidade = 5f;
    [SerializeField] private Vector3 posicao;
    [SerializeField] private float posicaoX = 0f;
    [SerializeField] private float limiteHorizontal = 7.7f;
    [SerializeField] private int vida = 3;
    private GameController gameController;
    private float posicaoAlvoX = 0f;
    [SerializeField] float suavidade = 10f; // maior = mais suave
    public enum ModoControle { Sensor, Teclado }
    public ModoControle modoAtual = ModoControle.Sensor;

    // Start is called before the first frame update
    void Start()
    {
        posicao = transform.position;
        gameController = FindObjectOfType<GameController>();
    }

    // Update is called once per frame
    void Update()
    {
        // Alternar modo ao pressionar T
        if (Input.GetKeyDown(KeyCode.T))
        {
            modoAtual = (modoAtual == ModoControle.Sensor) ? ModoControle.Teclado : ModoControle.Sensor;
            Debug.Log("Modo de controle: " + modoAtual);
        }

        


        // Movimento baseado no modo atual
        if (modoAtual == ModoControle.Teclado)
        {
            Mover();
        }
        else
        {
            posicaoX = Mathf.Lerp(posicaoX, posicaoAlvoX, Time.deltaTime * suavidade);
        }

        transform.position = new Vector3(posicaoX, transform.position.y, transform.position.z);
    }

    private void Mover()
    {
        posicao.x = posicaoX;
        transform.position = posicao;
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            if(posicaoX > -limiteHorizontal)
            {
                posicaoX = posicaoX - velocidade * Time.deltaTime;
            }
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            if (posicaoX < limiteHorizontal)
            {
                posicaoX = posicaoX + velocidade * Time.deltaTime;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Asteroide"))
        {
            vida--;
            gameController.TocarSom(gameController.GetSomAsteroide());
            Destroy(collision.gameObject);
            gameController.AtualizarVidas(vida);
            if (vida <= 0)
            {
                DadosFinais.pontuacao = gameController.GetPontuacao();
                DadosFinais.tempo = gameController.GetTempo();
                DadosFinais.nivel = gameController.GetNivel();
                gameController.TocarSom(gameController.GetSomMorte());
                Destroy(gameObject);
                SceneManager.LoadScene("GameOver");
            }
        }

        if (collision.CompareTag("Moeda"))
        {
            gameController.TocarSom(gameController.GetSomMoeda());
            gameController.AdicionarPontuacao(1);
            Destroy(collision.gameObject);
        }
    }

    public void DefinirPosicaoAlvo(float destino)
    {
        if (modoAtual == ModoControle.Sensor)
        {
            posicaoAlvoX = Mathf.Clamp(destino, -limiteHorizontal, limiteHorizontal);
        }
    }

}
