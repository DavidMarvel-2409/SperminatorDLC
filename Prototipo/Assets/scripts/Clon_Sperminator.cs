using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clon_Sperminator : MonoBehaviour
{
    public GameObject Sperminator;
    void Start()
    {
        
    }


    void Update()
    {
        if (Sperminator == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            transform.position = new Vector3(Sperminator.transform.position.x + 700, Sperminator.transform.position.y, transform.position.z);
            transform.rotation = Sperminator.transform.rotation;
        }
    }
}
