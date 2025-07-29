using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameOver : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().TocarMusicaGameOver();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnGUI()
    {
        GUIStyle estilo = new GUIStyle();
        estilo.fontSize = 130;
        estilo.normal.textColor = Color.white;
        estilo.alignment = TextAnchor.UpperCenter;

        string tempo = Mathf.FloorToInt(DadosFinais.tempo).ToString();

        GUI.Label(new Rect(Screen.width / 2 - 150, 820, 300, 50), "Pontuação Final: " + DadosFinais.pontuacao, estilo);
        GUI.Label(new Rect(Screen.width / 2 - 150, 980, 300, 50), "Tempo Final: " + tempo + "s", estilo);
        GUI.Label(new Rect(Screen.width / 2 - 150, 1150, 300, 50), "Nível alcançado: " + DadosFinais.nivel, estilo);
    }


    public void VoltarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Sair()
    {
        Application.Quit();
    }
}
