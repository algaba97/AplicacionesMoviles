using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControler : MonoBehaviour {

    // Use this for initialization
    Vector3 ini;
    Rigidbody2D rb;
   
    [Range(1.0f,300.0f)]
    [Tooltip("ball Speed. Value between 1 and 10.")]
    [Header("Bola")]

    public float Fuerza = 5f;
    public GameObject balls;
	void Start () {
        ini =  gameObject.transform.position;
        rb = gameObject.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void Update () {
        
        Vector3 fin;
        if (Input.GetMouseButtonUp(0))
        {
            fin = Camera.main.ScreenToWorldPoint(new Vector3 (Input.mousePosition.x, Input.mousePosition.y,0));
            //el screentoworldpoint, te transforma las coordenadas del raton(1920x1080) a un punto de la escena
            rb.AddForce(new Vector2(fin.x - ini.x, fin.y - ini.y).normalized * Fuerza);
            //hacer un for con el numero actual de bolas que hay en el nivel, y llamar con el invoke o con una
            //coroutine al metodo instance balls para instanciar una bola y darle la fuerza.
        }
	}

    void instanceBalls(){
        //instanciar el gameObject balls declarado arriba
        //añadir la fuerza

        //las pelotas estan en una layer de fisica para que no se colisionen entre sí

    }
    void LateUpdate()
    {
        var width = Camera.main.orthographicSize * Screen.width / Screen.height; //TODO a esto habria que añadirle los margenes en caso de que no fuera vertical
        var height = Camera.main.orthographicSize;

        //rb.velocity = new Vector2(0.0f,0.0f);

        if (transform.position.x > width - 0.2f || transform.position.x < -width + 0.2f) //El 0.2 esta un poco a ojo punteito 
        {
            Debug.Log(transform.position.x);
            rb.velocity = new Vector2(-rb.velocity.x, rb.velocity.y);
        }
        if (transform.position.y > height - 0.2f)
        {
            rb.velocity = new Vector2(rb.velocity.x, -rb.velocity.y);

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
