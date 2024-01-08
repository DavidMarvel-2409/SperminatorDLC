using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class escenebutton : MonoBehaviour
{
    public void scenetransition(string nombreEscena)
    {
        SceneManager.LoadScene(nombreEscena);
    }


}
