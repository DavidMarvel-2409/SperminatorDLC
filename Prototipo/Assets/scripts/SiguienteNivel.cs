using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SiguienteNivel : MonoBehaviour
{

    public string escena;
    private void Start()
    {
        if (this.gameObject.name == "Salir")
        {
            escena = "Title";
        }
        else
        {
            if (SceneManager.GetActiveScene().name == "Nivel 1")
            {
                escena = "Cinematica 2";
            }
            else if (SceneManager.GetActiveScene().name == "Nivel 2")
            {
                escena = "Cinematica 3";
            }
            else if (SceneManager.GetActiveScene().name == "Nivel 3")
            {
                escena = "finalscene";
            }
        }
    }
    public void Cambio()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(escena);
    }

}
