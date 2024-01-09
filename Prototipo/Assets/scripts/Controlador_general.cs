using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controlador_general : MonoBehaviour
{
    public Camera camara;
    private float[] aspecto_camara;             //16 x 9        (original)
    private float[] tamano_camara_original;     //1920 x 1080   (original)
    private float[] tamano_camara;              //tamaño actual durante la ejecucion del juego

    private float escala_de_las_cosas;
    public GameObject OrigenCanvas;
    public GameObject[] cosas_del_canvas;

    public GameObject Spawner;
    public GameObject Inicio_oleada;
    public GameObject Player1;
    public GameObject Ovulo;
    public GameObject panel_pausa;
    private string nivel;
    public AudioSource audiosource;
    [SerializeField] AudioClip Muerte_jugador;
    [SerializeField] AudioClip Jefe;

    public GameObject Panel_Win;
    public GameObject Panel_Lose;
    public GameObject precentacion_jefe;
    public GameObject tutorial;

    public bool Menu_pausa;

    public int enemigos_en_escena;
    private bool precentacion_check = false;

    public GameObject spawn_player;

    public GameObject mapa;

    public TextMeshProUGUI oleada_text;

    public GameObject[] Objetivos_Originales;
    private bool trigger = false;


    private void Start()
    {
        nivel = SceneManager.GetActiveScene().name;
        audiosource = GetComponent<AudioSource>();
        Menu_pausa = false;
        enemigos_en_escena = 0;
        if (spawn_player.activeInHierarchy == true)
        {
            Player1.transform.position = spawn_player.transform.position;
        }
        setear_objetos_en_mapa("" + SceneManager.GetActiveScene().name);

        aspecto_camara = new float[2];
        tamano_camara_original = new float[2];
        aspecto_camara[0] = 16;             //ancho
        aspecto_camara[1] = 9;              //alto
        tamano_camara_original[0] = 1920;   //ancho
        tamano_camara_original[1] = 1080;   //alto
        tamano_camara = new float[2];

        //Debug.Log("" + SceneManager.GetActiveScene().name);
    }
    private void Update()
    {
        set_tamano_camara();
        if (nivel == "Nivel 3" && oleada_text.text == "Oleada 3" && precentacion_check == false)
        {
            precentacion_check = true;
            precentacion_jefe.SetActive(true);
            Time.timeScale = 0;
            Spawner.GetComponent<creadorEnemigos>().final_boss = true;
            if (audiosource.isPlaying == false && trigger == false)
            {
              audiosource.PlayOneShot(Jefe);
              trigger = true;
            }
        }

        if (Vector3.Distance(Player1.transform.position, Ovulo.transform.position) < 120)
        {
            Spawner.GetComponent<creadorEnemigos>().comienza = true;
            Inicio_oleada.SetActive(true);
            Inicio_oleada.GetComponent<Barra_Oleada>().IniciaOleada = true;
        }
        
        if ( Ovulo.GetComponent<uterScript>().vidaUter <= 0 || Player1.GetComponent<movement>().varVida == 0)
        {
            //SceneManager.LoadScene("Perdida");
            //audiosource.PlayOneShot(Muerte_jugador);
            Time.timeScale = 0;
            Panel_Lose.SetActive(true);
            //audiosource.PlayOneShot(Muerte_jugador);
            if (audiosource.isPlaying == false && trigger==false) {
                audiosource.PlayOneShot(Muerte_jugador);
                trigger = true;
            }
        }


        if (Inicio_oleada.GetComponent<Barra_Oleada>().tiempo <= 0.1 
            && enemigos_en_escena - Player1.GetComponent<movement>().Enemigos_muertos <= 0 && nivel != "Nivel 3")
        {
                Panel_Win.SetActive(true);
                Time.timeScale = 0;
            
            //SceneManager.LoadScene("finalscene");
        }
        if (Inicio_oleada.GetComponent<Barra_Oleada>().tiempo <= 0.1
            && enemigos_en_escena - Player1.GetComponent<movement>().Enemigos_muertos - 100 <= 0 && nivel == "Nivel 3")
        {
            
                Panel_Win.SetActive(true);
                Time.timeScale = 0;
            //SceneManager.LoadScene("finalscene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0)
            {
                Time.timeScale = 1;
                Menu_pausa = false;
            }
            else
            {
                Time.timeScale = 0;
                Menu_pausa = true;
            }
        }
        if (Menu_pausa == true)
        {
            panel_pausa.SetActive(true);
        }
        else
        {
            panel_pausa.SetActive(false);
        }

        
    }
    private void set_tamano_camara()
    {
        /*float tamaño_camara_ = camara.orthographicSize;
        Debug.Log("El tamaño de la camara es de: " + tamaño_camara_);*/
        Resolution resolucion = Screen.currentResolution;
        tamano_camara[0] = resolucion.width; 
        tamano_camara[1] = resolucion.height;
        //Debug.Log("El tamaño de la pantalla es de: " + tamano_camara[0] + " x " + tamano_camara[1]);
        escala_de_las_cosas = obtener_escala();
        if (escala_de_las_cosas != 1)
        {
            Redimencionar_canvas(escala_de_las_cosas);
        }
    }
    private float obtener_escala()
    {
        float porcentaje = regla_de_tres(tamano_camara_original[0], tamano_camara[0], 100, false, true);
        Debug.Log("La pantalla del PC es el " + porcentaje + "% de la pantalla del juego");
        float escala = regla_de_tres(1, porcentaje, 100, false, false);
        Debug.Log("La escala de relacion es de " + escala + " a 1");

        return escala;
    }
    private float regla_de_tres(float numA, float numB, float resulA, bool sup, bool izq)
    {
        float incognita;

        if (sup == true)
        {
            if (izq == true)
            {
                incognita = (resulA * numA) / numB;
            }
            else
            {
                incognita = (numA * numB) / resulA;
            }
        }
        else
        {
            if (izq == true)
            {
                incognita = (resulA * numB) / numA;
            }
            else
            {
                incognita = (numA * numB) / resulA;
            }
        }

        return incognita;
    }
    private void Redimencionar_canvas(float escala_)
    {
        float OrigenX, OrigenY;
        OrigenX = OrigenCanvas.transform.position.x;
        OrigenY = OrigenCanvas.transform.position.y;
        for (int i = 0; i < cosas_del_canvas.Length; i++)
        {
            cosas_del_canvas[i].transform.localScale = new Vector3( cosas_del_canvas[i].transform.localScale.x * escala_, 
                                                                    cosas_del_canvas[i].transform.localScale.y * escala_, 
                                                                    cosas_del_canvas[i].transform.localScale.z * escala_);
            float X = OrigenX + regla_de_tres(1, cosas_del_canvas[i].transform.position.x, escala_, false, true);
            float Y = OrigenY + regla_de_tres(1, cosas_del_canvas[i].transform.position.y, escala_, false, true);
            cosas_del_canvas[i].transform.position = new Vector3(X, Y, cosas_del_canvas[i].transform.position.z);
        }
        tamano_camara_original[0] = tamano_camara[0];
        tamano_camara_original[1] = tamano_camara[1];
    }
    private void setear_objetos_en_mapa(string nivel)
    {
        switch(nivel)
        {
            case "Nivel 1":
                //lo deja todo tal cual esta
                tutorial.SetActive(true);
                break;
            case "Nivel 2":
                a_la_Derecha();
                break;
            case "Nivel 3":
                int op = op_random(2);
                switch (op)
                {
                    case 0:
                        //eligio la derecha por lo que se queda igual
                        break;
                    case 1:
                        a_la_Derecha();
                        break;
                }
                break;
        }
    }

    private void a_la_Derecha()
    {
        //objetivos
        Objetivos_Originales[0].transform.position = new Vector3(Objetivos_Originales[0].transform.position.x + distancia_reflejo(mapa, Objetivos_Originales[0]),
                                                                Objetivos_Originales[0].transform.position.y,
                                                                Objetivos_Originales[0].transform.position.z);
        Objetivos_Originales[1].transform.position = new Vector3(Objetivos_Originales[1].transform.position.x + distancia_reflejo(mapa, Objetivos_Originales[1]),
                                                                Objetivos_Originales[1].transform.position.y,
                                                                Objetivos_Originales[1].transform.position.z);
        Objetivos_Originales[2].transform.position = new Vector3(Objetivos_Originales[2].transform.position.x + distancia_reflejo(mapa, Objetivos_Originales[2]),
                                                                Objetivos_Originales[2].transform.position.y,
                                                                Objetivos_Originales[2].transform.position.z);
        Objetivo_a_la_derecha(Objetivos_Originales[0]);


        //ovulo
        Ovulo.GetComponent<Transform>().position = new Vector3(Ovulo.GetComponent<Transform>().position.x + distancia_reflejo(mapa, Ovulo),
                                                                Ovulo.GetComponent<Transform>().position.y,
                                                                Ovulo.GetComponent<Transform>().position.z);

    }
    private void Objetivo_a_la_derecha(GameObject origen)
    {
        int i = origen.GetComponent<EnemyObjetives>().objetives.Length;

        for (int x = 0; x < i; x++)
        {
            origen.GetComponent<EnemyObjetives>().objetives[x].transform.position = new Vector3(origen.GetComponent<EnemyObjetives>().objetives[x].transform.position.x + distancia_reflejo(mapa, origen.GetComponent<EnemyObjetives>().objetives[x]),
                                                                                                origen.GetComponent<EnemyObjetives>().objetives[x].transform.position.y,
                                                                                                origen.GetComponent<EnemyObjetives>().objetives[x].transform.position.z);
        }

        Objetivo_a_la_derecha(origen.GetComponent<EnemyObjetives>().objetives[0]);
    }

    private float distancia_reflejo(GameObject objeto1, GameObject objeto2)
    {
        float distancia_ = (objeto1.transform.position.x - objeto2.transform.position.x) * 2;

        return distancia_;
    }

    private int op_random(int num)
    {
        return UnityEngine.Random.Range(0, num);
    }
}
