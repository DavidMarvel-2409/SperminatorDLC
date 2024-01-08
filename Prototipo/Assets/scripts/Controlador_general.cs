using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Controlador_general : MonoBehaviour
{
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
        //Debug.Log("" + SceneManager.GetActiveScene().name);
    }
    private void Update()
    {

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
