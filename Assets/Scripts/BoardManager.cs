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
    public void Init(float ts, ReadMap rd)
    {
        Board = new Tile[11, 11];
        tilesize = ts;
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

    public void NextRoud()
    {
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
}

