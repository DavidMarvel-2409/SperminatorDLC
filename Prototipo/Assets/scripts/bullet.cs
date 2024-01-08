using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class bullet : MonoBehaviour
{
    //private Rigidbody2D MyRb;
    public float speed;
    private float time;
    public int score;
    public int rest;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        score = 0;
        rest = 10;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.up * speed * Time.deltaTime);
        time += Time.deltaTime;
        
        if (time > 1)
        {
            Destroy(gameObject);
        }
    }
    private void OnTriggerEnter2D(Collider2D npc)
    {
        if (npc.gameObject.CompareTag("enemigo"))
        {
            //player.GetComponent<movement>().Enemigos_muertos++;
            npc.gameObject.GetComponent<enemyScript>().vida -= 1;
            /*npc.gameObject.GetComponent<enemyScript>().Droping();
            Destroy(npc.gameObject); // Elimina el enemigo*/
            Destroy(gameObject); // Elimina la bala

            // No es necesario incrementar el puntaje aquí, ya que el jugador lo hace
        }
    }

    private void OnCollisionEnter2D(Collision2D npc)
    {
        if (npc.gameObject.CompareTag("enemigo"))
        {
            //player.GetComponent<movement>().Enemigos_muertos++;
            npc.gameObject.GetComponent<enemyScript>().vida -= 1;
            /*npc.gameObject.GetComponent<enemyScript>().Droping();
            Destroy(npc.gameObject); // Elimina el enemigo*/
            Destroy(gameObject); // Elimina la bala


            // No es necesario incrementar el puntaje aquí, ya que el jugador lo hace
        }
    }

}
