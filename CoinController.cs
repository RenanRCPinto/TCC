using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinController : MonoBehaviour
{
    [SerializeField] private float velocidade = 4f;
    [SerializeField] GameObject moeda;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(moeda, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.down * Time.deltaTime * velocidade;
    }
}
