using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadZone : MonoBehaviour {
    LevelManager LM;
    GameObject auxBall;
    bool first = false;
    public void Init(LevelManager aux)
    {
        LM = aux;
        
    }
    public void ResetPosition()
    {
        first = true;
        Destroy(auxBall);
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
            }
            
            else ball.MoveTo(LM.ballsink.getPosition(), 1.0f, LM.ballsink.llega);
            ball.Stop();
        }

    }
}
