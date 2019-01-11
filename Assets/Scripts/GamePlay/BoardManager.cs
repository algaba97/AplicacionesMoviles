﻿using System;
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

    public int nBolas = 50;
    public int bolasAMeter = 0;
    int contador = 0;
    bool fBola = true;


    bool EnableShoot;// Variable que controla si puedes disparar o no

    public Image NextLevelMenu;
    public RectTransform panelJuego;
    public RectTransform panelPU;

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
            gameManager.setState(0);
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
       

        NextLevelMenu.gameObject.SetActive(false);
        LM.nuevoNivel();
        readMap.NextLevel(gameManager.getLevel() );
        gameManager.setState(1);
        EnableShoot = true;
        Debug.Log("Cargando nuevo mapa");
    }

    public bool Ball(GameObject aux)// para llevar el conteo de bolas y ver si es la primera para guardar la x
    {

        contador++;
        pelotas.Remove(aux.GetComponent<BallLogic>());

        if (fBola)
        {

            fBola = false;

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
        nBolas += bolasAMeter;
        bolasAMeter = 0;
        tilesize = ts;
        //Primero calculamos la posición en la cual si toca el tile sería game over

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
        TogglePanels();
        GetComponent<PUHierro>().removePowerUp();
    }
  /// <summary>
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
        
        if (LM.deadZone.getAuxBall() != null)
        {
            return LM.deadZone.getAuxBall().transform.position.x;
        }
        else return 0;
    }
    public bool roundIsEnd()
    {
        bool aux = EnableShoot;
        if (aux) {
            Time.timeScale = 1;
            TogglePanels();
        }
        EnableShoot = false;
        return aux;
    }
    public void addBall(BallLogic b)
    {
        pelotas.Add(b);
    }
    public void duplicateVel()
    {
        if (Time.timeScale == 1.0f)
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
    void TogglePanels()
    {
        panelPU.gameObject.SetActive( !panelPU.gameObject.activeSelf);
        panelJuego.gameObject.SetActive( !panelJuego.gameObject.activeSelf);
    }

}

