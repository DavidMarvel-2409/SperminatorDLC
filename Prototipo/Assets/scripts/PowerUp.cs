using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    public string funcion;
    private float[] Escala_Original;
    private float[] Escala_Auxiliar;
    private float[] Limites_rotacion;
    private float[] Limites_revote;
    public float Speed_Rotation;
    public float Speed_rebound;
    private float angulo;
    public GameObject Colision;
    private bool boing;
    void Start()
    {
        Escala_Original = new float[2];
        Escala_Original[0] = transform.localScale.x;
        Escala_Original[1] = transform.localScale.y;
        Limites_rotacion = new float[3];
        Limites_rotacion[0] = transform.rotation.z + 30;
        Limites_rotacion[1] = transform.rotation.z;
        Limites_rotacion[2] = 1;
        angulo = transform.rotation.z;
        boing = false;
        Limites_revote = new float[6];
        Limites_revote[0] = 1;                              //direccion x
        Limites_revote[5] = 1;                              //direccion y
        Limites_revote[1] = transform.localScale.x * 0.9f;
        Limites_revote[2] = transform.localScale.y * 0.9f;

        Limites_revote[3] = transform.localScale.x * 1.1f;
        Limites_revote[4] = transform.localScale.y * 1.1f;

        Escala_Auxiliar = new float[2];
        Escala_Auxiliar[0] = transform.localScale.x;
        Escala_Auxiliar[1] = transform.localScale.y;
    }
    void Update()
    {
        transform.rotation = Quaternion.Euler(0, 0, angulo);
        Rotacion();
        if (Colision.GetComponent<Collider_PowerUp>().coliciono == true)
        {
            boing = true;
            Invoke("Parar_rebote", 1);
        }
        if (boing == true)
        {
            revote();
        }
        transform.localScale = new Vector3(Escala_Auxiliar[0], Escala_Auxiliar[1], transform.localScale.z);
    }
    private void Parar_rebote()
    {
        boing = false;
        Escala_Auxiliar[0] = Escala_Original[0];
        Escala_Auxiliar[1] = Escala_Original[1];
    }

    private void Rotacion()
    {
        if (angulo <= Limites_rotacion[0] && angulo >= Limites_rotacion[1])
        {
            angulo += Speed_Rotation * Limites_rotacion[2] * Time.deltaTime;
        }
        else
        {
            if (angulo > Limites_rotacion[0])
            {
                angulo = Limites_rotacion[0];
                Limites_rotacion[2] *= -1;
            }
            else if (angulo < Limites_rotacion[1])
            {
                angulo = Limites_rotacion[1];
                Limites_rotacion[2] *= -1;
            }
        }
    }

    private void revote()
    {
        if (Escala_Auxiliar[0] <= Limites_revote[3] && Escala_Auxiliar[0] >= Limites_revote[1])
        {
            Escala_Auxiliar[0] += Speed_rebound * Limites_revote[0] * Time.deltaTime;
        }
        else
        {
            if (Escala_Auxiliar[0] > Limites_revote[3])
            {
                Escala_Auxiliar[0] = Limites_revote[3];
                Limites_revote[0] *= -1;
            }
            else if (Escala_Auxiliar[0] < Limites_revote[1])
            {
                Escala_Auxiliar[0] = Limites_revote[1];
                Limites_revote[0] *= -1;
            }
        }
        if (Escala_Auxiliar[1] <= Limites_revote[4] && Escala_Auxiliar[1] >= Limites_revote[2])
        {
            Escala_Auxiliar[1] += Speed_rebound * Limites_revote[5] * Time.deltaTime;
        }
        else
        {
            if (Escala_Auxiliar[1] > Limites_revote[4])
            {
                Escala_Auxiliar[1] = Limites_revote[4];
                Limites_revote[5] *= -1;
            }
            else if (Escala_Auxiliar[1] < Limites_revote[2])
            {
                Escala_Auxiliar[1] = Limites_revote[2];
                Limites_revote[5] *= -1;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
