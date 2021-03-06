﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
   private static GameManager GM; // privada para que solo el gamemanager pueda crear la isnancia singletone

    public int levelSelected = 1;

    //Guardado de datos
    DataManager DM;

    
    float tsize = 0.0f;
    float volumen = 0.3f;
    GameObject msgListener;
    int state;// 0:Menú, 1:Juego
    int stateaux;// Guardamos el estado para hacerlo en el lateUpdate
    /// <summary>
    /// 0= Menu 1 = Juego
    /// </summary>
    /// <param name="aux"></param>
    public void setState(int aux)
    {
        stateaux = aux;
    }
    public int getState()
    {
        return state;
    }

    void LateUpdate()
    {
        state = stateaux;
    }
 

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
        levelSelected = DM.getDatos().niveles;

    }
    public int getLeveldatos(int level)
    {
        return DM.getDatos().estrellas[level];
    }
   

  
	// Use this for initialization
	void Awake () {
        if (GM == null)
        {
            setGM(this);
            DontDestroyOnLoad(this);
        }
        else Destroy(this.gameObject);
        stateaux = 1;
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
    public void setLevelRate( int stars)
    {
        DM.addLevelData(levelSelected, stars);
        levelSelected++ ;
    }
    public void setLevel(int l)
    {
        levelSelected = l;
    }
    public int getLevel()
    {
        return levelSelected;
    }
    public int maxLevel()
    {
        return  DM.getDatos().niveles;
    }
    public bool  addRubies(int number)
    {
        bool b;
        b = DM.sumaRubies( number);
        msgListener.SendMessage("updateRubies");
        return b;
    }
    public bool addPowerUp(string name, int number)
    {
        bool b= false;
        if(name == "Hierro")
        {
            b = DM.sumaPUhierro(number);
        }
        else if (name == "Bomba")
        {
            b = DM.sumaPUbomba(number);
        }
        else if (name ==  "Terremoto")
        {
            b = DM.sumaPUTerremoto(number);
        }
        else Debug.Log("NO SE ENCUENTRA POWER UP " + name);
        msgListener.SendMessage("updatePowerUpsText");
        return b;
    }
    public int GetRubies()
    {
        return DM.getDatos().rubies;
    }
    public int getPowerUp(string name)
    {
        int a = -1;
        if (name == "Hierro")
        {
            a =DM.getDatos().puHierro;
        }
       else if (name == "Bomba")
        {
            a = DM.getDatos().puBomba;
        }
        else if (name == "Terremoto")
        {
            a = DM.getDatos().puTerremoto;
        }
        else Debug.Log("NO SE ENCUENTRA POWER UP " + name);
        return a;
    }
    public int getStars()
    {
        int cont = 0;
        foreach ( int star in DM.getDatos().estrellas)
        {
            cont += star;
        }
        return cont;
    }
    public void setRubiesListener(GameObject l)
    {
        msgListener = l;
    }
    public void setVolumen(float v)
    {
        volumen = v;
    }
    public float getVolumen()
    {
        return volumen;
    }
}
