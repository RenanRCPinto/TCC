using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BotaoHover : MonoBehaviour, IPointerEnterHandler
{
    [SerializeField] private AudioClip somHover;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (somHover != null)
            FindObjectOfType<AudioManager>().TocarSom(somHover);
    }
}
