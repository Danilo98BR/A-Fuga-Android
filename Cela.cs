using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cela : MonoBehaviour
{
    AudioSource grade;
    GameObject npc;

    public GameObject[] npcs;
    public GameObject cela;
    public GameObject cadeado;
    public Transform respawn;

    public bool temchave;
    bool tocou;
    bool abriu;
    //bool fechado;

    public float time;
    int valor;

    void Start()
    {
        grade = GetComponent<AudioSource>();

        if(temchave == false)
        {
            valor = Random.Range(1, 5);

            if (valor == 1)
            {
                npc = Instantiate(npcs[0], respawn.transform.position, respawn.transform.rotation);
            }
            else if (valor == 2)
            {
                npc = Instantiate(npcs[1], respawn.transform.position, respawn.transform.rotation);
            }
            else if (valor == 3)
            {
                npc = Instantiate(npcs[2], respawn.transform.position, respawn.transform.rotation);
            }
            else if (valor == 4)
            {
                npc = Instantiate(npcs[3], respawn.transform.position, respawn.transform.rotation);
            }
        }
    }
    void FixedUpdate()
    {
        if (tocou & abriu == false)
        {
            time += Time.deltaTime;
            grade.volume = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume;

            if (time >= 0.2)
            {
                cadeado.GetComponent<Rigidbody>().useGravity = true;
                cadeado.GetComponent<Rigidbody>().isKinematic = false;
                cadeado.GetComponent<Collider>().isTrigger = false;
            }
            else
            {
                grade.Play();
                cadeado.GetComponent<AudioSource>().Play();
                cadeado.GetComponent<Animator>().SetBool("Cadeado", true);
            }
            if (time >= 0.5)
            {
                cela.GetComponent<Animator>().SetBool("Abrir", true);

                if (time >= 1)
                {
                    if(temchave == false)
                    {
                        time = 0;
                        abriu = true;
                        npc.GetComponent<Npc>().solto = true;
                    }
                    else
                    {
                        time = 0;
                        abriu = true;
                    }
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player" /*& abriu == false & fechado == false*/)
        {
            tocou = true;
        }
        //if (collision.gameObject.tag == "Player" & abriu == true)
        //{
        //    fechado = true;
        //    GetComponent<Animator>().SetBool("Abrir", false);
        //}
    }
    //void OnCollisionExit(Collision collision)
    //{
    //    if (collision.gameObject.tag == "Player")
    //    {
    //        abriu = true;
    //    }
    //    if (collision.gameObject.tag == "Player" & fechado == true)
    //    {
    //        abriu = false;
    //        fechado = false;
    //    }
    //}
}




