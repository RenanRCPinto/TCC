using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManager>().TocarMusicaMenu();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Jogar()
    {
        SceneManager.LoadScene("Jogo");
    }

    public void Configuracoes()
    {
        SceneManager.LoadScene("Config");
    }

    public void Sair()
    {
        Application.Quit();
    }

}
