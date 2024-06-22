using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    AudioSource botao;

    public GameObject menu;
    public GameObject jogar;
    public GameObject tutorial;
    public GameObject creditos;

    public GameObject p1;
    public GameObject p2;
    public GameObject p3;
    public GameObject p4;

    public Image som;
    public Image semsom;
    public Image controle;
    public Image selecao;

    public RectTransform player1;
    public RectTransform player2;
    public RectTransform player3;
    public RectTransform player4;

    public Camera cam;
    public Slider volume;
   
    public bool iniciar;

    public float altura;
    public float escala;

    void Start()
    {
        Time.timeScale = 1;
        menu.SetActive(true);
        jogar.SetActive(false);
        tutorial.SetActive(false);
        creditos.SetActive(false);
        semsom.enabled = false;
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        botao = GetComponent<AudioSource>();
    }
    void Update()
    {
        botao.volume = volume.value;
        cam.GetComponent<AudioSource>().volume = volume.value / 8;
        altura = Screen.height;

        if (altura == 720)
        {
            GetComponent<Canvas>().scaleFactor = 1.37f;
        }
        else if (altura > 720)
        {
            escala = altura * 1.37f / 720;
            GetComponent<Canvas>().scaleFactor = escala;
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
        if (iniciar)
        {
            volume.value -= Time.deltaTime;

            if (cam.GetComponent<AudioSource>().volume == 0)
            {
                iniciar = false;
                SceneManager.LoadScene("Jogo", LoadSceneMode.Single);
            }
        }
    }
    public void Iniciar()
    {
        SomBotao();
        Selecao1();
        menu.SetActive(false);
        jogar.SetActive(true);
        tutorial.SetActive(false);
    }
    public void Tutorial()
    {
        SomBotao();
        tutorial.SetActive(true);
        jogar.SetActive(false);
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
    }
    public void Creditos()
    {
        SomBotao();
        creditos.SetActive(true);
        menu.SetActive(false);
    }
    public void VoltaMenu()
    {
        SomBotao();
        menu.SetActive(true);
        jogar.SetActive(false);
        tutorial.SetActive(false);
        creditos.SetActive(false);
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
    }
    public void Sair()
    {
        SomBotao();
        Application.Quit();
    }
    public void Selecao1()
    {
        SomBotao();
        p1.SetActive(true);
        p2.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        Gerenciador.p1 = true;
        Gerenciador.p2 = false;
        Gerenciador.p3 = false;
        Gerenciador.p4 = false;
        selecao.rectTransform.position = player1.position;
    }
    public void Selecao2()
    {
        SomBotao();
        p2.SetActive(true);
        p1.SetActive(false);
        p3.SetActive(false);
        p4.SetActive(false);
        Gerenciador.p2 = true;
        Gerenciador.p1 = false;
        Gerenciador.p3 = false;
        Gerenciador.p4 = false;
        selecao.rectTransform.position = player2.transform.position;
    }
    public void Selecao3()
    {
        SomBotao();       
        p3.SetActive(true);
        p1.SetActive(false);
        p2.SetActive(false);
        p4.SetActive(false);
        Gerenciador.p3 = true;
        Gerenciador.p1 = false;
        Gerenciador.p2 = false;
        Gerenciador.p4 = false;
        selecao.rectTransform.position = player3.transform.position;
    }
    public void Selecao4()
    {
        SomBotao();
        p4.SetActive(true);
        p1.SetActive(false);
        p2.SetActive(false);
        p3.SetActive(false);
        Gerenciador.p4 = true;
        Gerenciador.p1 = false;
        Gerenciador.p2 = false;
        Gerenciador.p3 = false;
        selecao.rectTransform.position = player4.transform.position;
    }
    public void SomBotao()
    {
        botao.Play();
        botao.pitch = 1.5f;
    }
    public void Jogo()
    {
        iniciar = true;
    }
}
