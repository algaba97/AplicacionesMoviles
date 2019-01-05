using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    Tile[,] Board;
    float tilesize;
   public int numeroTiles;
    ReadMap readMap;
    GameManager gameManager;
    LevelManager LM;

    GameObject Primera;//primera bola que cae
    public int nBolas = 50;
    int contador = 0;
    bool fBola = true;
    float iniX = 0.0f;

    bool EnableShoot;// Variable que controla si puedes disparar o no

    public Canvas NextLevelMenu;
    void Start()
    {
        EnableShoot = true;
    }

    public void Init(float ts, ReadMap rd,LevelManager lm)
    {
        Board = new Tile[11, 11];
        tilesize = ts;
        Debug.Log(tilesize);
        numeroTiles = 0;
        readMap = rd;
        gameManager = GameManager.getGM();
        gameManager.SetBoard(this);
        LM = lm;

       
    }


    //Añadimos un tile al array e incrementamos el contador
    public void addTile(int x, int y, Tile _tile)
    {
        Board[x, y] = _tile;
        numeroTiles++;
    }
    //miramos cuantos tiles estan desactivados y los sacamos del array y decrementamos el contador
    public void UpdateTiles()
    {
       
        
            for (int i = 0; i < Board.GetLength(0); i++)
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] != null && Board[i, j].CanFall())
                    {
                        if (!Board[i, j].gameObject.activeSelf)
                        {

                        
                            Destroy(Board[i, j].gameObject);
                            Board[i, j] = null;

                            numeroTiles--;
                        }
                    }
                }
        
        if (numeroTiles == 0)
        {
            EnableShoot = false;
            NextLevelMenu.enabled = true;
            Debug.Log("SIguiente nivelo loko");
        }
        else
        {
            EnableShoot = true;
        }
    }
    void GameOver()
    {
        Debug.Log("GameOver");
      
           

    }
    public void NextLevel()
    {
        EnableShoot = true;
        NextLevelMenu.enabled = false;
        readMap.NextLevel(2);
        Primera.transform.position = new Vector2(0,Primera.transform.position.y);

        Debug.Log("Cargando nuevo mapa");
    }

    public bool Ball(GameObject aux)// para llevar el conteo de bolas y ver si es la primera para guardar la x
    {
        contador++;
        
        if (fBola)
        {

            fBola = false;
            iniX = aux.transform.position.x;
            Primera = aux;
            return true;
        }
        else
        {

            if (contador >= nBolas)
            {
                UpdateTiles();
                NextRound(gameManager.getTilesize());
                contador = 0;
                fBola = true;
            }

            return false;
        }

    }

    public void NextRound(float ts)
    {
        tilesize = ts;
        //Primero calculamos la posición en la cual si toca el tile sería game over
        float posy = LM.GetPosition(); // la posicion del margeny
        foreach (Tile tile in Board)
        {
            if (tile != null && tile.CanFall())
            {
             
                tile.gameObject.transform.position = new Vector3(tile.gameObject.transform.position.x, 
                    tile.gameObject.transform.position.y - tilesize,
                    tile.gameObject.transform.position.z);
                if (tile.gameObject.transform.position.y - tile.gameObject.transform.localScale.y / 2.0f - 0.01f <= posy) // la altura del tile mas la mitad de la altura del tile, el 0.01 es por si pierde pixeles unity.
                {
                    GameOver();
                }
            }
        }
    }
    public GameObject GetPrimera()
    {
        return Primera;
    }
    public float GetPosBola()
    {
        if (Primera != null)
        {
            return Primera.transform.position.x;
        }
        else return 0;
    }
    public bool roundIsEnd()
    {
        bool aux = EnableShoot;
        EnableShoot = false;
        return aux;
    }
}

