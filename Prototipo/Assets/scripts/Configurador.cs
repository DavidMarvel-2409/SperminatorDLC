using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Configurador : MonoBehaviour
{
    public Camera Camara;
    public GameObject Camvas;
    private Vector2[] esquinas;
    private float[] dimenciones;
    private Vector2 origen;
    public void Start()
    {
        esquinas = new Vector2[2];              //0 = sup_izq       1 = inf_der
        dimenciones = new float[2];             //0 = ancho         1 = alto
        origen = new Vector2(0, 0);
    }
    private void Update()
    {
        Obtener_datos();
        redimencionar_cosas();
    }
    private void Obtener_datos()
    {
        //origen = new Vector2(Camara.orthographicSize * 2 *)
        dimenciones[0] = Camara.orthographicSize * 2 * Camara.aspect;
        dimenciones[1] = Camara.orthographicSize * 2;
        origen = new Vector2(Camara.transform.position.x, Camara.transform.position.y);
        esquinas[0] = new Vector2(origen.x - (dimenciones[0] / 2), origen.y + (dimenciones[1] / 2));
        esquinas[1] = new Vector2(origen.x + (dimenciones[0] / 2), origen.y - (dimenciones[1] / 2));
    }
    private void redimencionar_cosas()
    {
        
    }

    private float regla_de_tres(float numA, float numB, float numC, string direc)
    {
        float incognita;

        if (direc == "inicial")
        {
            incognita = (numC * numA) / numB;
        }
        else
        {
            incognita = (numC * numB) / numA;
        }
        return incognita;
    }
}
