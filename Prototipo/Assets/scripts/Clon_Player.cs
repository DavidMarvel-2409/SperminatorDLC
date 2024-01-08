using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clon_Player : MonoBehaviour
{
    public GameObject player;
    private Vector3 coor;
    public void Start()
    {
        
    }
    public void Update()
    {
        coor = new Vector3(player.transform.position.x + 700, player.transform.position.y, transform.position.z);

        transform.rotation = player.transform.rotation;
        transform.position = coor;
    }
}
