using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class barraVidaUtero : MonoBehaviour
{
    public float vidaTotalUtero;
    public Image vidaUtero;
    public GameObject utero;
    private float estadoVida;

    void Start()
    {

    }

    void Update()
    {
        estadoVida = utero.GetComponent<uterScript>().vidaUter;

        vidaUtero.fillAmount = estadoVida / vidaTotalUtero;

    }
}
