using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public GameObject guarda;
    GameObject copia;
    public float distancia;
    public float distanciaponto;
    public int limite;
    bool instancia;

    void Update()
    {
        distancia = Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position);

        if (instancia == false & distancia > distanciaponto)
        {
            instancia = true;
            copia = Instantiate(guarda, transform.position, transform.rotation);
        }
        else if (instancia)
        {
            if (/*Player.pontos < 2 & */copia.GetComponent<IA>().morreu == true & distancia > distanciaponto & limite > 0)
            {
                limite -= 1;
                copia = Instantiate(guarda, transform.position, transform.rotation);
            }
            //else if(Player.pontos == 2 & distancia > distanciaponto & limite == 0)
            //{
            //    limite -= 1;
            //    copia = Instantiate(guarda, transform.position, transform.rotation);
            //    copia.GetComponent<IA>().persegue = true;
            //}
        }
    }       
}
