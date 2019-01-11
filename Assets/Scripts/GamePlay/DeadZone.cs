using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    LevelManager LM;
    GameObject auxBall;
    BallSink ballsink;

    public GameObject Ball;
    bool first = false;
    public void Init(LevelManager aux,BallSink _ballsink)
    {
        LM = aux;
        ballsink = _ballsink;
        //FirstFakeBall();


    }
    public void FirstFakeBall()
    {
        if (auxBall == null)
        {
            auxBall = Instantiate(Ball);
            auxBall.GetComponent<CircleCollider2D>().enabled = false;
            Vector3 posicion = new Vector3(0, LM.GetPosition(), 0);
            auxBall.transform.position = new Vector3(posicion.x, posicion.y + auxBall.transform.localScale.y /2, posicion.z);
        }
    }
    public void DeleteFirstFakeBall()
    {
        Destroy(auxBall);
    }
    public void ResetPosition()
    {
        first = true;
        Destroy(auxBall);
        ballsink.Hide();
    }
    
    //simulamos que ha llegado una pelota a la posicion de spawn anterior en caso de no haber llegado la primera (first)
    public void FalsaPrimera()
    {
        FirstFakeBall();
        if (first)
        {
            Debug.Log("---------------");
            first = false;
            ballsink.Show(auxBall.transform.position.x, auxBall.transform.position.y);
        }
    }
    //void OnTriggerEnter2D(Collider2D collision)
    void OnCollisionEnter2D(Collision2D collision)
    {

        BallLogic ball = collision.gameObject.GetComponent<BallLogic>();
        if (ball != null)
        {
           
            if (first)
            {
                first = false;
                
                    LM.ballsink.SetPosition(ball.gameObject.transform.position.x);
                    auxBall = ball.gameObject;
                
                LM.boardManager.Ball(ball.gameObject);
             
                ballsink.Show(ball.transform.position.x, ball.transform.position.y);
            }
            
            else ball.MoveTo(LM.ballsink.getPosition(), 10, false,LM.ballsink.llega);
            ball.Stop();
        }

    }
    public GameObject getAuxBall()
    {
        return auxBall;
    }
}
