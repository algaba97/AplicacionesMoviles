using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour {

   public BallSink ballsink;
    public Spawner spawner;
    //AimController aimcontroller;
    public DeadZone deadZone;
    public BoardManager boardManager;
    GameManager gameManager;
    public ReadMap readMap;

    float position; // posicion de spawn de bolas y deadzone

    private void Start() {
        gameManager = GameManager.getGM();
        deadZone.Init(this);
        spawner.Init(this);
        ballsink.Init(this);
        boardManager.Init(gameManager.getTilesize(),readMap,this);
        readMap.Init(this);
    }

    public void NewShot()
    {
        deadZone.ResetPosition();
    }
 
    public void llega(BallLogic bl)
    {
        Debug.Log("Holaaa");
    }
    public void SetPosition(float aux)
    {
        position = aux;
    }
    public float GetPosition()
    {
        return position;
    }
}
