using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    // Use this for initialization
    Vector3 ini;
    Rigidbody2D rb;
   
    [Range(1.0f,100000.0f)]
    [Tooltip("ball Speed. Value between 1 and 10.")]
    [Header("Bola")]

    public float Fuerza = 5f;

	void Start () {
        ini =  gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 fin;
        if (Input.GetMouseButtonUp(0))
        {
            fin = Input.mousePosition;
            rb.AddForce(new Vector2(((fin.x-Screen.width/2) - ini.x), (fin.y - ini.y))); //* (Fuerza*10));
        }
	}
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Cubos"))
        Destroy(collision.gameObject);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        rb.velocity = new Vector2(0, 0);
        gameObject.transform.position = ini;
    }
}
