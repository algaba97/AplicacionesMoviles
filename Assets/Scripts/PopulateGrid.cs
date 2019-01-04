using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopulateGrid : MonoBehaviour
{

    // Use this for initialization
    public GameObject boton;
    public int numNiveles;
    void Start()
    {
        Create();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void Create()
    {
        GameObject aux;
        for (int i = 0; i < numNiveles; i++)
        {
            aux = (GameObject)Instantiate(boton, transform);
            aux.GetComponent<LevelButtons>().SetId(i);
        }
    }
}
