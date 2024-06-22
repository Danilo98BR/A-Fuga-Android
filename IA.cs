using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA : MonoBehaviour
{
    Animator a;
    AudioSource som;
    NavMeshAgent nav;
    GameObject[] inimigos;

    public GameObject[] pontos;
    public GameObject ponto;
    public GameObject cano;
    public GameObject bala;
    public GameObject vida;
    
    public AudioClip passo;
    public AudioClip tiro;

    public float time;
    public float tempo;
    public float inimigo;
    public float distancia;
    public float distanciaponto;

    public int quantidade;
    public int qdtpontos;
    public int contador;
    public int numero;
    public int valor;
    public int vidas;
 
    public bool menu;
    public bool troca;
    public bool morreu;
    public bool passos;
    public bool player;
    public bool persegue;

    //float pontoinicio;
    //float posicao;
    //Vector3 vetor;
    //bool vendo;

    void Start()
    {
        //vetor = transform.position;
        a = GetComponent<Animator>();
        som = GetComponent<AudioSource>();
        nav = GetComponent<NavMeshAgent>();
        pontos = GameObject.FindGameObjectsWithTag("ponto");
        qdtpontos = pontos.Length; 
        valor = Random.Range(0, qdtpontos);
    }
    void Update()
    {
        if (menu == false & vidas > 0)
        {
            som.volume = GameObject.FindGameObjectWithTag("Player").GetComponent<AudioSource>().volume / 2;
            distancia = Vector3.Distance(GameObject.FindGameObjectWithTag("Player").transform.position, transform.position);

            inimigos = GameObject.FindGameObjectsWithTag("npc");
            quantidade = inimigos.Length - 1;
            inimigo = Vector3.Distance(inimigos[contador].transform.position, transform.position);

            if (inimigo <= 15 & inimigos[contador].GetComponent<Npc>().solto == true & inimigos[contador].GetComponent<Npc>().vidas > 0)
            {
                SegueNpc();
            }
            else
            {
                if (contador == quantidade)
                {
                    contador = 0;
                }
                else if(Player.vidas > 0)
                {
                    contador += 1;
                    Patrulha();
                }
                else
                {
                    nav.enabled = false;
                    a.SetBool("Andar", false);
                }
                if (distancia <= 15 & Player.vidas > 0)
                {
                    SeguePlayer();

                    if (passos == false)
                    {
                        passos = true;
                        som.clip = passo;
                        som.pitch = 1.5f;
                        som.loop = true;
                        som.Play();
                    }
                }
                else
                {
                    som.Stop();
                    passos = false;
                    a.SetBool("Atacar", false);
                    a.SetBool("Correr", false);
                }
            }
        }
        else if (morreu == false & vidas <= 0)
        {
            som.Stop();
            nav.enabled = false;
            a.SetBool("Andar", false);
            a.SetBool("Atacar", false);
            a.SetBool("Correr", false);
            a.SetBool("Morrer", true);
            GetComponent<Collider>().enabled = false;

            tempo += Time.deltaTime;

            if (tempo >= 2)
            {
                tempo = 0;
                numero = Random.Range(1, 3);

                if (numero == 2)
                {
                    morreu = true;
                    Instantiate(vida, ponto.transform.position, ponto.transform.rotation);
                }
                else
                {
                    morreu = true;
                }
            }
        }
        else if (menu)
        {
            Patrulha();
        }
        if (Player.pausar == true)
        {
            som.Pause();

            if (Player.pausar == false)
            {
                som.Play();
            }
        }
        if (distancia > 15)
        {
            som.mute = true;
        }
        else
        {
            som.mute = false;
        }
    }
    void Patrulha()
    {
        player = false;
        a.SetBool("Andar", true);
        a.SetBool("Atacar", false);
        a.SetBool("Correr", false);

        nav.speed = 6.5f;
        distanciaponto = Vector3.Distance(transform.position, pontos[valor].transform.position);

        if(persegue == false)
        {
            nav.destination = pontos[valor].transform.position;

            if (troca)
            {
                troca = false;
                valor = Random.Range(0, qdtpontos);
            }
            else if (distanciaponto <= 8)
            {
                troca = true;
                a.SetBool("Andar", false);
            }
        }
        else
        {
            nav.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        }
    }
    void SeguePlayer()
    {
        player = true;
        som.loop = false;
        a.SetBool("Correr", true);
        a.SetBool("Andar", false);
        a.SetBool("Atacar", false);

        nav.speed = 8;
        nav.destination = GameObject.FindGameObjectWithTag("Player").transform.position;
        nav.stoppingDistance = 9;

        if (distancia <= 8)
        {
            Atirar();
            nav.speed = 0;
            transform.LookAt(GameObject.FindGameObjectWithTag("Player").transform.position);
        }
    }
    void SegueNpc()
    {
        player = false;
        a.SetBool("Correr", true);
        a.SetBool("Andar", false);
        a.SetBool("Atacar", false);

        nav.speed = 8;
        nav.destination = inimigos[contador].transform.position;
        nav.stoppingDistance = 9;

        if (inimigo <= 8)
        {
            Atirar();
            nav.speed = 0;
            transform.LookAt(inimigos[contador].transform.position);
        }
    }
    void Atirar()
    {        
        a.SetBool("Atacar", true);
        a.SetBool("Correr", false);

        time += Time.deltaTime;

        if(time >= 0.5)
        {
            som.Stop();

            if (time >= 1.1)
            {
                time = 0;
                som.clip = tiro;
                som.pitch = 1.5f;
                som.Play();
                Instantiate(bala, cano.transform.position, cano.transform.rotation);

                if (player)
                {
                    Player.vidas -= 1;
                }
                else
                {
                    inimigos[contador].GetComponent<Npc>().vidas -= 1;
                }
            }
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "arma")
        {
            if(vidas > 2)
            {
                vidas -= 2;
            }
            else
            {
                vidas--;
            }
        }
    }
}
//pontoinicio = Vector3.Distance(transform.position, vetor);
//posicao = Vector3.Distance(player.transform.position, vetor);

//if (distancia <= 15)
//{
//    speed = 3;
//    transform.LookAt(player.transform.position);
//}
//if (distancia <= 15 & Player.dentro == false)
//{
//    vendo = true;
//    speed = 3;
//    transform.LookAt(player.transform.position);
//    a.SetBool("Andar", true);
//}
//if (pontoinicio >= 10)
//{
//    speed = 0;
//    a.SetBool("Andar", false);
//}
//if (pontoinicio >= 10 & posicao <= 10)
//{
//    speed = 3;
//    transform.LookAt(player.transform.position);
//    a.SetBool("Andar", true);
//}
//if (distancia >= 15 & vendo == true)
//{
//    speed = 3;
//    transform.LookAt(vetor);
//    a.SetBool("Andar", true);

//    if (pontoinicio < 0.1)
//    {
//        speed = 0;
//        a.SetBool("Andar", false);
//    }






