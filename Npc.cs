using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Npc : MonoBehaviour
{
    Animator a;
    AudioSource som;
    NavMeshAgent nav;
    GameObject[] inimigos;

    public float speed;
    public float time;
    public float distancia;
    public float inimigo;

    public int vidas;
    public int quantidade;
    public int contador;
   
    public bool solto;
    public bool passos;
    public bool morreu;
    public bool menu;

    void Start()
    {
        a = GetComponent<Animator>();
        som = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();

        if(menu)
        {
            a.SetBool("Ocioso", false);
        }
        else
        {
            a.SetBool("Ocioso", true);
        }
    }
    void Update()
    {
        if (solto & menu == false & vidas > 0)
        {
            a.SetBool("Ocioso", false);
            som.volume = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume / 2;
            distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

            inimigos = GameObject.FindGameObjectsWithTag("inimigo");
            quantidade = inimigos.Length - 1;
            inimigo = Vector3.Distance(inimigos[contador].transform.position, transform.position);

            if (inimigo <= 12 & inimigos[contador].GetComponent<IA>().vidas > 0)
            {
                SegueInimigo(); 
            }
            else
            {
                if (contador == quantidade)
                {
                    contador = 0;
                }
                else
                {
                    contador += 1;
                    SeguePlayer();

                    if (distancia <= 15 & distancia > 8)
                    {
                        if (passos == false)
                        {
                            passos = true;
                            som.loop = true;
                            som.Play();
                        }
                    }
                    else
                    {
                        passos = false;
                        som.Stop();
                    }
                }
            }
        }
        else if(morreu == false & vidas <= 0)
        {
            som.Stop();
            morreu = true;
            nav.enabled = false;
            a.SetBool("Andar", false);
            a.SetBool("Atacar", false);
            a.SetBool("Correr", false);
            a.SetBool("Morrer", true);
            GetComponent<Collider>().enabled = false;
        }
        else if (menu)
        {
            transform.Rotate(0, 30 * Time.deltaTime, 0);
        }
        if (Player.pausar == true)
        {
            som.Pause();

            if (Player.pausar == false)
            {
                som.Play();
            }
        }
    }
    void SeguePlayer()
    {
        som.pitch = 1;
        nav.speed = 6.5f;
        nav.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        nav.stoppingDistance = 7;

        a.SetBool("Andar", true);
        a.SetBool("Atacar", false);
        a.SetBool("Correr", false);

        if (distancia <= 6)
        {
            nav.speed = 0;
            a.SetBool("Andar", false);
        }
    }
    void SegueInimigo()
    {
        som.pitch = 1.5f;
        nav.speed = 8;
        nav.destination = inimigos[contador].transform.position;
        nav.stoppingDistance = 2;

        a.SetBool("Correr", true);
        a.SetBool("Andar", false);
        a.SetBool("Atacar", false);

        if (inimigo <= 2)
        {
            Atacar();
            som.Stop();
            nav.speed = 0;
            passos = false;
            transform.LookAt(inimigos[contador].transform.position);
        }
    }
    void Atacar()
    {
        a.SetBool("Correr", false);
        a.SetBool("Andar", false);
        a.SetBool("Atacar", true);

        time += Time.deltaTime;

        if (time >= 0.5)
        {
            time = 0;
            inimigos[contador].GetComponent<IA>().vidas -= 1;
        }
    }
}

