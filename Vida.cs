using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vida : MonoBehaviour
{
    float time;
    int giro = 2;
    bool girar = true;

    void Update()
    {
        if(Player.pausar == false)
        {
            if (girar)
            {
                transform.Rotate(0, giro, 0);
                time += Time.deltaTime;

                if (time >= 2)
                {
                    time = 0;
                    giro += 3;
                }
                if (giro >= 15)
                {
                    Destroy(gameObject);
                }
            }
            else
            {
                transform.LookAt(Camera.main.transform.position);
                transform.Translate(20 * Time.deltaTime, 15 * Time.deltaTime, 0);

                if (transform.position.y >= 6)
                {
                    Destroy(gameObject);
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" & Player.vidas < 4)
        {
            girar = false;
            GetComponent<Collider>().enabled = false;
        }
    }
}
