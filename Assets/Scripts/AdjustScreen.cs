using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjustScreen : MonoBehaviour {

	// Use this for initialization
	void Start () {
      

    }
	
	// Update is called once per frame
	void Update () {
        var width = Camera.main.orthographicSize * Screen.width / Screen.height;
   
        if (width < transform.localScale.x  || Screen.width * 2 < Screen.height)
        {
            transform.localScale = new Vector3((float)width, transform.localScale.y, transform.localScale.z);
        }
    
    }
}
