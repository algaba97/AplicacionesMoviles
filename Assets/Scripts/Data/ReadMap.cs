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
    public Transform CanvasAbajo;
    public Transform CanvasArriba;
    public Transform CanvasDerecha;
    public Transform CanvasIzquierda;
    //public RectTransform CanvasAbajo; //Para ajustar el tamaño de los canvas, no me apaño con lo nativo de unity
    public RectTransform CanvasA; //Para ajustar el tamaño de los canvas, no me apaño con lo nativo de unity
    public RectTransform CanvasB; //Para ajustar el tamaño de los canvas, no me apaño con lo nativo de unity
    public Camera cam;


    float tilesize;
    float width;
    float height;
    float margenY;
    float margenX;
    // Use this for initialization
    void Start () {
      
	}

    public void Init(LevelManager aux)

    {
        LM = aux;
        gamemanager = GameManager.getGM();
        Debug.Log("Empezamos a leer");
        Mapa = new int[tam, tam];
        Mapa2 = new int[tam, tam];
        ReadFile(GameManager.getGM().getLevel());
        instantiateMap();
        

    }

    // Update is called once per frame
    void Update () {
        
	}
    /// <summary>
    /// Instanciamos el mapa previamente leido, margenes y colocamos los distitntos elementos del juego que encesiten colocación
    /// </summary>
    void instantiateMap(){
        width = Camera.main.orthographicSize * (float)Screen.width / (float)Screen.height; 
        height = Camera.main.orthographicSize ;
        bool v = true;

       tilesize = (float)2 * width / (float)tam;


        if (tilesize * 17.0f > (height * 2.0f))
        {
            Debug.Log("entra");
            tilesize = height * 2 / 17.0f;
        }
    
       

        //calculamos el espacio que sobra arriba y abajo, los margenes
        
        margenY =( height*2.0f -(tilesize *13.0f)) /2.0f;
        margenX = (width*2.0f - (tilesize * 11)) / 2.0f;

        //Colocamos los bordes(estéticos) y el canvas inferior y superior 
        CanvasAbajo.localScale += new Vector3(width * 2.0f, margenY, 0.0f);
        CanvasAbajo.SetPositionAndRotation(new Vector3(0, -height + CanvasAbajo.localScale.y / 2.0f, -1.0f), new Quaternion(0, 0, 0, 0));

        CanvasArriba.localScale += new Vector3(width * 2.0f, margenY, 0.0f);
        CanvasArriba.SetPositionAndRotation(new Vector3(0, +height - CanvasArriba.localScale.y / 2.0f, -1.0f), new Quaternion(0, 0, 0, 0));

        CanvasDerecha.localScale += new Vector3(margenX, height*2, 0.0f);
        CanvasDerecha.SetPositionAndRotation(new Vector3(+width -CanvasDerecha.localScale.x/2.0f, 0.0f, -1.0f), new Quaternion(0, 0, 0, 0));

        CanvasIzquierda.localScale += new Vector3(margenX, height * 2, 0.0f);
        CanvasIzquierda.SetPositionAndRotation(new Vector3(-width + CanvasDerecha.localScale.x / 2.0f, 0.0f, -1.0f), new Quaternion(0, 0, 0, 0));



        float y2 = cam.WorldToScreenPoint(new Vector3(0.0f, margenY, 0.0f)).y/ Camera.main.orthographicSize ;
        Debug.Log("y2");
        Debug.Log(y2);
   
        CanvasA.offsetMin = new Vector2(0, 0);
        CanvasA.offsetMax = new Vector2(0, -((float)Screen.height -y2));
        Debug.Log(Screen.height);
        Debug.Log(Camera.main.orthographicSize);

        CanvasB.offsetMin = new Vector2(0, (float)Screen.height -y2);
        CanvasB.offsetMax = new Vector2(0, 0);

        //Colocamos la deadzone
        BoxCollider2D deadzone = LM.deadZone.gameObject.GetComponent<BoxCollider2D>();
        deadzone.size= new Vector2(width/6.0f,  margenY); // width/6.0f se ajusta al tamño de la pantalla
        deadzone.offset = new Vector2(0, -height +margenY/2.0f ); //importante la altura del gameobject porqué afecta

        gamemanager.SetTSize(tilesize);

        //colocamos los bordes fisicos
        GameObject aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(-width +margenX-0.25f , 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.position = new Vector3(width -margenX + 0.25f, 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.Rotate(new Vector3(0, 0,1), -90);
        aux2.transform.position = new Vector3(0, height -margenY +0.25f, 0);
  
        //mandamos la posicion al leevlmanager para que lo tuilicen los demás componentes
        LM.SetPosition( margenY - height);

        InstanceMap();

    }

    void InstanceMap()
    {


        for (int i = 0; i < tam; i++)
        {
            for (int j = 0; j < tam; j++)
            {

                if (Mapa[i, j] == 1)
                {

                    Tile aux = Instantiate(bloque);

                    aux.gameObject.transform.localScale = new Vector3(tilesize, tilesize, aux.transform.localScale.z);
                    aux.gameObject.transform.position = new Vector3(
                        j * tilesize + (-width + tilesize / 2.0f) + margenX,
                                        (-i * tilesize) + (height - tilesize / 2.0f) - margenY - tilesize,
                                        0);

                    aux.gameObject.GetComponent<BlockLogic>().setVida(Mapa2[i, j]);
                    aux.gameObject.GetComponent<BlockLogic>().Init(LM);
                    gamemanager.AddCubo(j, i, aux);
                }
            }

        }
    }
    

    
    void ReadFile(int number)
    {
        string path = "Assets/Mapas/mapdata" + number +".txt";
       // string path = "Assets/Mapas/test.txt";
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
                

                if (mapa2[i * tam + j].Length >= 2 && (mapa2[i * tam + j][mapa2[i * tam + j].Length - 2] == '.'))// EWL string tiene al forma XXXXXXXX.0 y le borramos la final
                {
                    mapa2[i * tam + j] = mapa2[i * tam + j].Substring(0, mapa2[i * tam + j].Length - 2);
                }


                try {
                    Mapa[i, j] = Convert.ToInt32(mapa2[i * tam + j]);
                        }
                catch
                {
                    Debug.Log("Error " +mapa2[i * tam + j]);
                }

            }
        }
     
        for (int i = 0; i < tam; i++)
        {
           // Mapa2[i, j] = (int)Char.GetNumericValue(mapa2[tam * tam - 1 + i * tam + j][0]);
            for (int j = 0; j < tam; j++)
            {
               

                if (mapa2[tam * tam - 1 + i * tam + j].Length >= 2 && mapa2[tam * tam - 1 + i * tam + j][mapa2[tam * tam - 1 + i * tam + j].Length - 1] == '.') // EL string tiene la forma XXXXXXXXX. y le borramos el final
                {
                    mapa2[tam * tam - 1 + i * tam + j] = mapa2[tam * tam - 1 + i * tam + j].Substring(0, mapa2[tam * tam - 1 + i * tam + j].Length - 1);
                }


                try
                {
                    Mapa2[i, j] = Convert.ToInt32(mapa2[tam * tam - 1 + i * tam + j]);
                 
                }
                catch
                {
                    Debug.Log("Error " + mapa2[tam * tam - 1 + i * tam + j]);
                }
            }
        }

        //convertir maps2 en dos matrices  
     

    }
    public void NextLevel(int number)
    {
        ReadFile(number);
        InstanceMap();

    }

}
