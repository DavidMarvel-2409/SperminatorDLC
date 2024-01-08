using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class uterScript : MonoBehaviour
{
    public static uterScript instancia;
    private float rota;                         //angulo de rotacion
    private int direct_angle;                   //para saber en que direccion esta girando el ovulo
    private int direct_sclae;                   //para saber si está creciendo o decreciendo
    public float top_angle;                     //para marcar el limite de giro
    //public float angle;                         //es un auxiliar
    private float escala;                       //la escala en la que está el ovulo
    private float escala_2;                     //cuanto se le suma a la escala

    public float vidaUter;

    //public int enemigos_muertos;

    private void Start()
    {
        instancia = this;
        rota = 0;                               //se inicializa en 0
        direct_angle = 1;                       //se inicializa en 1
        direct_sclae = 1;                       //se inicializa en 1
        escala = 1;                             //se inicializa en 1
        escala_2 = 0;                           //se inicializa en 0
        //enemigos_muertos = 0;
    }

    private void Update()
    {
        rotar();                                            //llama a la funcion que cambia el angulo

        if (escala_2 > 0)
        {
            revote();                                           //llama a la funcion que cambia la escala del ovulo
        }
        //angle = rota;
        transform.rotation = Quaternion.Euler(0, 0, rota);  //actualiza el transform de la rotacion
        transform.localScale = new Vector3(1, escala, 1);   //actualiza el transform de la escala

        if (vidaUter <= 0)
        {
            vidaUter = 0;
        }
    }
    private void rotar()
    {
        rota += 10 * direct_angle * Time.deltaTime;        //suma al angilo en 0.3f segun la direccion
        if (rota > top_angle)               //si llega al tope positivo de giro cambia la direccion
        {
            rota = top_angle;
            direct_angle *= -1;
        }
        else if (rota < -top_angle)         //si llega al tope negativo de giro cambia la direccion
        {
            rota = -top_angle;
            direct_angle *= -1;
        }
    }
    private void revote()
    {
        escala += escala_2 * direct_sclae * Time.deltaTime;  //suma a la escala la escala2 dependiendo de la direccion de crecimiento o decrecimiento

        if (escala > 1)                     //si llega al tope de crecimiento cambia la direccion de la escala
        {
            escala = 1;
            direct_sclae *= -1;
        }
        else if (escala < 0.9f)              //si llega al tope de decrecimiento cambia la direccion de escala
        {
            escala = 0.9f;
            direct_sclae *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)      //si coliciona con cualquier cosa se inicia el cambio de rebote
    {
        //Debug.Log("olha");
        escala_2 = 1;
        Invoke("parar_revote", 1.2f);           //llama a la funcion que frena el revote


        if (collision.gameObject.CompareTag("enemigo"))
        {
            vidaUter -= 10;
            //enemigos_muertos++;
        }

    }
    private void parar_revote()                 //deja en 0 el revote
    {
        escala_2 = 0;
        escala = 1;
    }

}
