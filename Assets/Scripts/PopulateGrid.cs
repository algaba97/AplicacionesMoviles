using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PopulateGrid : MonoBehaviour
{

    // Use this for initialization
    public GameObject boton;
    GameManager GM;
    public Sprite desbloqueado0;
    public Sprite desbloqueado1;
    public Sprite desbloqueado2;
    public Sprite desbloqueado3;
    public int numNiveles;
    void Start()
    {
        GM = GameManager.getGM();
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
            if(i <= GM.getLevel())
            {

                switch (GM.getLeveldatos(i-1))
                {
                    case 0:
                        aux.GetComponent<Image>().sprite = desbloqueado0;
                        break;
                    case 1:
                        aux.GetComponent<Image>().sprite = desbloqueado1;
                        break;
                    case 2:
                        aux.GetComponent<Image>().sprite = desbloqueado2;
                        break;
                    case 3:
                        aux.GetComponent<Image>().sprite = desbloqueado3;
                        break;


                }
                    
            }
            aux.GetComponentInChildren<Text>().text = i.ToString() ;
        }
    }
}
