using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Conejo : MonoBehaviour
{
    
    //Declaro una variable publica llamada Points y otra privada llamada DestPoints
    public Transform[] points;
    private int destPoint = 0;
    
    //Declaro el NavMeshAgent que voy a usar
    private NavMeshAgent agent;
   
    //Declaro la variable para las animaciones
    private Animator animator;
    


    //Declaro un Gameobject llamado caja
    private GameObject caja;


    void Start () {
        
        //Llamo a las variables que hemos declarado antes
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        GetComponent<BoxCollider>();

        //Desactivo el autoBraking para que no se ralentice al llegar al punto.
        agent.autoBraking = false;

        //Llamo al void GotoNextPoint
        GotoNextPoint();
    }


    void GotoNextPoint() {
        //Return, si no se ha puesto ningún punto
        if (points.Length == 0)
            return;

        //Le digo al agente que vaya al siguiente punto
        agent.destination = points[destPoint].position;

        //Elijo el siguiente punto, se reinicia el ciclo si es necesario
        destPoint = (destPoint + 1) % points.Length;
    }
    void Update () {
        //Elige el nuevo punto cuando se acerca al actual
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            GotoNextPoint();
    }
    }

