using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DialogosControler : MonoBehaviour
{

    public string[] Dialogo;
    public TextMeshProUGUI texto;
    private int index_text = 0;
    public GameObject pantalla_dialogo;
    public GameObject cara2;
    public GameObject[] Pantallas_fuera_del_jefe;
    public GameObject[] Imagenes_de_fondo;

    public GameObject nave;
    private string nivel;

    void Start()
    {
        texto.text = Dialogo[index_text];
        nivel = SceneManager.GetActiveScene().name;
    }


    void Update()
    {
        texto.text = Dialogo[index_text];
    }

    public void cambiar_dialogo()
    {
        if (index_text < Dialogo.Length - 1)
        {
            index_text++;
        }
        else
        {
            Time.timeScale = 1;
            pantalla_dialogo.SetActive(false);
        }
        if (index_text ==  Dialogo.Length - 1)
        {
            if (nivel == "Nivel 3")
            {
                cara2.SetActive(true);
                nave.SetActive(true);
            }
            else if (nivel == "acercade")
            {
                if (Imagenes_de_fondo[0].name == "noticias")
                {
                    Imagenes_de_fondo[0].SetActive(false);
                    Imagenes_de_fondo[1].SetActive(true);
                    Pantallas_fuera_del_jefe[1].SetActive(true);
                    Pantallas_fuera_del_jefe[0].SetActive(false);
                }
                else if (Imagenes_de_fondo[0].name == "noticiasdos")
                {
                    SceneManager.LoadScene("title");
                }
            }else if (nivel == "finalscene")
            {
                SceneManager.LoadScene("title");
            }

        }
    }
}
