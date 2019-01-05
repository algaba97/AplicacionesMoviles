using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        if (GameManager.getGM().getLevel() >= _id)
        {
            GameManager.getGM().setLevel(_id);
            SceneManager.LoadScene(1);
        }
    }
}
