using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{

    // Use this for initialization
    public GameObject boton;
    public Sprite desbloqueado;
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
        for (int i = 1; i <= numNiveles; i++)
        {
            aux = (GameObject)Instantiate(boton, transform);
            aux.GetComponent<LevelButtons>().SetId(i);
            if(i <= GameManager.getGM().getLevel())
            {
                aux.GetComponent<Image>().sprite = desbloqueado;
            }
            aux.GetComponentInChildren<Text>().text = i.ToString() ;
        }
    }
}
