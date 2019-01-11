using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLogic : MonoBehaviour {
    Rigidbody2D rb; 
    AudioSource sound;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        sound.volume = GameManager.getGM().getVolumen();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public void setForce(Vector2 force){ //Launch
        GetComponent<Rigidbody2D>().AddForce(force);
    }

   public  void Stop()
    {
        rb.velocity = new Vector2(0, 0);
    } 
    /// <summary>
    /// Crea una coroutine para ir acercando las bolas al sink, también llama al callback
    /// </summary>
    /// <param name="position"></param>
    /// <param name="time"></param>
    /// <param name="ejeY"></param>
    /// <param name="callback"></param>
   public  void MoveTo(Vector3 position,int vueltas, bool ejeY, System.Action<BallLogic> callback = null)
    {
        StartCoroutine(MovetoCoroutine( position, vueltas, ejeY, callback));
    }

    private IEnumerator MovetoCoroutine(Vector3 position, int vueltas, bool ejeY, System.Action<BallLogic> callback = null)
    {
       
        float totalx = (position.x - transform.position.x)/vueltas;
        float totaly = 0;
        if (ejeY)
        {
             totaly = (position.y - transform.position.y) / vueltas;
        }
        for (int i = 0; i < vueltas; i++)
        {

            transform.position = new Vector3(transform.position.x + totalx, transform.position.y + totaly, position.z);
            //yield return new WaitForSeconds(time / vueltas);
            yield return new WaitForFixedUpdate();
        }
        if (callback != null) { callback(this); }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Cubos")
        sound.Play();
            }
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!GameManager.GM.Ball(transform.position.x))
    //    {
    //        Destroy(this.gameObject);
    //    }


    //}
}
