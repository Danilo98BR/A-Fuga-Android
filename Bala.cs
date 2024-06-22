using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    void Update()
    {
        transform.Translate(0, 0, 50 * Time.deltaTime);
        Destroy(gameObject, 0.5f);
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "npc" | collision.gameObject.tag == "Player" |
            collision.gameObject.tag == "inimigo")
        {
            Destroy(gameObject);
        }
    }
    void OnTriggEnter(Collider other)
    {
        if (other.gameObject.tag == "cela")
        {
            Destroy(gameObject);
        }
    }
}
