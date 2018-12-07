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
    /// <param name="callback"></param>
    void MoveTo(Vector3 position,float time, System.Action<BallLogic> callback = null)
    {
        if (callback != null)

        {
            callback(this);
           
        }
        StartCoroutine(MovetoCoroutine( position,time));
    }

    private IEnumerator MovetoCoroutine(Vector3 position, float time)
    {
        yield return new WaitForSeconds(0.1f);
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (!GameManager.GM.Ball(transform.position.x))
    //    {
    //        Destroy(this.gameObject);
    //    }
     

    //}
}
