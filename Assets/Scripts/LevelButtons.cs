using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelButtons : MonoBehaviour {

    // Use this for initialization
    int _id;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void SetId(int id)
    {
        _id = id;
    }
    public int GetId()
    {
        return _id;
    }
    public void SelectLevel()
    {
      //  Destroy(this);
       Debug.Log("Hola soy el " + _id);
    }
}
