using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jefe : MonoBehaviour
{
    // Start is called before the first frame update
    public float ImpulsePower;
    public float BackPower;
    
    public float stoppingDistance;
    public float retreatDistance;
    
    private float timeBtwShots;
    public float startTimeBtwShots;

    public float distancia_seguimiento;
    
    public float vida = 100;
    public Transform FPEnemigo;
    public GameObject Player;
    public GameObject EBPrefab;

    public GameObject Objetivo;
    private GameObject Objetivo_aux;
    private GameObject Objetivo_anterior;
    public GameObject Objetivo_central;

    private Vector2 coor_boss;
    private Vector2 coor_player;
    private Vector2 coor_objective;

    public float speed_boss;

    Rigidbody2D rb;

    void Start()
    {
        //StartCoroutine(disparar());
        Player = GameObject.FindGameObjectWithTag("Player");
        timeBtwShots = startTimeBtwShots;
        rb = GetComponent<Rigidbody2D>();
        Objetivo_aux = Objetivo;
        Objetivo_anterior = Player;
    }

    // Update is called once per frame
    void Update()
    {
        coor_boss = new Vector2(transform.position.x, transform.position.y);
        coor_player = new Vector2(Player.transform.position.x, Player.transform.position.y);
        coor_objective = new Vector2(Objetivo.transform.position.x, Objetivo.transform.position.y);

        if (Vector3.Distance(coor_player, coor_boss) < 50)
        {
            Objetivo = Player;
        }
        else
        {
            if (Objetivo == Player)
            {
                Objetivo = Objetivo_central;
            }
            if (Vector2.Distance(coor_objective, coor_boss) < 5)
            {
                cambio_de_objetivo();
            }
        }



        Movimiento(objtener_angulo(coor_boss, coor_objective));

        if (timeBtwShots <= 0)
        {
            Instantiate(EBPrefab, FPEnemigo.position, transform.rotation);
            timeBtwShots = startTimeBtwShots;
        }
        
        else
        {
            timeBtwShots -= Time.deltaTime;
        }
        transform.position = coor_boss;
    }

    private void OnTriggerEnter2D(Collider2D npc)
    {
        if (npc.gameObject.CompareTag("bala"))
        {
            vida -= 5;
            if (vida<=0)
            {
            Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bala"))
        {
            vida -= 5;
            if (vida<=0)
            {
                Player.GetComponent<movement>().Enemigos_muertos+= 100;
                Destroy(gameObject);
            }
        }
    }

    private void cambio_de_objetivo()
    {
        GameObject aux;
        int x;
        do
        {
            x = select_random(Objetivo.GetComponent<Objetivos_boss>().objetivo.Length);
            aux = Objetivo.GetComponent<Objetivos_boss>().objetivo[x];
        } while (aux == Objetivo_anterior);

        Objetivo = aux;
        coor_objective = new Vector2(Objetivo.transform.position.x, Objetivo.transform.position.y);
    }

    private int select_random(int num)
    {
        int a = UnityEngine.Random.Range(0, num);
        return a;
    }

    private void Movimiento(float angle)
    {
        coor_boss.x += (speed_boss * Mathf.Cos(angle)) * Time.deltaTime;
        coor_boss.y += (speed_boss * Mathf.Sin(angle)) * Time.deltaTime;
        transform.rotation = Quaternion.AngleAxis(angle-90, Vector3.forward);
    }

    private float objtener_angulo(Vector3 ob1, Vector3 ob2)
    {
        Vector3 _vector = ob2 - ob1;

        float angle = Mathf.Atan2(_vector.y, _vector.x) * Mathf.Rad2Deg;
        return angle;
    }

}
