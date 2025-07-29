using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidController : MonoBehaviour
{
    [SerializeField] private float velocidade = 4f;
    [SerializeField] GameObject asteroide;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(asteroide, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * velocidade;
    }
}
