using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gerenciador : MonoBehaviour
{
    public GameObject player1;
    public GameObject player2;
    public GameObject player3;
    public GameObject player4;

    public static bool p1;
    public static bool p2;
    public static bool p3;
    public static bool p4;

    void Start()
    { 
        if (p1)
        {
            player1.SetActive(true);
            player2.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);
        }
        if (p2)
        {
            player2.SetActive(true);
            player1.SetActive(false);
            player3.SetActive(false);
            player4.SetActive(false);
        }
        if (p3)
        {
            player3.SetActive(true);
            player1.SetActive(false);
            player2.SetActive(false);
            player4.SetActive(false);
        }
        if (p4)
        {
            player4.SetActive(true);
            player1.SetActive(false);
            player2.SetActive(false);
            player3.SetActive(false);
        }
    }
    public void Atacar()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Atacar();
    }
    public void Pausa()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Pausa();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SomBotao();
    }
    public void Retorna()
    {
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().Retorna();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SomBotao();
    }
    public void Jogar()
    {
        SceneManager.LoadScene("Jogo", LoadSceneMode.Single);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SomBotao();
    }
    public void VoltaMenu()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SomBotao();
    }
    public void Sair()
    {
        Application.Quit();
        GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().SomBotao();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "cela")
        {
            if (Player.giradireita)
            {
                Player.direita = true;
            }
            if (Player.giraesquerda)
            {
                Player.esquerda = true;
            }
        }
    }
    void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "cela")
        {
            if (Player.giradireita == false)
            {
                Player.direita = false;
            }
            if (Player.giraesquerda == false)
            {
                Player.esquerda = false;
            }
        }
    }
}
