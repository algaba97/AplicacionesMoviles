﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
   private static GameManager GM; // privada para que solo el gamemanager pueda crear la isnancia singletone

    public int levelSelected = 1;

    //Guardado de datos
    DataManager DM;
    DataJuego dataJuego;

 
 
    float tsize = 0.0f;

    public void SetTSize(float aux)
    {
        tsize = aux;
    }

  
    public float getTilesize()
    {
        return tsize;
    }
   
   public void setDM(DataManager aux)
    {
        DM = aux;
        dataJuego = DM.getDatos();
        levelSelected = dataJuego.levels;
        Debug.Log("AAAAAAAAAAA");

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
        DM.addLevelData(l, 3);
    }
    public int getLevel()
    {
        return levelSelected;
    }
}
