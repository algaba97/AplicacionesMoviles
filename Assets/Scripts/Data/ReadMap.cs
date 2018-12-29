using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ReadMap : MonoBehaviour {
   public int[,] Mapa;
    public int[,] Mapa2;
    public int tam = 11;
    public Tile bloque;
    public GameObject Pared;
    GameManager gamemanager;
    LevelManager LM;
    public RectTransform CanvasAbajo; //Para ajustar el tamaño de los canvas, no me apaño con lo nativo de unity
    public RectTransform CanvasArriba; //Para ajustar el tamaño de los canvas, no me apaño con lo nativo de unity
    public Camera cam;

    // Use this for initialization
    void Start () {
        gamemanager = GameManager.getGM();
        Debug.Log("Empezamos a leer");
        Mapa = new int[tam, tam];
        Mapa2 = new int[tam,tam];
        ReadFile(1);
        instantiateMap();
	}

    public void Init(LevelManager aux)
    {
        LM = aux;

    }

    // Update is called once per frame
    void Update () {
        
	}
    void instantiateMap(){
        var width = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height; 
        var height = Camera.main.orthographicSize ;
        bool v = true;

        float tilesize =  (float)2 * width / (float)tam;
        if (((float)2 * height / ((float)tam + 1)) < tilesize)
        {
            tilesize = (float)2 * height / ((float)tam + 1); //Decidimos el tamañod el tile dependiendo de la pantalla
            v = false; // para indicar que no es vertical, lo usaremos en el siguiente paso
            Debug.Log("Soy Horizontal");
        }

       
        if (tilesize * 16.0f > height*2) tilesize = height*2/ 16.0f;
        

        //calculamos el espacio que sobra arriba y abajo aka margenes
        float margenY;
        float margenX;
        margenY =( height*2 -(tilesize *14));
        margenX = (width*2 - (tilesize * 11)) / 2.0f;
        Debug.Log(margenY);

        float y2 = cam.WorldToScreenPoint(new Vector3(0, margenY, 0)).y/ Camera.main.orthographicSize;
    
        CanvasAbajo.offsetMin = new Vector2(0,0);
        CanvasAbajo.offsetMax = new Vector2(0,-((float)Screen.height  -y2));

        CanvasArriba.offsetMin = new Vector2(0, (float)Screen.height -y2);
        CanvasArriba.offsetMax = new Vector2(0, 0);





        gamemanager.SetTSize(tilesize);

        GameObject aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(-width -0.25f , 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(width + 0.25f, 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.Rotate(new Vector3(0, 0,1), -90);
        aux2.transform.position = new Vector3(0, height +0.25f, 0);


        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {
                if (Mapa[i, j] == 1)
                {

                    Tile aux = Instantiate(bloque);

                    aux.gameObject.transform.localScale = new Vector3(tilesize, tilesize, aux.transform.localScale.z);
                    aux.gameObject.transform.position = new Vector3(
                        j * tilesize + (-width + tilesize / 2.0f) +margenX,
                        (-i * tilesize) + (height - tilesize / 2.0f) -margenY,
                        0);
                    aux.gameObject.GetComponent<BlockLogic>().setVida(Mapa2[i, j] );
                    gamemanager.AddCubo(j,i,aux);
                }
            }

        }

    }

    void ReadFile(int number)
    {
        // string path = "Assets/Mapas/mapdata" + number +".txt";
        string path = "Assets/Mapas/test.txt";
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
