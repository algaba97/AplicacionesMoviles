using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager GM;
    public int nBolas = 50;
    int contador = 0;
    float iniX = 0.0f; //posicion desde donde se lanzan als bolas
    bool fBola = true;
    GameObject Primera;//primera bola que cae
    List<GameObject> Cubos;
    float tsize = 0.0f;

    public void SetTSize(float aux)
    {
        tsize = aux;
    }
    public void AddCubo(GameObject aux)//Añadir cubo a la lista
    {
        Cubos.Add(aux);
    }
    public void UpdateCubos()// ver cuales estan inactivos y borrarlos
    {
        if (Cubos.Count != 0)
        {
            for (int i = 0; i < Cubos.Count; i++)
            {
                if (!Cubos[i].activeSelf)
                {
                    Destroy(Cubos[i]);
                    Cubos.RemoveAt(i);

                    i--;
                }
            }
        }
        if(Cubos.Count == 0)
        {
            Debug.Log("SIguiente nivelo loko");
        }
    }

    public void Avanza()
    {
        if (Cubos.Count != 0)
        {
            for (int i = 0; i < Cubos.Count; i++)
            {
                Cubos[i].transform.position = new Vector3(Cubos[i].transform.position.x, Cubos[i].transform.position.y - tsize, Cubos[i].transform.position.z);
            }
        }

    }
    
    public void SetPrimera(GameObject aux)// para tener track de la primera bola que cae
    {
        Primera = aux;
    }

   public  GameObject GetPrimera()
    {
        return Primera;
    }


    public bool Ball(float aux)// para llevar el conteo de bolas y ver si es la primera para guardar la x
    {
        contador++;
        if (fBola)
        {
          
            fBola = false;
            iniX = aux;
            return true;
        }
        else
        {
       
            if (contador >= nBolas)
            {
                UpdateCubos();
                Avanza();
                contador = 0;
                fBola = true;
            }
                
            return false;
        }

    }
	// Use this for initialization
	void Start () {
        GM = this;
        Cubos = new List<GameObject>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public float getX() { return iniX; }
}
