using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Barra_Oleada : MonoBehaviour
{

    public float tiempo;
    public float tiempoTotal;
    public Image oleada;
    public bool IniciaOleada;
    public TextMeshProUGUI texto;

    public float oleadas;

    void Start()
    {
        IniciaOleada = false;
        oleadas = 0;
    }

    void Update()
    {
        if (IniciaOleada)
        {
            tiempo -= 0.5f * Time.deltaTime;
            oleada.fillAmount = tiempo / tiempoTotal;
        }

        if (tiempo < 40)
        {
            texto.text = "Oleada 1";
        }
        if (tiempo < 26)
        {
            texto.text = "Oleada 2";
        }
        if (tiempo < 11)
        {
            texto.text = "Oleada 3";
        }

        
    }
}
