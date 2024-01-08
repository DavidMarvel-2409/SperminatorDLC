using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collider_PowerUp : MonoBehaviour
{
    public bool coliciono = false;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        coliciono = true;
        Invoke("volver", 0.5f);

        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
    private void volver()
    {
        coliciono = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }
}
