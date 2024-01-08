using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controlador_pausa : MonoBehaviour
{
    public GameObject _controlador_general;

    public void Reanudar()
    {
        _controlador_general.GetComponent<Controlador_general>().Menu_pausa = false;
        Time.timeScale = 1;
    }
    public void Vover()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("title");
    }
}
