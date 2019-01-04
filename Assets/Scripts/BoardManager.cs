using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    Tile[,] Board;
    float tilesize;
    int numeroTiles;
    ReadMap readMap;
    GameManager gameManager;

    GameObject Primera;//primera bola que cae
    public int nBolas = 50;
    int contador = 0;
    bool fBola = true;
    float iniX = 0.0f;

    public void Init(float ts, ReadMap rd)
    {
        Board = new Tile[11, 11];
        tilesize = ts;
        Debug.Log(tilesize);
        numeroTiles = 0;
        readMap = rd;
        gameManager = GameManager.getGM();
        gameManager.SetBoard(this);

       
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
        if (numeroTiles != 0)
        {
            for (int i = 0; i < Board.GetLength(0); i++)
                for (int j = 0; j < Board.GetLength(1); j++)
                {
                    if (Board[i, j] != null && Board[i, j].CanFall())
                    {
                        if (!Board[i, j].gameObject.activeSelf)
                        {
                            Destroy(Board[i, j]);
                            Board[i, j] = null;

                            numeroTiles--;
                        }
                    }
                }
        }
        if (numeroTiles == 0)
        {
            Debug.Log("SIguiente nivelo loko");
        }
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

        foreach(Tile tile in Board)
        {
            if (tile != null && tile.CanFall())
            {

                tile.gameObject.transform.position = new Vector3(tile.gameObject.transform.position.x, 
                    tile.gameObject.transform.position.y - tilesize,
                    tile.gameObject.transform.position.z);
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
}

