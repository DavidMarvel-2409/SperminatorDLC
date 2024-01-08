using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Proteccion : MonoBehaviour
{
    public GameObject objetivo;
    void Start()
    {
        
    }


    void Update()
    {
        transform.position = objetivo.transform.position;
        transform.rotation = objetivo.transform.rotation;
        if (objetivo == null)
        {
            destruirme();
        }
    }

    private void destruirme()
    {
        Destroy(this.gameObject);
    }
}
