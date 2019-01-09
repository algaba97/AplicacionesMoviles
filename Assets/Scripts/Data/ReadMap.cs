using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
/// <summary>
///Script que lee e instancia un mapa desde un txt desde la carpeta "Assets/Resources/Mapas",
///el GameManager es el que sabe que nivel es el que se va a jugar, y aqui se lo pedimos.
/// </summary>
public class ReadMap : MonoBehaviour {
   public int[,] Mapa;
    public int[,] Mapa2;
    public int tam = 11;
    int filas;
    public Tile bloque;
    public GameObject Pared;
    GameManager gamemanager;
    LevelManager LM;
    
   
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


        if (tilesize * 18.0f > (height * 2.0f))
        {
            Debug.Log("entra");
            tilesize = height * 2 / 18.0f;
        }
    
       

        //calculamos el espacio que sobra arriba y abajo, los margenes
        
        margenY =( height*2.0f -(tilesize *14.0f)) /2.0f;
        margenX = (width*2.0f - (tilesize * 11)) / 2.0f;

      

    
        //Colocamos la deadzone
        BoxCollider2D deadzone = LM.deadZone.gameObject.GetComponent<BoxCollider2D>();
        deadzone.size= new Vector2(width*2,  margenY); // width/6.0f se ajusta al tamño de la pantalla
        deadzone.offset = new Vector2(0, -height +margenY/2.0f ); //importante la altura del gameobject porqué afecta

        gamemanager.SetTSize(tilesize);

        //colocamos los bordes fisicos
        GameObject aux2 = Instantiate(Pared);
        float scale = 1;
        aux2.transform.localScale = new Vector3(1,tilesize*14,scale);
        aux2.transform.position = new Vector3(-width+margenX-scale/2, 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.localScale = new Vector3(1, tilesize * 14, scale);
 
        aux2.transform.position = new Vector3(width -margenX + scale/2, 0, 0);
        aux2 = Instantiate(Pared);
        aux2.transform.localScale = new Vector3(1, tilesize * 15, scale);
        aux2.transform.Rotate(new Vector3(0, 0,1), -90);
        aux2.transform.position = new Vector3(0, height -margenY +scale/2, 0);
  
        //mandamos la posicion de la Bola al leevlmanager para que lo tuilicen los demás componentes
        LM.SetPosition(  -7 * tilesize);

        InstanceMap();
       

    }

    void InstanceMap()
    {


        for (int i = filas-1; i >= 0; i--)
        {
            for (int j = tam-1; j >= 0; j--)
            {

                if (Mapa[i, j] != 0)
                {

                    Tile aux = Instantiate(bloque);

                    aux.gameObject.transform.localScale = new Vector3(tilesize, tilesize, aux.transform.localScale.z);
                    aux.gameObject.transform.position = new Vector3(
                        (j) * tilesize + (-width + tilesize / 2.0f) + margenX,
                                        (-(i - (filas - tam)) * tilesize) + (height - tilesize / 2.0f) - margenY - tilesize,
                                        0);
                    
                    aux.gameObject.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("Bloques/game_img_block" + Mapa[i, j] + "_1");
                    aux.gameObject.AddComponent<PolygonCollider2D>();
                    if (Mapa2[i, j] != 0)
                    {
                        aux.gameObject.GetComponent<BlockLogic>().setVida(Mapa2[i, j]);
                    }

                    aux.gameObject.GetComponent<BlockLogic>().Init(LM,i-(filas-tam),j);
                    //si i-(filas-tam) < 0 no deberiá pitnarse nada, lo deasctivamos y lo volveremos a activar en su momento
                    if(i-(filas-tam) <0)
                    {
                        aux.gameObject.SetActive(false);
                    }
                    LM.getBM().addTile(aux); 

                }
            }

        }
    }
    

    
    void ReadFile(int number)
    {
        TextAsset text = (TextAsset) Resources.Load("Mapas/mapdata"+ number, typeof(TextAsset));
        //  string path = "Assets/Mapas/mapdata" + number +".txt";
        // string path = "Assets/Mapas/test.txt";
        string p = text.text.Replace('\r', ' ');
        string[] path = text.text.Split('\n');
        string mapa = "";

        //Read the text from directly from the test.txt file
        int index = 2;


        do
        {
            index++;
            mapa += path[index];
        } while (!path[index].Contains("."));

         filas = index - 2;

        Mapa = new int[filas, tam];
        Mapa2 = new int[filas, tam];

        

        string[] mapa2 = mapa.Split(',');
        for (int i = 0; i < filas; i++)
        {
            for (int j = 0; j <tam; j++)
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
        mapa = "";
        index += 3;
        do
        {
            index++;
            mapa += path[index];
        } while (!path[index].Contains("."));

         mapa2 = mapa.Split(',');
        for (int i = 0; i < filas; i++)
        {
           // Mapa2[i, j] = (int)Char.GetNumericValue(mapa2[tam * tam - 1 + i * tam + j][0]);
            for (int j = 0; j < tam; j++)
            {
               

                if (mapa2[ i * tam + j].Length >= 2 && mapa2[i * tam + j][mapa2 [i * tam + j].Length - 1] == '.') // EL string tiene la forma XXXXXXXXX. y le borramos el final
                {
                    mapa2[ i * tam + j] = mapa2[ i * tam + j].Substring(0, mapa2[ i * tam + j].Length - 1);
                }


                try
                {
                    Mapa2[i, j] = Convert.ToInt32(mapa2[ i * tam + j]);
                 
                }
                catch
                {
                    Debug.Log("Error " + mapa2[ i * tam + j]);
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
