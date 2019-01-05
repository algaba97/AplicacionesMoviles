using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
   private static GameManager GM; // privada para que solo el gamemanager pueda crear la isnancia singletone
    int levelSelected = 1;
    

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
        if (GM == null)
        {
            setGM(this);
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
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
    public void setLevel(int l)
    {
        levelSelected = l;
    }
    public int getLevel()
    {
        return levelSelected;
    }
}
