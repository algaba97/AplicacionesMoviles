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

    public int nBolas = 50;
    public int bolasAMeter = 0;
    int contador = 0;
    bool fBola = true;


    bool EnableShoot;// Variable que controla si puedes disparar o no

    public Image NextLevelMenu;
    public Image gameOverMenu;
    public RectTransform panelJuego;
    public RectTransform panelPU;

    //linea de danger
    GameObject danger;

    void Start()
    {
        EnableShoot = true;
    }
   
    public void SetDanger(GameObject aux)
    {
        danger = aux;
        SetActivateDanger(false);
    }
    
    public void SetActivateDanger(bool aux)
    {
        danger.SetActive(aux);
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
    public void addTile( Tile _tile, bool sumar)
    {
        Board.Add( _tile);
        if (sumar)
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
            limpiarTablero();
            cleanBalls();


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

        gameOverMenu.gameObject.SetActive(true);
        limpiarTablero();
        cleanBalls();
        gameManager.setState(0);



    }
    public void NextLevel(bool over)//SI OVER == TRUE, GAMEOVER
    {

        if (over)
        {
            gameOverMenu.gameObject.SetActive(false);


        }
        else
        {
            NextLevelMenu.gameObject.SetActive(false);
        }
        LM.nuevoNivel(over);
        readMap.NextLevel(gameManager.getLevel() );
        gameManager.setState(1);
        EnableShoot = true;
        Debug.Log("Cargando nuevo mapa");
    }
    void limpiarTablero()
    {
        for (int i = 0; i < Board.Count; i++)
        {
            if (Board[i] != null)
            {
                Destroy(Board[i].gameObject);
                Board[i] = null;
            }
        }
        numeroTiles = 0;
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
        bool lineDanger = false;
        foreach (Tile tile in Board)
        {
            if (tile != null && tile.CanFall())
            {
             
                tile.gameObject.transform.position = new Vector3(tile.gameObject.transform.position.x, 
                    tile.gameObject.transform.position.y - tilesize,
                    tile.gameObject.transform.position.z);
                int   y = tile.gameObject.GetComponent<BlockLogic>().getY();
                if (y == 0) tile.gameObject.SetActive(true);// miramos si está en la posición 0 para estar activado
                else if (y == 11) lineDanger = true;

                if (tile.gameObject.GetComponent<BlockLogic>().gameOver()) 
                {
                    GameOver();
                }
            }
        }
        SetActivateDanger(lineDanger);
        TogglePanels();
        GetComponent<PUHierro>().removePowerUp();
    }
  /// <summary>
    /// Destruye la ultima fila, como en Board estan ordenados desde lac reación buscamos el primer tile no nulo y borramos los siguientes que permanezcan
    /// en la misma linea
    /// </summary>
    public void PowerUpDestroyLast()
    {
        if (GameManager.getGM().addPowerUp("Bomba", -1))
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
        SetActivateDanger(false);
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
    public void cleanBalls()
    {
        for (int i = 0; i < pelotas.Count; i++)
        {
            if (pelotas[i] != null)
            {
                Destroy(pelotas[i].gameObject);
                pelotas[i] = null;
            }
        }
    }
    void TogglePanels()
    {
        panelPU.gameObject.SetActive( !panelPU.gameObject.activeSelf);
        panelJuego.gameObject.SetActive( !panelJuego.gameObject.activeSelf);
    }

}

