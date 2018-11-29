using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadMap : MonoBehaviour {
   public int[,] Mapa;
    public int[,] Mapa2;
    public int tam = 11;
    public GameObject bloque;
    public GameObject Pared;

	// Use this for initialization
	void Start () {
        Debug.Log("Empezamos a leer");
        Mapa = new int[tam, tam];
        Mapa2 = new int[tam,tam];
        ReadFile(1);
        instantiateMap();
	}
	
	// Update is called once per frame
	void Update () {
        
	}
    void instantiateMap(){
        var width = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height; //TODO a esto habria que añadirle los margenes en caso de que no fuera vertical
        var height = Camera.main.orthographicSize ;


        float tilesize =  (float)2 * width / (float)tam;
        GameManager.GM.SetTSize(tilesize);

        GameObject aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(-width , 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(width, 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.Rotate(new Vector3(0, 0,1), -90);
        aux2.transform.position = new Vector3(0, height, 0);


        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {
                if (Mapa[i, j] == 1)
                {

                    GameObject aux = Instantiate(bloque);

                    aux.transform.localScale = new Vector3(tilesize, tilesize, aux.transform.localScale.z);
                    aux.transform.position = new Vector3(j * tilesize + (-width + tilesize / 2.0f), (-i * tilesize) + (height - tilesize / 2.0f), 0);
                    aux.GetComponent<BlockLogic>().setVida(Mapa2[i, j] );
                    GameManager.GM.AddCubo(aux);
                }
            }

        }

    }

    void ReadFile(int number)
    {
        string path = "Assets/Mapas/mapdata" + number +".txt";
        string mapa = "";

        //Read the text from directly from the test.txt file
        using (StreamReader reader = new StreamReader(path))
        {
            while (reader.Peek() > -1)
            {
                string linea = reader.ReadLine();
                if (linea == "data=")//buscamos cuando empieza la matriz
                {
                   
                    for(int i = 0; i < tam; i++)
                    {
                        mapa +=reader.ReadLine();
                        //Debug.Log(reader.ReadLine());
                    }

                  

                    
                }
            }

        }
        string[] mapa2 = mapa.Split(',');
        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {
                Mapa[i, j] = (int)Char.GetNumericValue(mapa2[i*tam +j][0]);

            }
        }
        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {
               Mapa2[i, j] = (int)Char.GetNumericValue(mapa2[tam *tam -1 + i * tam + j][0]);
            }
        }

        //convertir maps2 en dos matrices  
     

    }
}
