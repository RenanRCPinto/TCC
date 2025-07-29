using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GameObject menuPause;
    private bool pausado = false;

    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausado = !pausado;
            menuPause.SetActive(pausado);
            Time.timeScale = pausado ? 0f : 1f;
        }
    }

    public void Continuar()
    {
        pausado = false;
        menuPause.SetActive(false);
        Time.timeScale = 1f;
    }

    public void VoltarMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
