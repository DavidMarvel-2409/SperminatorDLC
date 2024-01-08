using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class camerascript : MonoBehaviour
{

    public GameObject personaje;
    public float sumaY;
    public float sumaX;

    Vector3 posicionC;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        posicionC = new Vector3(personaje.transform.position.x,personaje.transform.position.y, -10);
        transform.position = posicionC;


        /*if (personaje.transform.position.x < 3.289065 && personaje.transform.position.x > -94.44431)
        {
            transform.position = new Vector3(personaje.transform.position.x + sumaX, personaje.transform.position.y + sumaY, -10);
        }*/
        
    }
}
