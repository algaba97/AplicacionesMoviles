using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

using System;
using System.Text;
using System.Security.Cryptography;

[Serializable]
public class DataJuego
{
    public int rubies;
    public int puBomba;
    public int puHierro;
    public int puTerremoto;
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


            string dataAsJson = PlayerPrefs.GetString("DATA");
            
           // Debug.Log(dataAsJson);
            datos = JsonUtility.FromJson<DataJuego>(Decrypt(dataAsJson));

        }
        else
        {
            datos = new DataJuego();
            datos.rubies = 50;
            datos.puBomba = 0;
            datos.puHierro = 0;
            datos.estrellas = new List<int>(500);
            datos.estrellas.Add(0);
            datos.niveles = datos.estrellas.Count;
            SaveData();

        }
    }
    public void SaveData()
    {
        var json = JsonUtility.ToJson(datos);
       

        PlayerPrefs.SetString("DATA", Encrypt(json));
        PlayerPrefs.Save();
   

    }

    public bool sumaRubies(int value)
    {
        if (datos.rubies + value >= 0)
        {
        datos.rubies += value;
            SaveData();

            return true;
        }
        else return false;
    }
    public bool sumaPUbomba(int value)
    {
        if (datos.puBomba + value >= 0)
        {
            datos.puBomba += value;
            SaveData();

            return true;
        }
        return false;
    }
    public bool sumaPUhierro(int value)
    {
        if (datos.puHierro + value >= 0)
        {
            datos.puHierro += value;
            SaveData();
            return true;
        }
        else return false;
    }
    public bool sumaPUTerremoto(int value)
    {
        if (datos.puTerremoto + value >= 0)
        {
            datos.puTerremoto += value;
            SaveData();
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

    private string hash ="4312@!";
    
    //Encrypt
    public string Encrypt(string data)
    {
      
        byte[] datos = UTF8Encoding.UTF8.GetBytes(data);
        using(MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateEncryptor();
                byte[] results = tr.TransformFinalBlock(datos, 0, datos.Length);
                return Convert.ToBase64String(results,0,results.Length);
            } 
        }
    }

    public string Decrypt(string data)
    {

        byte[] datos = Convert.FromBase64String(data);
        using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
        {
            byte[] key = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(hash));
            using (TripleDESCryptoServiceProvider trip = new TripleDESCryptoServiceProvider() { Key = key, Mode = CipherMode.ECB, Padding = PaddingMode.PKCS7 })
            {
                ICryptoTransform tr = trip.CreateDecryptor();
                byte[] results = tr.TransformFinalBlock(datos, 0, datos.Length);
                return UTF8Encoding.UTF8.GetString(results);
            }
        }
    }

}
