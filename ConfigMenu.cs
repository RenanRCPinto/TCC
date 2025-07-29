using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfigMenu : MonoBehaviour
{
    [SerializeField] private Slider seletorDificuldade;
    [SerializeField] private Text textoValor;

    // Start is called before the first frame update
    void Start()
    {
        seletorDificuldade.value = ConfigGlobal.dificuldadeInicial;
        AtualizarTexto((int)seletorDificuldade.value);
        seletorDificuldade.onValueChanged.AddListener((v) => AtualizarTexto((int)v));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AtualizarTexto(int valor)
    {
        ConfigGlobal.dificuldadeInicial = valor;
        textoValor.text = "Dificuldade Inicial: " + valor.ToString();
    }

    public void Voltar()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
    }

}
