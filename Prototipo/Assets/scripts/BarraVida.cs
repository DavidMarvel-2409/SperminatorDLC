using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public float vidaTotalPlayer;
    public Image oleada;
    public GameObject jugador;
    private float estadoVida;

    void Start()
    {

    }

    void Update()
    {
            estadoVida = jugador.GetComponent<movement>().varVida;
            
            oleada.fillAmount = estadoVida / vidaTotalPlayer;

    }
}
