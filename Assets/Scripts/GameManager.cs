using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
   private static GameManager GM; // privada para que solo el gamemanager pueda crear la isnancia singletone
   
    

   public  BoardManager boardManager;
    float tsize = 0.0f;

    public void SetTSize(float aux)
    {
        tsize = aux;
    }

    public void SetBoard(BoardManager bm)
    {
        boardManager = bm;
    }
    public float getTilesize()
    {
        return tsize;
    }
    public void AddCubo(int x, int y, Tile tile)//Añadir cubo a la lista
    {
        if (boardManager != null)
        {
            boardManager.addTile(x,y,tile);
        }
    }
   
   

  
	// Use this for initialization
	void Awake () {
        setGM(this);
  
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public static GameManager getGM()
    {
        return GM;
    }

   private void setGM(GameManager gm)
    {
        GM = gm;
    }
}
