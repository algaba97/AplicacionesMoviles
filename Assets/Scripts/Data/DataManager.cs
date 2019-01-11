using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;

[Serializable]
public class DataJuego
{
    public int rubies;
    public int puBomba;
    public int puHierro;
    public List< int> estrellas;
    public int niveles;
}

public class DataManager : MonoBehaviour
{
    static DataManager DM;
    DataJuego datos;
    GameManager Gm;

    void Awake()
    {
        if (DM == null)
        {
            DM = this;
            DontDestroyOnLoad(this);
            ReadData();
            Gm = GameManager.getGM();
            Gm.setDM(this);
        }
        else { Destroy(this.gameObject); }
        
    }
    public DataJuego getDatos() {
        return datos;
    }



    public void ReadData()
    {
       // PlayerPrefs.DeleteKey("DATA");

        if(PlayerPrefs.HasKey("DATA"))
        {
        //string filePath = Application.dataPath + "/Resources/Mapas/data";
        //if (File.Exists(filePath)) {
            //string dataAsJson = File.ReadAllText(filePath);

            string dataAsJson = PlayerPrefs.GetString("DATA");
            //byte[] data = Convert.FromBase64String(dataAsJson);
            //string result = System.Text.Encoding.UTF8.GetString(data);
           
            datos = JsonUtility.FromJson<DataJuego>(dataAsJson);

        }
        else
        {
            datos = new DataJuego();
            datos.rubies = 50;
            datos.puBomba = 0;
            datos.puHierro = 0;
            datos.estrellas = new List<int>(3000);
            datos.estrellas.Add(1);
            datos.niveles = datos.estrellas.Count;
            SaveData();

        }
    }
    public void SaveData()
    {
        //Log string to console window
        var json = JsonUtility.ToJson(datos);
        //string filePath = Application.dataPath + "/Resources/Mapas/data";

        // TextAsset text = (TextAsset)Resources.Load("/Scenes/data.json", typeof(TextAsset));
        //byte[] json2 = System.Text.Encoding.UTF8.GetBytes(json);
        //string data = Convert.ToBase64String(json2);
        //File.WriteAllText(filePath, json);

        PlayerPrefs.SetString("DATA", json);
        PlayerPrefs.Save();
   

    }

    public bool sumaRubies(int value)
    {
        if (datos.rubies + value >= 0)
        {
        datos.rubies += value;
        return true;
        }
        else return false;
    }
    public bool sumaPUbomba(int value)
    {
        if (datos.puBomba + value >= 0)
        {
            datos.puBomba += value;
            return true;
        }
        return false;
    }
    public bool sumaPUhierro(int value)
    {
        if (datos.puHierro + value >= 0)
        {
            datos.puHierro += value;
            return true;
        }
        else return false;
    }

    public void addLevelData(int level, int stars)
    {
     
       
            datos.estrellas[level-1] = stars;
        if (datos.niveles == level)
        {
            datos.estrellas.Add(0);
            datos.niveles++;
        }
            SaveData();
            
    
    }

   
}
