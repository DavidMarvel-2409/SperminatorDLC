using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CambioCinematica : MonoBehaviour
{
    private float time;
    new public string name;
    public float speed;
    public GameObject tipa;
    public GameObject tipo;
    private Vector3 tipo_coor;
    private Vector3 tipa_coor;

    private Vector3 tipo_origen;
    private Vector3 tipa_origen;
    private int direct;
    private void Start()
    {
        time = 0;
        Debug.Log(tipa.transform);
        Debug.Log(tipo.transform);
        tipa_coor = tipa.transform.position;
        tipo_coor = tipo.transform.position;
        if (SceneManager.GetActiveScene().name == "Cinematic")
        {
            tipo_coor.y = tipa_coor.y;
        }
        Time.timeScale = 1;
        tipo_origen = tipo_coor;
        tipa_origen = tipa_coor;
        direct = 1;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (SceneManager.GetActiveScene().name == "Cinematic")
        {
            if (time < 3)
            {
                mover();
                //cambio();
            }
        }
        else if (SceneManager.GetActiveScene().name == "Cinematica 2")
        {
            nikiniki();
        }
        else if (SceneManager.GetActiveScene().name == "Cinematica 3")
        {
            nikiniki2();
        }
        tipo.transform.position = tipo_coor;
        tipa.transform.position = tipa_coor;
        if (time > 4)
        {
            cambio();
        }
    }
    private void mover()
    {
        tipa_coor.x += speed * Time.deltaTime;
        tipo_coor.x -= speed * Time.deltaTime;
    }
    private void cambio()
    {
        SceneManager.LoadScene(name);
    }

    private void nikiniki()
    {
        if (tipo_coor.y > tipo_origen.y)
        {
            tipo_coor = tipo_origen;
            direct *= -1;
        }
        else if (tipo_coor.y < tipo_origen.y - 2)
        {
            tipo_coor.y = tipo_origen.y - 2;
            direct *= -1;
        }

        tipo_coor.y -= speed * direct * Time.deltaTime;
    }
    private void nikiniki2()
    {
        if (tipa_coor.y > tipa_origen.y)
        {
            tipa_coor = tipa_origen;
            direct *= -1;
        }
        else if (tipa_coor.y < tipa_origen.y - 2)
        {
            tipa_coor.y = tipa_origen.y - 2;
            direct *= -1;
        }

        tipa_coor.y -= speed * direct * Time.deltaTime;
    }
}
