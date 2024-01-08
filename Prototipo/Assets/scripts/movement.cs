using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class movement : MonoBehaviour
{
    public float speed;

    private float speed_original;
        
    public float varVida;

    public float espera;
    public AudioSource Sonido_Disparo;
    public AudioSource rial_disparo;
    [SerializeField] AudioClip moverse;
    private float espera_original;

    public int rest;
    float horizontalM;
    float verticalM;
    public float velocidadRotacion;
    float angulo;
    Vector2 movimiento;
    bool shoot;
    public Transform firePoint;
    public GameObject bulletPrefab;
    public float shootCooldown;

    public int Enemigos_muertos;

    public static movement playerInstance;

    private void Awake()
    {
        playerInstance = this;
    }

    void Start()
    {
        varVida = 100;
        Sonido_Disparo=GetComponent<AudioSource>();
        speed_original = speed;
        espera_original = espera;
        Enemigos_muertos = 0;
    }


    void Update()
    {
        horizontalM = Input.GetAxisRaw("Horizontal");
        verticalM = Input.GetAxisRaw("Vertical");
        shoot = Input.GetKeyDown(KeyCode.Space);

        movimiento = new Vector2(horizontalM, verticalM);
        movimiento.Normalize();
        transform.Translate(movimiento * speed * Time.deltaTime, Space.World);

        if (movimiento != Vector2.zero)
        {
            angulo = Mathf.Atan2(movimiento.y, movimiento.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.Euler(0, 0, angulo), velocidadRotacion * Time.deltaTime);
            //Sonido_Disparo.PlayOneShot(moverse);

            if (Sonido_Disparo.isPlaying == false)
            {
                Sonido_Disparo.clip = moverse;
                Sonido_Disparo.Play();
            }
        }

        else {
            Sonido_Disparo.Pause();
        }

        if (shoot && Time.timeScale > 0)
        {
            Shoot();
        }

        if (varVida > 100)
        {
            varVida = 100;
        }
        //scoretext.text = "Enemigos derrotados:" + score;
        //restantes.text = "Derrota esta candidad de enemigos:" + rest;
        
    }

    void Shoot()
    {
        // Verificar si ha pasado suficiente tiempo desde el último disparo
        if (Time.time >= shootCooldown)
        {
            // Calcular la rotación de la bala basada en la rotación del jugador
            Quaternion bulletRotation = firePoint.rotation;

            // Instanciar la bala y ajustar su rotación
            GameObject _bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);

            _bullet.GetComponent<bullet>().player = this.gameObject;
            
            // Reiniciar el temporizador de disparo
            shootCooldown = Time.time + espera; // Ajusta el valor según la velocidad de disparo deseada
            rial_disparo.Play();
        }
    }

    private void OnTriggerEnter2D(Collider2D npc)
    {
        if (npc.gameObject.CompareTag("enemigo"))
        {
            //npc.GetComponent<enemyScript>().eliminar_cola();
            npc.GetComponent<enemyScript>().vida--;
            //Enemigos_muertos -= 1;
        }
        if (npc.gameObject.CompareTag("vidapower"))
        {
            varVida += 20;
        }
        if (npc.gameObject.CompareTag("speedpower"))
        {
            speed += 20;
            Invoke("Back_speed", 5);
        }
        if (npc.gameObject.CompareTag("vidapower"))
        {
            varVida += 20;
        }
        if (npc.gameObject.CompareTag("shootpower"))
        {
            espera = espera_original * 0.6f;
            Invoke("Back_cooldown", 10);
        }
        if (npc.gameObject.CompareTag("Proteccion_enemigo"))
        {
            Destroy(npc.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D npc)
    {
        if (npc.gameObject.CompareTag("enemigo"))
        {
            //npc.GetComponent<enemyScript>().eliminar_cola();
            npc.gameObject.GetComponent<enemyScript>().vida--;
            varVida -= 10;
            //Enemigos_muertos -= 1;
        }
        if (npc.gameObject.CompareTag("vidapower"))
        {
            varVida += 20;
        }
        if (npc.gameObject.CompareTag("speedpower"))
        {
            speed += 20;
            Invoke("Back_speed", 5);
        }
        if (npc.gameObject.CompareTag("shootpower"))
        {
            espera = espera_original * 0.6f;
            Invoke("Back_cooldown", 10);
        }
        if (npc.gameObject.CompareTag("Proteccion_enemigo"))
        {
            Destroy(npc.gameObject);
        }

        if (npc.gameObject.CompareTag("Boss"))
        {
            varVida -= 10;
        }

        /*if (npc.gameObject.CompareTag("shootpower"))
        {
            espera = 0.1f;
        }
        if (npc.gameObject.CompareTag("speedpower"))
        {
            speed += 50;
        }*/
    }
    private void Back_speed()
    {
        speed = speed_original;
    }
    private void Back_cooldown()
    {
        espera = espera_original;
    }
}
