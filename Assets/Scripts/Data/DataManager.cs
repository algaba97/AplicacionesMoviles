﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;

[Serializable]
public class DataJuego
{
    public int rubies;
    public int extratiros;
    public List< int> level;
    public int levels;
}

public class DataManager : MonoBehaviour
{
    DataJuego datos;
    GameManager Gm;

    void Start()
    {
        DontDestroyOnLoad(this);
        ReadData();
        Gm = GameManager.getGM();
        Gm.setDM(this);
        
    }
    public DataJuego getDatos() {
        return datos;
    }


   
    public void ReadData()
    {
        Debug.Log(Application.dataPath);
        string filePath = Application.dataPath + "/Resources/Mapas/data";
        if (File.Exists(filePath))
        {
            Debug.Log("Layendo datos");
            string dataAsJson = File.ReadAllText(filePath);
            byte[] data = Convert.FromBase64String(dataAsJson);
            string result = System.Text.Encoding.UTF8.GetString(data);
            datos =JsonUtility.FromJson<DataJuego>(result);

        }
        else
        {
            Debug.Log("creando datos");
            datos = new DataJuego();
          
            datos.rubies = 50;
            datos.extratiros = 0;
            datos.level = new List<int>(3000);
            datos.level.Add(1);
            datos.levels = datos.level.Count;
            SaveData();

        }
    }
   public  void SaveData()
    {
        //Log string to console window
        var json = JsonUtility.ToJson(datos);
        string filePath = Application.dataPath + "/Resources/Mapas/data";

        // TextAsset text = (TextAsset)Resources.Load("/Scenes/data.json", typeof(TextAsset));
        byte[] json2 = System.Text.Encoding.UTF8.GetBytes(json);
        string data = Convert.ToBase64String(json2);
        
        File.WriteAllText(filePath, data);
      
    }

    public void setRubies(int value)
    {
        datos.rubies = value;
    }
    public void setExtraTiros(int value)
    {
        datos.extratiros = value;
    }

    public void addLevelData(int level, int stars)
    {
     if( datos.levels-1 >= level)
        {
            
            datos.level[level-1] = stars;
        }
     else
        {
            datos.level.Add(stars);
            datos.levels++;
            SaveData();
        }
    }

   
}