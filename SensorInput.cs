using System.IO.Ports;
using UnityEngine;

public class SensorInput : MonoBehaviour
{
    [SerializeField] private string portaSerial = "COM3"; // Ver no gerenciador de dispositivos
    [SerializeField] private int baudRate = 115200;
    private SerialPort porta;
    [SerializeField] private PlayerController player;
    [SerializeField] private float alcanceMaximo = 7.7f;
    private long offsetEsq = 0;
    private long offsetDir = 0;
    private bool calibrado = false;
    private int amostrasCalibracao = 50;
    private int contador = 0;
    private long somaEsq = 0;
    private long somaDir = 0;
    [SerializeField] float ganho = 1.0f;
    [SerializeField] float zonaMorta = 0.15f; // valores menores que 5% serão ignorados
    private float destinoAnterior = 0f;
    [SerializeField] private float suavizacaoSensor = 0.2f; // entre 0 e 1
    [SerializeField] private float fatorSensibilidade = 200f; // ajuste conforme necessário

    void Start()
    {
        porta = new SerialPort(portaSerial, baudRate);
        try
        {
            porta.Open();
        }
        catch (System.Exception e)
        {
            Debug.LogError("Erro ao abrir porta " + portaSerial + ": " + e.Message);
            Debug.LogException(e); // opcional: mostra o stack trace completo
        }
    }


    void Update()
    {
        if (porta == null || !porta.IsOpen || porta.BytesToRead == 0)
            return;

        try
        {
            string linha = porta.ReadLine();
            string[] dados = linha.Split(',');

            if (dados.Length != 2 ||
                !long.TryParse(dados[0], out long sE) ||
                !long.TryParse(dados[1], out long sD))
                return;

            // Calibração inicial
            if (!calibrado)
            {
                somaEsq += sE;
                somaDir += sD;
                contador++;

                if (contador >= amostrasCalibracao)
                {
                    offsetEsq = somaEsq / contador;
                    offsetDir = somaDir / contador;
                    calibrado = true;
                    Debug.Log($"Offset calibrado: Esq={offsetEsq}, Dir={offsetDir}");
                }
                return;
            }

            // Leitura com offset
            float sECalibrado = sE - offsetEsq;
            float sDCalibrado = sD - offsetDir;

            float total = Mathf.Abs(sECalibrado + sDCalibrado); //  garante positivo
            float diff = sECalibrado - sDCalibrado;
            Debug.Log($"Esq: {sECalibrado}, Dir: {sDCalibrado}, Dif: {diff}, Total: {total}");

            if (total < 1000)
            {
                player.DefinirPosicaoAlvo(0f); // se sem peso, volta ao centro
                return;
            }

            float normalizado = Mathf.Clamp(diff * fatorSensibilidade, -1f, 1f);

            // zona morta real
            if (Mathf.Abs(normalizado) < zonaMorta)
                normalizado = 0f;

            // clamp e ganho
            normalizado = Mathf.Clamp(normalizado, -1f, 1f);
            float destinoBruto = normalizado * ganho * alcanceMaximo;
            float destino = Mathf.Lerp(destinoAnterior, destinoBruto, suavizacaoSensor);
            destinoAnterior = destino;

            Debug.Log($"Destino final enviado: {destino}");

            player.DefinirPosicaoAlvo(destino);
        }
        catch { }
    }

    void OnApplicationQuit()
    {
        if (porta != null && porta.IsOpen) porta.Close();
    }
}
