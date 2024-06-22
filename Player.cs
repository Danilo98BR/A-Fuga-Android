using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    Touch []toque = new Touch[2];
    Vector3 vetor;
    
    Animator a;
    AudioSource som;
    AudioSource iten;

    public AudioClip andar;
    public AudioClip botao;
    public AudioClip somvida;
    public AudioClip somchave;

    public GameObject giracamera;
    public GameObject centro;
    public GameObject ponto;
    //public GameObject ponto2;
    public GameObject arma;
    public GameObject cam;
    //public GameObject porta;

    public Canvas hud;
    public Canvas pausa;
    public Canvas venceu;
    public Canvas perdeu;

    public Image temsom;
    public Image semsom;

    public Text vida;
    public Text chave;

    public Slider volume;
    public Slider joydir;

    //public RectTransform rectdireita;
    public RectTransform rectesquerda;

    public static bool pausar;
    public static bool giradireita;
    public static bool giraesquerda;
    public static bool direita;
    public static bool esquerda;
    public static bool parede;

    public bool passos;
    public bool morreu;
    public bool atacar;
    public bool tocar;

    public float speed;
    public float tempo;
    public float ataca;
    public float sensibilidade;
    public float distancia;
    public float distancia1;
    //public float distancia2;
    //public float distancia3;
    //public float distancia4;
    public float angulo;
    public float altura;
    public float escala;
    public float limite;
    public float valor;
    //public float eixoy;
    //public float posy;
    //public float y1;
    //public float y2;
    public float x;
    public float y;

    public static int vidas;
    public static int pontos;

    void Start()
    {
        vidas = 3;
        pontos = 0;
        Time.timeScale = 1;
        pausar = false;
        pausa.enabled = false;
        venceu.enabled = false;
        perdeu.enabled = false;
        chave.text = pontos + "/3";
        a = GetComponent<Animator>();
        som = GetComponent<AudioSource>();
        iten = giracamera.GetComponent<AudioSource>();
        arma.GetComponent<Collider>().enabled = false;
        //player = transform.position;
    }
    void Update()
    {
        vida.text = vidas + "/3";
        som.volume = volume.value;
        iten.volume = volume.value;
        altura = Screen.height;
      
        if (altura == 720)
        {
            //y1 = 235;
            //y2 = 95;
            valor = 80;
            //eixoy = 90;
            //posy = 165;
            limite = 370;

            hud.scaleFactor = 1.37f;
            pausa.scaleFactor = 1.37f;
            perdeu.scaleFactor = 1.37f;
            venceu.scaleFactor = 1.37f;
        }
        else if (altura > 720)
        {
            //y1 = altura * 235 / 720;
            //y2 = altura * 95 / 720;
            valor = altura * 80 / 720;
           //eixoy = altura * 90 / 720;
            //posy = altura * 165 / 720;
            limite = altura * 235 / 720;
            escala = altura * 1.37f / 720;

            hud.scaleFactor = escala;
            pausa.scaleFactor = escala;
            perdeu.scaleFactor = escala;
            venceu.scaleFactor = escala;
        }
        if (volume.value > 0)
        {
            som.enabled = true;
            semsom.enabled = false;
        }
        else
        {
            som.enabled = false;
            semsom.enabled = true;
        }
        if (morreu == false & vidas > 0)
        {
            giradireita = false;
            giraesquerda = false;
            a.SetBool("Atacar", false);
            //rectdireita.localPosition = new Vector2(0, 0);
            rectesquerda.localPosition = new Vector2(0, 0);
            ponto.GetComponent<RectTransform>().localPosition = new Vector2(0, 0);
            centro.transform.rotation = Quaternion.Euler(0, 0, 0);
            //giracamera.transform.rotation = Quaternion.Euler(90, 0, 0);
            giracamera.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            distancia = Vector3.Distance(transform.position, cam.transform.position);

            if (Input.touchCount > 0 & pausar == false)
            {
                toque[0] = Input.GetTouch(0);
                distancia1 = Vector2.Distance(toque[0].position, rectesquerda.position);
                //distancia2 = Vector2.Distance(toque[0].position, rectdireita.position);

                if (distancia1 <= valor)
                {
                    tocar = true;
                    rectesquerda.position = toque[0].position;
                    vetor.x = rectesquerda.localPosition.x;
                    vetor.z = rectesquerda.localPosition.y;
                    angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, -180 + angulo, 0);
                }
                else if (distancia1 > valor & distancia1 <= limite)
                {
                    tocar = true;
                    rectesquerda.localPosition = new Vector2(50, 0);
                    ponto.GetComponent<RectTransform>().position = toque[0].position;
                    vetor.x = ponto.GetComponent<RectTransform>().localPosition.x;
                    vetor.z = ponto.GetComponent<RectTransform>().localPosition.y;
                    angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                    transform.rotation = Quaternion.Euler(0, -180 + angulo, 0);
                    centro.transform.rotation = Quaternion.Euler(0, 0, 90 + -angulo);
                }
                else
                {
                    tocar = false;
                }
                if (Input.touchCount > 1 & joydir.value > 0.5f | Input.touchCount > 1 & joydir.value < 0.5f)
                {
                    toque[1] = Input.GetTouch(1);
                    distancia1 = Vector2.Distance(toque[1].position, rectesquerda.position);

                    if (distancia1 <= valor)
                    {
                        rectesquerda.position = toque[1].position;
                        vetor.x = rectesquerda.localPosition.x;
                        vetor.z = rectesquerda.localPosition.y;
                        angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                    }
                    else if (distancia1 > valor & distancia1 <= limite)
                    {
                        rectesquerda.localPosition = new Vector2(50, 0);
                        ponto.GetComponent<RectTransform>().position = toque[1].position;
                        vetor.x = ponto.GetComponent<RectTransform>().localPosition.x;
                        vetor.z = ponto.GetComponent<RectTransform>().localPosition.y;
                        angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                        transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                        centro.transform.rotation = Quaternion.Euler(0, 0, 90 + -angulo);
                    }
                }
                if (Input.touchCount == 1 & tocar)
                {
                    joydir.value = 0.5f;
                }
                //if (Input.touchCount > 1)
                //{
                //    toque[1] = Input.GetTouch(1);
                //    distancia3 = Vector2.Distance(toque[1].position, rectdireita.position);

                //    if (distancia3 <= eixoy & pausar == false)
                //    {
                //        if (toque[1].position.y <= y1 & toque[1].position.y >= y2)
                //        {
                //            rectdireita.position = new Vector2(toque[1].position.x, posy);
                //        }
                //    }
                //    else if (distancia3 > eixoy & distancia3 <= limite & pausar == false)
                //    {
                //        if (toque[1].position.y <= y1 & toque[1].position.y >= y2)
                //        {
                //            ponto2.GetComponent<RectTransform>().position = toque[1].position;

                //            if (ponto2.GetComponent<RectTransform>().localPosition.x > 0)
                //            {
                //                rectdireita.localPosition = new Vector2(65, 0);
                //            }
                //            else if (ponto2.GetComponent<RectTransform>().localPosition.x < 0)
                //            {
                //                rectdireita.localPosition = new Vector2(-65, 0);
                //            }
                //        }
                //    }
                //}
                //if (distancia2 <= eixoy & pausar == false)
                //{
                //    if (toque[0].position.y <= y1 & toque[0].position.y >= y2)
                //    {
                //        rectdireita.position = new Vector2(toque[0].position.x, posy);
                //    }
                //    if (Input.touchCount > 1)
                //    {
                //        toque[1] = Input.GetTouch(1);
                //        distancia4 = Vector2.Distance(toque[1].position, rectesquerda.position);

                //        if (distancia4 <= valor & pausar == false)
                //        {
                //            rectesquerda.position = toque[1].position;
                //            vetor.x = rectesquerda.localPosition.x;
                //            vetor.z = rectesquerda.localPosition.y;
                //            angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                //            transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                //        }
                //        else if (distancia4 > valor & distancia4 <= limite & pausar == false)
                //        {
                //            rectesquerda.localPosition = new Vector2(50, 0);
                //            ponto.GetComponent<RectTransform>().position = toque[1].position;
                //            vetor.x = ponto.GetComponent<RectTransform>().localPosition.x;
                //            vetor.z = ponto.GetComponent<RectTransform>().localPosition.y;
                //            angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                //            transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                //            centro.transform.rotation = Quaternion.Euler(0, 0, 90 + -angulo);
                //        }
                //    }
                //}
                //else if (distancia2 > eixoy & distancia2 <= limite & pausar == false)
                //{
                //    if (toque[0].position.y <= y1 & toque[0].position.y >= y2)
                //    {
                //        ponto2.GetComponent<RectTransform>().position = toque[0].position;

                //        if (ponto2.GetComponent<RectTransform>().localPosition.x > 0)
                //        {
                //            rectdireita.localPosition = new Vector2(65, 0);
                //        }
                //        else if (ponto2.GetComponent<RectTransform>().localPosition.x < 0)
                //        {
                //            rectdireita.localPosition = new Vector2(-65, 0);
                //        }
                //    }
                //    if (Input.touchCount > 1)
                //    {
                //        toque[1] = Input.GetTouch(1);
                //        distancia4 = Vector2.Distance(toque[1].position, rectesquerda.position);

                //        if (distancia4 <= valor & pausar == false)
                //        {
                //            rectesquerda.position = toque[1].position;
                //            vetor.x = rectesquerda.localPosition.x;
                //            vetor.z = rectesquerda.localPosition.y;
                //            angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                //            transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                //        }
                //        else if (distancia4 > valor & distancia4 <= limite & pausar == false)
                //        {
                //            rectesquerda.localPosition = new Vector2(50, 0);
                //            ponto.GetComponent<RectTransform>().position = toque[1].position;
                //            vetor.x = ponto.GetComponent<RectTransform>().localPosition.x;
                //            vetor.z = ponto.GetComponent<RectTransform>().localPosition.y;
                //            angulo = Mathf.Atan2(vetor.x, vetor.z) * Mathf.Rad2Deg;
                //            transform.rotation = Quaternion.Euler(0, 120 + y + angulo, 0);
                //            centro.transform.rotation = Quaternion.Euler(0, 0, 90 + -angulo);
                //        }
                //    }
                //}
            }
            else
            {
                tocar = false;
                joydir.value = 0.5f;
            }
            if (rectesquerda.localPosition.x > 0 | rectesquerda.localPosition.y > 0 | rectesquerda.localPosition.x < 0 | rectesquerda.localPosition.y < 0 | ponto.GetComponent<RectTransform>().localPosition.x > 0 | ponto.GetComponent<RectTransform>().localPosition.x < 0 | ponto.GetComponent<RectTransform>().localPosition.y > 0 | ponto.GetComponent<RectTransform>().localPosition.y < 0)
            {
                if (atacar == false)
                {
                    som.mute = false;
                    a.SetBool("Correr", true);
                    transform.Translate(0, 0, speed * Time.deltaTime);

                    if (passos == false)
                    {
                        passos = true;
                        som.clip = andar;
                        som.pitch = 1.5f;
                        som.loop = true;
                        som.Play();
                    }
                }
            }
            else
            {
                a.SetBool("Correr", false);
                transform.Translate(0, 0, 0);

                if (passos)
                {
                    som.Stop();
                    passos = false;
                }
            }
            if (joydir.value > 0.5 /*rectdireita.localPosition.x > 0*/)
            {
                if (direita == false)
                {
                    giradireita = true;

                    if (y <= 360)
                    {
                        y += Time.deltaTime * sensibilidade;

                        if (y >= 360)
                        {
                            y = 0;
                        }
                    }
                }
                else if (parede == false)
                {
                    cam.transform.Translate(0, 0, 8 * Time.deltaTime);

                    if (y <= 360)
                    {
                        y += Time.deltaTime * sensibilidade;

                        if (y >= 360)
                        {
                            y = 0;
                        }
                    }
                }
                //if (direita == false & distancia < 8.27)
                //{
                //    cam.transform.Translate(0, 0, -5 * Time.deltaTime);
                //}
            }
            if (joydir.value < 0.5 /*rectdireita.localPosition.x < 0*/)
            {
                if (esquerda == false)
                {
                    giraesquerda = true;

                    if (y >= -360)
                    {
                        y -= Time.deltaTime * sensibilidade;

                        if (y <= -360)
                        {
                            y = 0;
                        }
                    }
                }
                else if (parede == false)
                {
                    cam.transform.Translate(0, 0, 8 * Time.deltaTime);

                    if (y >= -360)
                    {
                        y -= Time.deltaTime * sensibilidade;

                        if (y <= -360)
                        {
                            y = 0;
                        }
                    }
                }
                //if (esquerda == false & distancia < 8.27)
                //{
                //    cam.transform.Translate(0, 0, -5 * Time.deltaTime);
                //}
            }
            if (distancia < 8.27 & rectesquerda.localPosition.x > 0 | distancia < 8.27 & rectesquerda.localPosition.x < 0 |
                distancia < 8.27 & rectesquerda.localPosition.y > 0 | distancia < 8.27 & rectesquerda.localPosition.y < 0)
            {
                cam.transform.Translate(0, 0, -5 * Time.deltaTime);
            }
            //if (rectdireita.localPosition.y > 0 & rectdireita.localPosition.y > rectdireita.localPosition.x)
            //{
            //    if (x < 45)
            //    {
            //        x += Time.deltaTime * sensibilidade;
            //    }
            //}
            //if (rectdireita.localPosition.y < 0 & rectdireita.localPosition.y < rectdireita.localPosition.x)
            //{
            //    if (x < -45)
            //    {
            //        x -= Time.deltaTime * sensibilidade;
            //    }
            //}
        }
        else if (morreu == false & vidas <= 0)
        {
            som.Stop();
            pausar = true;
            pausa.enabled = false;
            a.SetBool("Atacar", false);
            a.SetBool("Correr", false);
            a.SetBool("Morrer", true);
            GetComponent<Collider>().enabled = false;
            rectesquerda.localPosition = new Vector2(0, 0);
            //rectdireita.localPosition = new Vector2(0, 0);

            tempo += Time.deltaTime;

            if (tempo >= 2)
            {
                tempo = 0;
                morreu = true;
                perdeu.enabled = true;
                Time.timeScale = 0;
            }
        }
        else if(vidas <= 0)
        {
            joydir.value = 0.5f;
        }
        if (atacar)
        {
            som.mute = true;
            a.SetBool("Atacar", true);
            a.SetBool("Correr", false);
            ataca += Time.deltaTime;

            if (ataca >= 0.5)
            {
                arma.GetComponent<Collider>().enabled = true;

                if (ataca >= 1)
                {
                    ataca = 0;
                    atacar = false;
                    arma.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
    public void Atacar()
    {
        atacar = true;
    }
    public void Pausa()
    {
        if (vidas > 0)
        {
            Time.timeScale = 0;
            pausar = true;
            som.Stop();
            pausa.enabled = true;
        }
    }
    public void Retorna()
    {
        Time.timeScale = 1;
        pausar = false;
        som.Play();
        pausa.enabled = false;
    }
    public void SomBotao()
    {
        som.clip = botao;
        som.pitch = 1.5f;
        som.loop = false;
        som.Play();
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "chave")
        {
            pontos++;
            chave.text = pontos + "/3";
            iten.clip = somchave;
            iten.loop = false;
            iten.Play();

            if (pontos == 3)
            {
                pausar = true;
                venceu.enabled = true;
                Time.timeScale = 0;
            }
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cela")
        {
            parede = true;
        }
        if (other.tag == "vida")
        {
            iten.clip = somvida;
            iten.loop = false;
            iten.Play();

            if (vidas < 3)
            {
                vidas++;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cela")
        {
            parede = false;
        }
    }
}
//if (other.gameObject.tag == "cela")
//{
//    dentro = true;
//}
//void OnTriggerExit(Collider other)
//{
//    if (other.gameObject.tag == "cela")
//    {
//        dentro = false;
//    }
//}


