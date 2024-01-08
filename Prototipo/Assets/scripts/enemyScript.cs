using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class enemyScript : MonoBehaviour
{
    // Start is called before the first frame update
    public float moveSpeed;
    Vector2 movimiento;
    float angulo;
    public float velocidadRotacion;

    public string Name_meco;
    //esto es para ver las especificaciones del espermatosoide
    //Bob es el base
    //Dash es el rapido
    //Rex es el fuerte

    public GameObject Ovulo;
    public GameObject Player;

    /*public GameObject[] Objetivos_1_;
    public GameObject[] Objetivos_2_;
    public GameObject[] Objetivos_3_;
    public GameObject[] Objetivos_4_;
    public GameObject[] Objetivos_5_;*/
    public GameObject Objetivo_Auxiliar;

    public GameObject _cola_;

    public GameObject drop;
    public string nombre_drop;

    public int vida;
    public AudioSource Muerte;
    [SerializeField] AudioClip Muricion;
    bool trigger;
    public static enemyScript enemyisntance;

    void Start()
    {
        Muerte = GetComponent<AudioSource>();
    }

    void Update()
    {
        Movimiento(Objetivo_Auxiliar);
        cambiar_angulo(Objetivo_Auxiliar);

        if (distancia_objetivo(Objetivo_Auxiliar) < 10)
        {
            Cambio_de_objetivo(Objetivo_Auxiliar);
        }
        if (vida <= 0)
        {
            if (Muerte.isPlaying == false && trigger == false)
            {
                trigger = true;
            }
            me_muero();
        }

    }

    private float distancia_objetivo(GameObject _Objetivo)
    {
        float dist;
        dist = Vector3.Distance(transform.position, _Objetivo.transform.position);
        return dist;
    }

    private void Movimiento(GameObject Objetivo_)
    {
        //transform.position = Vector3.MoveTowards(transform.position, Objetivo_.transform.position, moveSpeed * Time.deltaTime);
        if (Name_meco == "Bob")
        {
            transform.position += (Objetivo_.transform.position - transform.position).normalized * moveSpeed * Time.deltaTime;
        }
        else if (Name_meco == "Dash")
        {
            transform.position += (Objetivo_.transform.position - transform.position).normalized * moveSpeed * 2 * Time.deltaTime;
        }
        else if (Name_meco == "Rex")
        {
            transform.position += (Objetivo_.transform.position - transform.position).normalized * moveSpeed * 0.7f * Time.deltaTime;
        }
    }

    private void cambiar_angulo(GameObject Objetivo_)
    {
        float anguloR = Mathf.Atan2(transform.position.y - Objetivo_.transform.position.y, transform.position.x - Objetivo_.transform.position.x);
        float anguloG = (180 / Mathf.PI) * anguloR - 90;

        transform.rotation = Quaternion.Euler(0, 0, anguloG - 180);
    }

    private void Cambio_de_objetivo(GameObject _Actual)
    {
        switch (select_op(_Actual.GetComponent<EnemyObjetives>().objetives.Length))
        {
            case 0:
                Objetivo_Auxiliar = _Actual.GetComponent<EnemyObjetives>().objetives[0];
                break;
            case 1:
                Objetivo_Auxiliar = _Actual.GetComponent<EnemyObjetives>().objetives[1];
                break;
            case 2:
                Objetivo_Auxiliar = _Actual.GetComponent<EnemyObjetives>().objetives[2];
                break;
        }
    }

    private void OnCollisionEnter2D(Collision2D uterocolision)
    {
        if (uterocolision.collider.CompareTag("UterPoint"))
        {
            me_muero();
        }
    }
    private void OnTriggerEnter2D(Collider2D uterocolisiondos)
    {
        if (uterocolisiondos.CompareTag("UterPoint"))
        {
            me_muero();
        }
    }

    private int select_op(int i)
    {
        int op = Random.Range(0, i);
        return op;
    }

    public void Droping()
    {
        drop.SetActive(true);
        GameObject Drop_clon = Instantiate(drop, transform.position, Quaternion.identity);
        drop.SetActive(false);
    }
    private void me_muero()
    {
        eliminar_cola();
        Player.GetComponent<movement>().Enemigos_muertos++;
        if (nombre_drop != "Nada")
        {
            Droping();
        }
        
        Destroy(this.gameObject);
    }
    public void eliminar_cola()
    {
        _cola_.GetComponent<Script_Cola>().eliminar_cola();
    }
}

