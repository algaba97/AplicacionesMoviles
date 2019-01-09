using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoardManager : MonoBehaviour
{
    
    List<Tile> Board;
    List<BallLogic> pelotas;
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

    public Image NextLevelMenu;
    void Start()
    {
        EnableShoot = true;
    }

    public void Init(float ts, ReadMap rd,LevelManager lm)
    {
        Board = new List<Tile>();
        pelotas = new List<BallLogic>();
        tilesize = ts;
        Debug.Log(tilesize);
        numeroTiles = 0;
        readMap = rd;
        gameManager = GameManager.getGM();
        LM = lm;

       
    }


    //Añadimos un tile al array e incrementamos el contador
    public void addTile( Tile _tile)
    {
        Board.Add( _tile);
        numeroTiles++;
    }
    //miramos cuantos tiles estan desactivados y los sacamos del array y decrementamos el contador
    public void UpdateTiles()
    {
       
        
            for (int i = 0; i < Board.Count; i++)
               
                    if (Board[i] != null && Board[i].CanFall())
                    {
                if (!Board[i].gameObject.activeSelf && Board[i].GetComponent<BlockLogic>().canDestroy() )
                        {

                        
                            Destroy(Board[i].gameObject);
                            Board[i] = null;

                            numeroTiles--;
                        }
                    
                }
        
        if (numeroTiles == 0)
        {
            EnableShoot = false;
            NextLevelMenu.gameObject.SetActive(true);
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

        NextLevelMenu.gameObject.SetActive(false);
        LM.nuevoNivel();
        GameManager.getGM().setLevel(GameManager.getGM().getLevel() + 1);
        readMap.NextLevel(GameManager.getGM().getLevel() );
        Primera.transform.position = new Vector2(0,Primera.transform.position.y);

        Debug.Log("Cargando nuevo mapa");
    }

    public bool Ball(GameObject aux)// para llevar el conteo de bolas y ver si es la primera para guardar la x
    {
        Debug.Log("he llehao");
        contador++;
        pelotas.Remove(aux.GetComponent<BallLogic>());

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
                if (tile.gameObject.GetComponent<BlockLogic>().getY() == 0) tile.gameObject.SetActive(true);// miramos si está en la posición 0 para estar activado
                if (tile.gameObject.GetComponent<BlockLogic>().gameOver()) // la altura del tile mas la mitad de la altura del tile, el 0.01 es por si pierde pixeles unity.
                
                {
                    GameOver();
                }
            }
        }
    }
    public GameObject GetPrimera()
    {
        return Primera;
    }/// <summary>
    /// Destruye la ultima fila, como en Board estan ordenados desde lac reación buscamos el primer tile no nulo y borramos los siguientes que permanezcan
    /// en la misma linea
    /// </summary>
    public void PowerUpDestroyLast()
    {
        float auxy = -1.0f; // nos obliga  ainicializar con cualquier valor
        bool encontrado = false;
        for (int i = 0; i < Board.Count; i++)
        {
            if (Board[i] != null)
            {


                if (!encontrado)
                {
                    encontrado = true;
                    auxy = Board[i].gameObject.GetComponent<BlockLogic>().position.y;
                    Destroy(Board[i].gameObject);
                    Board[i] = null;

                    numeroTiles--;
                }
                else
                {

                    if (Board[i].gameObject.GetComponent<BlockLogic>().position.y == auxy)
                    {
                        Destroy(Board[i].gameObject);
                        Board[i] = null;

                        numeroTiles--;
                    }
                    else
                    {
                        return;

                    }
                }

            }
        }
        UpdateTiles();


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
        Time.timeScale = 1;
        bool aux = EnableShoot;
        EnableShoot = false;
        return aux;
    }
    public void addBall(BallLogic b)
    {
        pelotas.Add(b);
    }
    public void duplicateVel()
    {
        Time.timeScale *= 2.0f;
      
    }
    public void takeDownAllBalls()
    {

         foreach (BallLogic pel in pelotas)
           {
            pel.Stop();
            pel.MoveTo(LM.ballsink.getPosition(),10, true,LM.ballsink.llega);

           }
    }

}

