using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour {
    Rigidbody2D rb; 
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setForce(Vector2 force){
        GetComponent<Rigidbody2D>().AddForce(force);
    }
    void LateUpdate()
    {
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!GameManager.GM.Ball(transform.position.x))
        {
            Destroy(this.gameObject);
        }
        else
        {
            GameManager.GM.SetPrimera(this.gameObject);
            rb.velocity = new Vector2(0, 0);
        }
        //gameObject.transform.position = ini;
    }
}
