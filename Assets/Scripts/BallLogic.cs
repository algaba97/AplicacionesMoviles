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
        var width = Camera.main.orthographicSize * Screen.width / Screen.height; //TODO a esto habria que añadirle los margenes en caso de que no fuera vertical
        var height = Camera.main.orthographicSize;

        //rb.velocity = new Vector2(0.0f,0.0f);

        if (transform.position.x > width - 0.2f || transform.position.x < -width + 0.2f) //El 0.2 esta un poco a ojo punteito 
        {

            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        if (transform.position.y > height - 0.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = new Vector2(0, 0);
        //gameObject.transform.position = ini;
    }
}
