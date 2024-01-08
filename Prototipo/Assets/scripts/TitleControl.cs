using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleControl : MonoBehaviour
{
    private float time;
    public string direct;
    public float speed;
    private Vector3 coor;
    private void Start()
    {
        time = 0;
        coor = transform.position;
        Time.timeScale = 1;
    }
    private void Update()
    {
        time += Time.deltaTime;
        mover();
        if (time > 0.5f)
        {
            Cambio_direct();
            time = 0;
        }
    }

    private void mover()
    {
        switch (direct)
        {
            case "up":
                coor.y += speed * Time.deltaTime;
                break;
            case "down":
                coor.y -= speed * Time.deltaTime;
                break;
        }
        transform.position = coor;
    }

    private void Cambio_direct()
    {
        switch (direct)
        {
            case "up":
                direct = "down";
                break;
            case "down":
                direct = "up";
                break;
        }
    }
}
