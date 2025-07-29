using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioClip musicaMenu;
    [SerializeField] private AudioClip musicaGameOver;
    [SerializeField] private AudioSource audioSource;

    private static AudioManager instancia;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Awake()
    {
        if (instancia == null)
        {
            instancia = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TocarMusicaMenu()
    {
        audioSource.clip = musicaMenu;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TocarMusicaGameOver()
    {
        audioSource.clip = musicaGameOver;
        audioSource.loop = true;
        audioSource.Play();
    }

    public void TocarSom(AudioClip som)
    {
        audioSource.PlayOneShot(som);
    }
}
