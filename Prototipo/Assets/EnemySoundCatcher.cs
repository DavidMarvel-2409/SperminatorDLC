using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySoundCatcher : MonoBehaviour
{
    AudioSource Audio;
    int enemigoCont;

    // Start is called before the first frame update
    void Start()
    {
        Audio = GetComponent<AudioSource>();
        enemigoCont = movement.playerInstance.Enemigos_muertos;
    }

    // Update is called once per frame
    void Update()
    {
        if (enemigoCont <= movement.playerInstance.Enemigos_muertos)
        {
            Audio.Play();
            enemigoCont++;
        }
    }
}
