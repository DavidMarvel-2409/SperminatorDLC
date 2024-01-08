using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clon_Meco : MonoBehaviour
{
    public GameObject meco;
    private Vector3 coor;
    void Start()
    {
        
    }

    void Update()
    {
        if (meco == null)
        {
            Destroy(this.gameObject);
        }
        else
        {
            coor = new Vector3(meco.transform.position.x + 700, meco.transform.position.y, transform.position.z);
            transform.position = coor;
        }
    }
}
