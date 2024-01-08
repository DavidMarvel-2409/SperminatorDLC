using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;
                                        //basicamente, mueve la cola.
public class Script_Cola : MonoBehaviour
{
    private float time;
    public GameObject cabeza;
    void Start()
    {
        time = 0;
    }

    void Update()
    {
        time += Time.deltaTime;
        if (time > 0.2f)
        {
            time = 0;
            transform.localScale = new Vector3(transform.localScale.x * -1, transform.localScale.y, transform.localScale.z);
        }
        if (cabeza == null)
        {
            Destroy(this.gameObject);
            return;
        }
        transform.position = cabeza.transform.position;
        transform.rotation = cabeza.transform.rotation;
        
    }
    public void eliminar_cola()
    {
        Destroy(this.gameObject);
    }
}
