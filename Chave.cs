using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chave : MonoBehaviour
{
    bool girar = true;

    void Update()
    {
        if(Player.pausar == false)
        {
            if (girar)
            {
                transform.Rotate(0, 8, 0);
            }
            else
            {
                transform.LookAt(Camera.main.transform.position);
                transform.Translate(-4 * Time.deltaTime, 4 * Time.deltaTime, 4 * Time.deltaTime);

                if (transform.position.y >= 4)
                {
                    Destroy(gameObject);
                }
            }
        }      
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            girar = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
